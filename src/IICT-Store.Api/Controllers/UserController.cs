using IICT_Store.Dtos.UserDtos;
using IICT_Store.Services.UserServices;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService userServcie;
        public UserController(IUserService userServcie)
        {
            this.userServcie = userServcie;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] UserRegistrationDto registrationDto)
        {
            var user = await userServcie.CreateUser(registrationDto);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm]UserRegistrationDto userRegistrationDto)
        {
            var user = await userServcie.UpdateUser("",userRegistrationDto);
            return Ok(user);
        }
    }
}
