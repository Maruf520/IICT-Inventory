using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.RoleDtos
{
    public class UserRoleDto
    {
        [Required]
        public string UserId { get; set; }
        public string UserName { get; set; }
        [Required]
        public bool IsSelected { get; set; }
    }
}
