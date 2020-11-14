using System.Collections.Generic;
using CookBookServer.Interfaces;
using CookBookServer.Enums;

namespace CookBookServer.Models.User
{
    public class User : IHasStringId
    {
        public string Id { get; set; }

        public UserRoleEnum Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public double Rating { get; set; }

        public bool IsConfirmed { get; set; }
        public IEnumerable<string> RecipesIds { get; set; }
    }
}
