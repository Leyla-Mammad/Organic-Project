using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic_Shop_project.DAL;
using Organic_Shop_project.Models;
using System.Drawing.Printing;

namespace Organic_Shop_project.Areas.Admin.Controllers
{
    [Area("Admin")]

	[Authorize(Roles = "SuperAdmin")]
	public class CategoryController : Controller 
    {
        private readonly AppDbContext db;
        public CategoryController(AppDbContext appDbContext)
        { db = appDbContext; }
        public async Task<IActionResult> Index()
        {

            List<Category> categories = await db.Categories.ToListAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View(category);
            bool isExist = await db.Categories.AnyAsync(x => x.Title.ToLower().Trim() == category.Title.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Name", "Artıq Kateqoriya mövcuddur.");
                return View(category);
            }
            await db.Categories.AddAsync(category);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Components(Category category)
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var category = await db.Categories.FindAsync(Id);
            if (category == null)
            { return NotFound(); }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int Id, Category model)
        {

            var category = await db.Categories.FindAsync(Id);

            if (!ModelState.IsValid)
            { return View(category); }

            bool isExist = await db.Categories.AnyAsync(x => x.Title.ToLower().Trim() == model.Title.ToLower().Trim() && x.Id!=Id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Artıq Kateqoriya mövcuddur.");
                return View(category);
            }



            if (category == null)
            { return NotFound(); }

            category.Title = model.Title;
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); }

        //else
        //{

        //    ModelState.AddModelError("Name", "Təkrar daxil etmə.");
        //    return View(model);
        //}


        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var category = await db.Categories.FindAsync(Id);
            if (category == null)
            { return NotFound(); }

            db.Categories.Remove(category);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
           
        }
    }
        
    }
