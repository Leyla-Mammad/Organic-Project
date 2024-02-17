using Microsoft.AspNetCore.Mvc.Rendering;
using Organic_Shop_project.Models;
using System.ComponentModel.DataAnnotations;

namespace Organic_Shop_project.Areas.Admin.ViewModels.CategoryComponent
{
    public class CategoryComponentUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string? FilePath { get; set; }
        public Category? Category { get; set; }
        [Required]
        [Display(Name = "Categories")]
        public int CategoryId { get; set; }
        public IFormFile? Photo { get; set; }
        public List<SelectListItem>? Categories { get; set; }





    }
}
