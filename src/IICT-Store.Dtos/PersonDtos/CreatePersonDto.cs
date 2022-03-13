using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.PersonDtos
{
    public class CreatePersonDto
    {
        [Required]
        public string Name { get; set; }
        public string Designation { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Email { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
