using CookBookServer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookServer.Models.User
{
    public class User
    {
        public Guid Id { get; set; }

        public UserRoleEnum Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Rating { get; set; }

        public string IsConfirmed { get; set; }
    }
}
