using Organic_Shop_project.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Organic_Shop_project.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public List<Card> Cards { get; set; }
        public List<Category> Categories { get; set; }
        public IQueryable<CategoryComponent> Components { get; set; }
        public List<CategoryComponent> CategoryComponents { get; set; } 
        


    }
}
