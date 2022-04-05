using IICT_Store.Dtos.AuthDtos;
using IICT_Store.Models;
using IICT_Store.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }
        public async Task<ServiceResponse<TokenDto>> Login(LoginDto loginDto)
        {
            ServiceResponse<TokenDto> response = new();
            var user = await userManager.FindByEmailAsync(loginDto.email);
            if(user == null)
            {
                response.Messages.Add("Not Found");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            List<string> roleList = new();
            if (user != null && await userManager.CheckPasswordAsync(user, loginDto.password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                   new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                };
                foreach (var userRole in userRoles)
                {
                    roleList.Add(userRole);
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(

                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                    );
                var Token = new JwtSecurityTokenHandler().WriteToken(token);
                var role = "".ToList();
                TokenDto tokenDto = new();
                tokenDto.Token = Token;
                tokenDto.Roles = roleList;
                response.Data = tokenDto;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            response.Messages.Add("Password incorrect.");
            response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            return response;

        }

        public async Task<ServiceResponse<string>> ForgotPasswordTokenGenerator(string email)
        {
            ServiceResponse<string> response = new();
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                response.Data = code;
                return response;
            }
            return response;
        }
        public async Task<ServiceResponse<string>> ResetPassword(ForgotPasswordDto passwordDto)
        {
            ServiceResponse<string> response = new();
            var user = await userManager.FindByEmailAsync(passwordDto.Email);
            if (user == null)
            {
                response.Messages.Add("User Not Found!");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            var resetPassword = await userManager.ResetPasswordAsync(user, passwordDto.Token, passwordDto.Password);
            await userManager.UpdateAsync(user);
            if (!resetPassword.Succeeded)
            {
                foreach (var error in resetPassword.Errors)
                {
                    response.Messages.Add(error.Description);
                }
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return response;
            }
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Messages.Add("Password updated Successfully!");
            return response;
        }
    }
}
