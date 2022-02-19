using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models.Users
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Names { get; set; }
        public string Designation { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
    }
}
