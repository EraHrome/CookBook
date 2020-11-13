using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookServer.Models.DTO.Auth
{
    public class SignUpDTOModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
