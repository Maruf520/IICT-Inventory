using IICT_Store.Dtos.RoleDtos;
using IICT_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.AdministrationServices
{
    public interface IAdministrationService
    {
        Task<ServiceResponse<RoleDto>> CreateRole(RoleDto roleDto);
        Task<string> UpdateUserInRole(List<UserRoleDto> userRoleDtos, string roleId);
        Task<ServiceResponse<GetRoleDto>> UpdateRole(string rolename, string roleId);
        Task<ServiceResponse<string>> DeleteRole(string roleId);
        Task<ServiceResponse<List<GetRoleDto>>> GetAllRole();
    }
}
