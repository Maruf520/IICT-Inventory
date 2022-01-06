using AutoMapper;
using IICT_Store.Dtos.UserDtos;
using IICT_Store.Models;
using IICT_Store.Models.Users;
using IICT_Store.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public async Task<ServiceResponse<UserRegistrationDto>> CreateUser(UserRegistrationDto userRegistrationDto)
        {
            ServiceResponse<UserRegistrationDto> response = new();

            ApplicationUser applicationUser = new();
            applicationUser.Designation = userRegistrationDto.Designation;
            applicationUser.Email = userRegistrationDto.Email;
            applicationUser.Phone = userRegistrationDto.Phone;
            applicationUser.Image = "";
            applicationUser.Names = userRegistrationDto.UserName;
            applicationUser.UserName = userRegistrationDto.Email;

            var user = await userRepository.Create(applicationUser, userRegistrationDto.Password);
            response.Messages.Add("User Created");
            response.StatusCode = System.Net.HttpStatusCode.Created;
            return response;
        }
    }
}
