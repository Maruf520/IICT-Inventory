using IICT_Store.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<string> Create(ApplicationUser applicationUser, string pass);
        Task<List<ApplicationUser>> GetAll();
        Task<ApplicationUser> GetById(string id);
        Task<string> Update(ApplicationUser applicationUser);
        Task<ApplicationUser> GetByEmail(string email);
        Task<List<string>> GetUserRoleByEmail(string email);
        Task<List<string>> GetUserByRole(string roleName);
    }
}
