using System.ComponentModel.DataAnnotations;

namespace TM.API.Models
{
    public class UserModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public static UserModel Map(Domain.Models.UserModel model)
        {
            if (model == null)
                return new UserModel();

            return new UserModel
            {
                UserName = model.UserName,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword
            };
        }

        public static Domain.Models.UserModel Map(UserModel model)
        {
            if (model == null)
                return new Domain.Models.UserModel();

            return new Domain.Models.UserModel
            {
                UserName = model.UserName,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword
            };
        }
    }
}