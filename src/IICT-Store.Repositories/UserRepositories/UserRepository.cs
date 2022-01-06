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
            var user = await userManager.CreateAsync(applicationUser, pass);
            return "";
        }

        public async Task<List<ApplicationUser>> GetAll()
        {
            var users =  userManager.Users.ToList();
/*            List<ApplicationUser> applicationUsers = new();
            applicationUsers = users;*/
            return users;
        }
    }
}
