﻿using AutoMapper;
using IICT_Store.Dtos.UserDtos;
using IICT_Store.Models;
using IICT_Store.Models.Users;
using IICT_Store.Repositories.UserRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        public UserService(IUserRepository userRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        public async Task<ServiceResponse<GetUserDto>> CreateUser(UserRegistrationDto userRegistrationDto)
        {
            ServiceResponse<GetUserDto> response = new();
            var user = await userRepository.GetByEmail(userRegistrationDto.Email);
            if (user != null)
            {
                response.Messages.Add($"User already exists with {userRegistrationDto.Email}");
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            ApplicationUser applicationUser = new();
            applicationUser.Designation = userRegistrationDto.Designation;
            applicationUser.Email = userRegistrationDto.Email;
            applicationUser.Phone = userRegistrationDto.Phone;
            applicationUser.Image = "";
            if (userRegistrationDto.Image != null)
            {
                var upload = await UploadImage(userRegistrationDto.Image);
                applicationUser.Image = upload;
            }

            applicationUser.Names = userRegistrationDto.UserName;
            applicationUser.UserName = userRegistrationDto.Email;

            await userRepository.Create(applicationUser, userRegistrationDto.Password);
            response.Data = mapper.Map<GetUserDto>(await userRepository.GetById(applicationUser.Id.ToString()));
            response.Messages.Add("User Created");
            response.StatusCode = System.Net.HttpStatusCode.Created;
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> GetUserById(string id)
        {
            ServiceResponse<GetUserDto> response = new();
            var user = await userRepository.GetById(id);
            if (user == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NoContent;
                return response;
            }
            var map = mapper.Map<GetUserDto>(user);
            response.Messages.Add("user");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Data = map;
            return response;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUser()
        {
            ServiceResponse<List<GetUserDto>> response = new();
            var users = await userRepository.GetAll();
            if (users.Count == 0)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NoContent;
                return response;
            }
            var map = mapper.Map<List<GetUserDto>>(users);
            response.Data = map;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(string id, UserUpdateDto userUpdateDto)
        {
            ServiceResponse<GetUserDto> response = new();
            var user = await userRepository.GetById(id);
            if (user == null)
            {
                response.Messages.Add("Not Found.");
                response.StatusCode = System.Net.HttpStatusCode.NoContent;
                return response;
            }

            if (userUpdateDto.Image != null)
            {
                var upload = await UploadImage(userUpdateDto.Image);
                user.Image = upload;
            }

            user.Names = userUpdateDto.UserName;
            user.Phone = userUpdateDto.Phone;
            user.PasswordHash = (userUpdateDto.Password != null && userUpdateDto.Password != "") ? (userManager.PasswordHasher.HashPassword(user, userUpdateDto.Password)) : (user.PasswordHash);
            await userRepository.Update(user);
            response.Data = mapper.Map<GetUserDto>(user);
            response.Messages.Add("Updated.");
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;

        }

        public async Task<string> UploadImage(IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                string fName = Path.GetRandomFileName();

                var getext = Path.GetExtension(formFile.FileName);
                var filename = Path.ChangeExtension(fName, getext);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "files");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath = Path.Combine(filePath, filename);
                var pathdb = "files/" + filename;
                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                    stream.Flush();
                }

                return pathdb;

            }
            return "enter valid photo";
        }

        public async Task<ServiceResponse<GetUserDto>> GetProfile(string userId)
        {
            ServiceResponse<GetUserDto> response = new();
            var user = userRepository.GetById(userId);
            var userToReturn = mapper.Map<GetUserDto>(user);
            response.Data = userToReturn;
            response.SetMessage(new List<string> { new string("Profile.") }, HttpStatusCode.OK);
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> GetUserByEmail(string email)
        {
            ServiceResponse<GetUserDto> response = new();
            var user = await userRepository.GetByEmail(email);
            if (user == null)
            {
                response.SetMessage(new List<string> { new string("User Not Found.") }, HttpStatusCode.NotFound);
                return response;
            }
            var userToMap = mapper.Map<GetUserDto>(user);
            userToMap.Names = user.Names;
            response.Data = userToMap;
            return response;
        }
    }
}
