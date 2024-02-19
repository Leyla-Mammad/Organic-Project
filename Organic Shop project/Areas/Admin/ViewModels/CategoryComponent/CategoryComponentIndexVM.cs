 using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Organic_Shop_project.Areas.Admin.ViewModels.CategoryComponent
{

    public class CategoryComponentIndexVM
    {
        public List<Models.CategoryComponent> categoryComponents { get; set; }

    }
}
