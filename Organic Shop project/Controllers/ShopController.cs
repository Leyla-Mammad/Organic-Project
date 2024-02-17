using Microsoft.AspNetCore.Mvc;
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

            public async Task<IActionResult> Index()
            {
                var categories = await _Db.Categories.Include(x => x.CategoryComponents).ToListAsync();

                ShopIndexViewModel model = new ShopIndexViewModel
                {

                    Categories = categories,
                };
               
                return View(model);
            }

        
        }
    }
}
