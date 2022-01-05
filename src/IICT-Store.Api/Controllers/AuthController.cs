using IICT_Store.Dtos.AuthDtos;
using IICT_Store.Services.AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IICT_Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await authService.Login(loginDto);
            return Ok(user);
        }
    }
}
