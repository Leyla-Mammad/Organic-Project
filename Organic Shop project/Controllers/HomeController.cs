using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic_Shop_project.DAL;
using Organic_Shop_project.Models;
using Organic_Shop_project.ViewModels.Home;

namespace Organic_Shop_project.Controllers
{
    public class HomeController : Controller 
    {
        private readonly AppDbContext _appDb;

        public HomeController(AppDbContext appDbContext)
        {
            _appDb = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var cards = await _appDb.Cards.ToListAsync();
            var categories = await _appDb.Categories.Include(x => x.CategoryComponents).ToListAsync();
            var categoryComponent = await _appDb.CategoryComponents.Take(5).ToListAsync();


            HomeIndexViewModel model = new HomeIndexViewModel
            {

                Cards = cards,
                Categories = categories,
                CategoryComponents = categoryComponent,


            };

        

            return View(model);
        }

    }
}


 
