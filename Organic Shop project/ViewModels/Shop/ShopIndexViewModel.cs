using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Organic_Shop_project.Models;

    




namespace Organic_Shop_project.ViewModels.Shop
{
    public class ShopIndexViewModel
    {
        public List<Category> Categories { get; set; }
        public IQueryable<CategoryComponent> Components { get; set; }

        #region Filter
        public string? Name { get; set; }
        //[Display(Name = "Minimum Quantity")]
        public int? MinPrice { get; set; }
        //[Display(Name = "Maximum Quantity")]

        public int? MaxPrice { get; set; }
        //[Display(Name = "Category")]

        public int? CategoryId { get; set; }
        public List<SelectListItem>? Categoriess { get; set; }

        #endregion Filter

    }
}
