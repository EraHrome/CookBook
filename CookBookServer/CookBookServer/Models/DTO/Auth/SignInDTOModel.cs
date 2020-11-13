using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookServer.Models.DTO.Auth
{
    public class SignInDTOModel
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}
