using Microsoft.AspNetCore.Mvc.Rendering;
using Organic_Shop_project.Constants;
using Organic_Shop_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organic_Shop_project.Areas.Admin.ViewModels.CategoryComponent
{
    public class CategoryComponentCreateViewModels
    {

        [Required]

    
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string? FilePath { get; set; }
        public Category? Category { get; set; }
        [Required]
        [Display(Name = "Categories")]
        public int CategoryId { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
        public Constants.ProductStatus status { get; set; }   

        public List<SelectListItem>? Categories { get; set; }

    }
}
