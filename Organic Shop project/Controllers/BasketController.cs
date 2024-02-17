using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic_Shop_project.DAL;
using Organic_Shop_project.Models;
using Organic_Shop_project.ViewModels.Basket;

namespace Organic_Shop_project.Controllers
{
    [Authorize]

    public class BasketController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        public BasketController(UserManager<User> userManager , AppDbContext context)
        {
            _userManager = userManager;
            _context = context; 
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var basket = await _context.Baskets.
                Include(bp=>bp.BasketProducts).
                ThenInclude(p=>p.CategoryComponent).
                FirstOrDefaultAsync(b=>b.User.Id == user.Id);

            var model = new BasketIndexVM();
            foreach (var basketProduct in basket.BasketProducts)
            {
                var product = new BasketProductVM
                {
                    Id= basketProduct.CategoryComponentId,
                    Quantity = basketProduct.Quantity,
                    Stock = basketProduct.CategoryComponent.Quantity,
                    Name = basketProduct.CategoryComponent.Name,
                    FilePath = basketProduct.CategoryComponent.FilePath,
                    Price = basketProduct.CategoryComponent.Price,
                };
                model.BasketProducts.Add(product);
            }

            return View();
        }
        public async Task<IActionResult> Add(int id)
        {
            var user =await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var categorycomponents = await _context.CategoryComponents.FindAsync(id);
            if (categorycomponents == null) return NotFound();
            var userBasket = await _context.Baskets.FirstOrDefaultAsync(b => b.UserId == user.Id);
            if (userBasket == null)
            {
                userBasket = new Basket
                {
                    UserId = user.Id,
                };

                await _context.Baskets.AddAsync(userBasket);
                await _context.SaveChangesAsync();
            }
            var basketProduct = await _context.BasketProducts.FirstOrDefaultAsync(b => b.CategoryComponentId == id);
            if (basketProduct == null)
            {
                basketProduct = new BasketProduct
                {
                    BasketId = userBasket.Id,
                    CategoryComponentId = categorycomponents.Id,
                    Quantity = 1,

                };
                await _context.BasketProducts.AddAsync(basketProduct);

            }
            else
            {
                basketProduct.Quantity++;
            }

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
