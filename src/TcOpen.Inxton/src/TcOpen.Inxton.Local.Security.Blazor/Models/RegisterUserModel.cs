
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Local.Security.Blazor.Users
{
    public class RegisterUserModel
    {

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool CanUserChangePassword { get; set; }
        public string Level { get; set; }
        public string Email { get; set; }

        public string Group { get; set; }
    }
}
