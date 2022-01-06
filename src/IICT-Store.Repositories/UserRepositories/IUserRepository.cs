﻿using IICT_Store.Models.Users;
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
    }
}
