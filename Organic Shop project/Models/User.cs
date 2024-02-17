using Microsoft.AspNetCore.Identity;

namespace Organic_Shop_project.Models
{
    public class User:IdentityUser
    {
        public string FullName { get; set; }    
        public Basket Basket { get; set; }
    }
}
 