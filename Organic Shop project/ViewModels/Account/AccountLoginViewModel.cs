using System.ComponentModel.DataAnnotations;

namespace Organic_Shop_project.ViewModels.Account
{
    public class AccountLoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
