using System.ComponentModel.DataAnnotations;

namespace Organic_Shop_project.Models
{
    public class Category
    {
        public Category(List<CategoryComponent>? categoryComponents)
        {
            CategoryComponents = categoryComponents;
        }

        public int Id { get; set; }
        [Required(ErrorMessage ="Mütləq doldurulmalıdır"), MinLength(4,ErrorMessage ="3 hərfdən kiçik olmaz")]
        public string Title { get; set; }
        public List<CategoryComponent>? CategoryComponents { get; set; }

    }
}
