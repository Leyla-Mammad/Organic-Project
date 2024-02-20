using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Organic_Shop_project.DAL;
using Organic_Shop_project.ViewModels.Shop;
using System.ComponentModel.DataAnnotations;


namespace Organic_Shop_project.Models
{

    namespace Organic_Shop_project.Controllers
    {
        public class ShopController : Controller
        {
            private readonly AppDbContext _Db;
            public ShopController(AppDbContext appDbContext)
            {
                _Db = appDbContext;
            }

            public async Task<IActionResult> Index(ShopIndexViewModel model)
            {

                var categories = await _Db.Categories.Include(x => x.CategoryComponents).ToListAsync();



                var categoryComponent = FilterByTitle(model.Name);
                categoryComponent = FilterByQuantity(model.MinPrice, model.MaxPrice, categoryComponent);
                categoryComponent = FilterByCategory(model.CategoryId, categoryComponent);

                model = new ShopIndexViewModel
                {
                    Components = categoryComponent.Take(9),
                    Categories = categories,
                    Categoriess = await _Db.Categories.Select(ct => new SelectListItem
                    {
                        Value = ct.Id.ToString(),
                        Text = ct.Title,
                    }).ToListAsync() 
                    
                    
                };


                return View(model);
              
            }
            public async Task<IActionResult> Loadmore()
            {
                var categoryComponent = await _Db.CategoryComponents.Skip(9).Take(9).ToListAsync();
                return PartialView("ProductComponentPartialView", categoryComponent);
            }

            private IQueryable<CategoryComponent> FilterByTitle(string? Title)
            {
                return _Db.CategoryComponents.Where(p => !string.IsNullOrEmpty(Title) ? p.Name.Contains(Title) : true);
            }
            private IQueryable<CategoryComponent> FilterByQuantity(int? minprice, int? maxprice, IQueryable<CategoryComponent> categoryComponent)
            {
                return categoryComponent.Where(p => (minprice != null ? p.Price >= minprice : true) && (maxprice != null ? p.Price <= maxprice : true));
            }

            private IQueryable<CategoryComponent> FilterByCategory(int? categoryId, IQueryable<CategoryComponent> categoryComponent)
            {

                return categoryComponent.Where(p => categoryId != null ? p.CategoryId >= categoryId : true);

            }


        }
    }
}
