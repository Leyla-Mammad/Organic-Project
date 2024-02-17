using System.ComponentModel.DataAnnotations;

namespace Organic_Shop_project.ViewModels.Account
{
    public class AccountSignupViewModel
    {
        [Required, MaxLength(255)] 
        
        public string FullName { get; set; }

        [Required, MaxLength(255)]

        public string UserName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]

        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]

        public string ConfirmPassword { get; set; }
    }
}
