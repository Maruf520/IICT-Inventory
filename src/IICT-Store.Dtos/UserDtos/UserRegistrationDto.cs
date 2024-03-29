﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace IICT_Store.Dtos.UserDtos
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Designation is required")]
        public string Designation { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
