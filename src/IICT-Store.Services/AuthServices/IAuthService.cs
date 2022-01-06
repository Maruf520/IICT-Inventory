﻿using IICT_Store.Dtos.AuthDtos;
using IICT_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.AuthServices
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Login(LoginDto loginDto);
    }
}
