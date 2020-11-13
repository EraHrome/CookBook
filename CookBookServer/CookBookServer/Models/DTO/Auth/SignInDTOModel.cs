using CookBookServer.Resourses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookServer.Models.DTO.Auth
{
    public class SignInDTOModel
    {
        [Required(ErrorMessageResourceName = "LoginValidationError", ErrorMessageResourceType = typeof(Resource))]
        public string Login { get; set; }


        [Required(ErrorMessageResourceName = "PasswordValidationError", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }
    }
}
