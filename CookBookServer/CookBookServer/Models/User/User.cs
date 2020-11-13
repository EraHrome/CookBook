using CookBookServer.Enums;
using CookBookServer.Interfaces;

namespace CookBookServer.Models.User
{
    public class User : IHasStringId
    {
        public string Id { get; set; }

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
