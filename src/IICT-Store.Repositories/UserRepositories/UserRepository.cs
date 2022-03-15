using IICT_Store.Models.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<string> Create(ApplicationUser applicationUser, string pass)
        {
            var role = await roleManager.FindByNameAsync("User");
            await userManager.CreateAsync(applicationUser, pass);
            await userManager.AddToRoleAsync(applicationUser, role.Name);
            return "Created.";
        }       
        public async Task<string> Update(ApplicationUser applicationUser)
        {
           await userManager.UpdateAsync(applicationUser);
            return "Updated.";
        }

        public async Task<List<ApplicationUser>> GetAll()
        {
            var users =  userManager.Users.ToList();
/*            List<ApplicationUser> applicationUsers = new();
            applicationUsers = users;*/
            return users;
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return user;
        }      
        public async Task<ApplicationUser> GetByEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<List<string>> GetUserRoleByEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var userRoles = await userManager.GetRolesAsync(user);
            return userRoles.ToList();
        }

        public async Task<List<string>> GetUserByRole(string roleName)
        {
            List<string> userList = new();
;            var user = await userManager.GetUsersInRoleAsync(roleName);
            foreach (var userMail in user)
            {
                userList.Add(userMail.Email);
            }
            return userList;
        }
    }
}
