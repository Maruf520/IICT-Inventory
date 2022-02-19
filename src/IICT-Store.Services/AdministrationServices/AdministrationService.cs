using IICT_Store.Dtos.RoleDtos;
using IICT_Store.Models;
using IICT_Store.Models.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.AdministrationServices
{
    public class AdministrationService : IAdministrationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        public AdministrationService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<ServiceResponse<RoleDto>> CreateRole(RoleDto roleDto)
        {
            ServiceResponse<RoleDto> response = new();
            var role = await roleManager.FindByNameAsync(roleDto.RoleName);
            if (role != null)
            {
                response.Messages.Add("Role already exits..");
                return response;
            }
            ApplicationRole applicationRole = new ApplicationRole
            {
                Name = roleDto.RoleName
            };
            await roleManager.CreateAsync(applicationRole);
            response.Messages.Add($"{roleDto.RoleName} role created.");
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteRole(string roleId)
        {
            ServiceResponse<string> response = new();
            var role = await roleManager.FindByIdAsync(roleId);
            await roleManager.DeleteAsync(role);
            response.Messages.Add("Role Deleted");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<List<GetRoleDto>>> GetAllRole()
        {
            List<GetRoleDto> getRoleDtos = new();
            ServiceResponse<List<GetRoleDto>> response = new();
            var roles =  roleManager.Roles.ToList();
            foreach (var role in roles)
            {
                GetRoleDto getRoleDto = new();
                getRoleDto.Id = role.Id.ToString();
                getRoleDto.Name = role.Name;
                getRoleDtos.Add(getRoleDto);
            }
            response.Data = getRoleDtos;
            response.Messages.Add("All Roles.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<GetRoleDto>> UpdateRole(string rolename, string roleId)
        {
            ServiceResponse<GetRoleDto> response = new();
            var role = await roleManager.FindByIdAsync(roleId);
            role.Name = rolename;
            await roleManager.UpdateAsync(role);
            GetRoleDto getRoleDto = new();
            getRoleDto.Name = rolename;
            getRoleDto.Id = roleId;
            response.Data = getRoleDto;
            response.Messages.Add("Role Updated.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<string> UpdateUserInRole(List<UserRoleDto> userRoleDtos, string roleId)
        {
            ServiceResponse<UserRoleDto> response = new();
            var role = await roleManager.FindByIdAsync(roleId);

            for (int i = 0; i < userRoleDtos.Count; i++)
            {
                var user = await userManager.FindByIdAsync(userRoleDtos[i].UserId);
                if (userRoleDtos[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    var x = await userManager.AddToRoleAsync(user, role.Name);
                    if (x.Succeeded)
                    {
                        Console.WriteLine("Added");
                        return "";
                    }
                }
                else if (!userRoleDtos[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

            }
            return "";
        }
    }
    
}
