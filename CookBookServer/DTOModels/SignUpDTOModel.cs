﻿using DTOModels.Resourses;
using System.ComponentModel.DataAnnotations;

namespace DTOModels
{
    public class SignUpDTOModel
    {
        [Required(ErrorMessageResourceName = "FirstNameValidationError", ErrorMessageResourceType = typeof(Resource))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "LastNameValidationError", ErrorMessageResourceType = typeof(Resource))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "LoginValidationError", ErrorMessageResourceType = typeof(Resource))]
        public string Login { get; set; }

        [Required(ErrorMessageResourceName = "EmailValidationError", ErrorMessageResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "PasswordValidationError", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }
    }
}
