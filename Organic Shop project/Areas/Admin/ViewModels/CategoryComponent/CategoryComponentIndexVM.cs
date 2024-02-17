using Microsoft.AspNetCore.Mvc.Rendering;

namespace Organic_Shop_project.Areas.Admin.ViewModels.CategoryComponent
{

    public class CategoryComponentIndexVM
    {
        #region Filter
        public string? Name { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }

        #endregion Filter
    }
}
