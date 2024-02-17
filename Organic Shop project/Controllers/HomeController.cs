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

            HomeIndexViewModel model = new HomeIndexViewModel
            {

                Cards = cards,
            };

            return View(model);
        }

    }
}


 
