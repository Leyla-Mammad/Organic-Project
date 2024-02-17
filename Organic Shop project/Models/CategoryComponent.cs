using Organic_Shop_project.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organic_Shop_project.Models
{
    public class CategoryComponent
    {
       
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public string Description { get; set; }
        public string? FilePath { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public ProductStatus status { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
        public Category? Category { get; set; }
        [Required]

        public int CategoryId { get; set; }

        [NotMapped]
        public object Categorycomponents { get;  set; }
        
        public ICollection<ProductPhoto> ProductPhotos { get;  set;}
        public ICollection<BasketProduct> BasketProducts { get; set; }


    }
}
