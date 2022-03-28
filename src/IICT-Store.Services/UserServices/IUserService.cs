using IICT_Store.Dtos.UserDtos;
using IICT_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.UserServices
{
    public interface IUserService
    {
        Task<ServiceResponse<UserRegistrationDto>> CreateUser(UserRegistrationDto userRegistrationDto);
        Task<ServiceResponse<GetUserDto>> UpdateUser(string id, UserRegistrationDto userRegistrationDto);
        Task<ServiceResponse<GetUserDto>> GetUserById(string id);
        Task<ServiceResponse<List<GetUserDto>>> GetAllUser();
        Task<ServiceResponse<GetUserDto>> GetProfile(string userId);
    }
}
