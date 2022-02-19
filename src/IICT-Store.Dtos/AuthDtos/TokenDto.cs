using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.AuthDtos
{
    public class TokenDto
    {
        public string Token { get; set; }
        public List<string> Roles { get; set; }
    }
}
