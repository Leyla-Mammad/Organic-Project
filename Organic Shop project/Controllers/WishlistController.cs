using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic_Shop_project.DAL;
using Organic_Shop_project.Models;
using Organic_Shop_project.ViewModels.Wishlist;

namespace Organic_Shop_project.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        public WishlistController(UserManager<User> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var wishlist = await _context.Wishlists.
                Include(bp => bp.WishlistProducts).
                ThenInclude(p => p.CategoryComponent).
                FirstOrDefaultAsync(b => b.User.Id == user.Id);

            var model = new WishlistIndexVM();
            model.WishlistProducts = new List<WishlistProductVM>();
            foreach (var wishlistProduct in wishlist.WishlistProducts)
            {
                var product = new WishlistProductVM
                {
                    Id = wishlistProduct.CategoryComponentId,
                    Description = wishlistProduct.CategoryComponent.Description,
                    Quantity = wishlistProduct.Quantity,
                    Stock = wishlistProduct.CategoryComponent.Quantity,
                    Name = wishlistProduct.CategoryComponent.Name,
                    FilePath = wishlistProduct.CategoryComponent.FilePath,
                    Price = wishlistProduct.CategoryComponent.Price,
                };
                model.WishlistProducts.Add(product);
            }

            return View(model);
        }

        public async Task<IActionResult> Add(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var categorycomponents = await _context.CategoryComponents.FindAsync(id);
            if (categorycomponents == null) return NotFound();
            var userWishlist = await _context.Wishlists.FirstOrDefaultAsync(b => b.UserId == user.Id);
            if (userWishlist == null)
            {
                userWishlist = new Wishlist
                {
                    UserId = user.Id,
                };

                await _context.Wishlists.AddAsync(userWishlist);
                await _context.SaveChangesAsync();
            }
            var wishlistProduct = await _context.WishlistProducts.FirstOrDefaultAsync(b => b.CategoryComponentId == id && b.WishlistId == userWishlist.Id);
            if (wishlistProduct == null)
            {
                wishlistProduct = new WishlistProduct
                {
                    WishlistId = userWishlist.Id,
                    CategoryComponentId = categorycomponents.Id,
                    Quantity = 1,

                };
                await _context.WishlistProducts.AddAsync(wishlistProduct);

            }
            else
            {
                wishlistProduct.Quantity++;
            }

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
