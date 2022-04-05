using IICT_Store.Dtos.AuthDtos;
using IICT_Store.Services.AuthServices;
using IICT_Store.Services.MailServices;
using IICT_Store.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IICT_Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IUserService userSevice;
        private readonly IMailService emailService;
        public AuthController(IAuthService authService, IUserService userSevice, IMailService emailService)
        {
            this.authService = authService;
            this.userSevice = userSevice;
            this.emailService = emailService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await authService.Login(loginDto);
            return Ok(user);
        }

        [HttpGet("user/forgot-password")]
        public async Task<IActionResult> ForgotPasswordTokenGenerate([Required] string email)
        {
            var user = await userSevice.GetUserByEmail(email);
            if (user.Data == null)
            {
                return NotFound(user);
            }
            var token = await authService.ForgotPasswordTokenGenerator(user.Data.Email);
            var bseUrl = "http://20.210.113.90/reset-password";
            var links =bseUrl + "?Data=" + token.Data + "&" + "email=" + email;
            await emailService.SendEmail(email, "IICT inventory password reset", "This is you password reset link. Use this to reset your password." + "\n" + $"{links}", user.Data.Names);
            return Ok($"Password reset link sent to {email}.");
        }

        [HttpPost("user/reset-password")]
        public async Task<IActionResult> ResetPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var resetPassword = await authService.ResetPassword(forgotPasswordDto);
            return Ok(resetPassword);
        }
    }
}
