﻿using IICT_Store.Dtos.RoleDtos;
using IICT_Store.Services.AdministrationServices;
using Microsoft.AspNetCore.Authorization;
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
    public class AdminController : ControllerBase
    {
            private readonly IAdministrationService administrationService;
            public AdminController(IAdministrationService administrationService)
            {
                this.administrationService = administrationService;
            }
       // [Authorize(Roles = "Super Admin")]
        [HttpGet]
            public async Task<IActionResult> GetAllRoles()
            {
                var roles = await administrationService.GetAllRole();
                return Ok(roles);
            }
       // [Authorize(Roles = "Super Admin")]
        [HttpPost]
            public async Task<IActionResult> Create(RoleDto roleDto)
            {
                var role = await administrationService.CreateRole(roleDto);
                return Ok(role);
            }
       // [Authorize(Roles = "Super Admin")]
        [HttpPut("{id}")]
            public async Task<IActionResult> UpdateRole(string rolename, string id)
            {
                var role = await administrationService.UpdateRole(rolename, id);
                return Ok(role);
            }
       // [Authorize(Roles = "Super Admin")]
        [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteRole(string id)
            {
                var role = await administrationService.DeleteRole(id);
                return Ok(role);
            }
       // [Authorize(Roles = "Super Admin")]
        [HttpPost("add-role/{roleid}")]
            public async Task<IActionResult> AddUserInRole(List<UserRoleDto> userRoleDtos, string roleid)
            {
                var role = await administrationService.UpdateUserInRole(userRoleDtos, roleid);
                return Ok(role);
            }
        }
    }

