﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.UserDtos
{
    public class GetUserDto
    {
        public string Id { get; set; }
        public string Names { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
    }
}
