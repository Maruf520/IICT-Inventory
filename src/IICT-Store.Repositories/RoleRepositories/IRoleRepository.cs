using IICT_Store.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Repositories.RoleRepositories
{
    public interface IRoleRepository 
    {
        Task<string> CreateAsync(ApplicationRole applicationRol);
    }
}
