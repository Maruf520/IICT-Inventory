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
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<string> Create(ApplicationUser applicationUser, string pass)
        {
            await userManager.CreateAsync(applicationUser, pass);
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
    }
}
