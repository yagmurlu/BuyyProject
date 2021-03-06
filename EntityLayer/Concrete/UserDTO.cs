using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Credential{ get; set; }
        public string Password{ get; set; }
        public string PasswordVerification{ get; set; }
        public string Name{ get; set; }
        public string Surname{ get; set; }
    }
}
