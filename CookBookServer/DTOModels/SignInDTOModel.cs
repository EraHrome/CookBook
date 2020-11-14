using DTOModels.Resourses;
using System.ComponentModel.DataAnnotations;

namespace DTOModels
{
    public class SignInDTOModel
    {
        [Required(ErrorMessageResourceName = "LoginValidationError", ErrorMessageResourceType = typeof(Resource))]
        public string Login { get; set; }


        [Required(ErrorMessageResourceName = "PasswordValidationError", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }
    }
}
