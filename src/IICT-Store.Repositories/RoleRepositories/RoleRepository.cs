using IICT_Store.Models.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.RoleRepositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        public RoleRepository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task<string> CreateAsync(ApplicationRole applicationRol)
        {
            await roleManager.CreateAsync(applicationRol);
            return "";
        }
    }
}
