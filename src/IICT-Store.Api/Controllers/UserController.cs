﻿using IICT_Store.Dtos.UserDtos;
using IICT_Store.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IICT_Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
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
        public async Task<IActionResult> Update([FromForm] UserUpdateDto userRegistrationDto)
        {
            var user = await userServcie.UpdateUser(GetuserId(), userRegistrationDto);
            return Ok(user);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await userServcie.GetUserById(id);
            return Ok(user);
        }
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var user = await userServcie.GetUserById(GetuserId());
            return Ok(user);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var user = await userServcie.GetAllUser();
            return Ok(user);
        }
    }
}
