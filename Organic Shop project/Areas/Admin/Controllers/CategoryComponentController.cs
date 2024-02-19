using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Organic_Shop_project.Areas.Admin.ViewModels.CategoryComponent;
using Organic_Shop_project.DAL;
using Organic_Shop_project.Helpers;
using Organic_Shop_project.Models;

namespace Organic_Shop_project.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "SuperAdmin")]
    public class CategoryComponentController : Controller

    {
        private readonly AppDbContext _db;
        private readonly IFileService _fileservice;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryComponentController(AppDbContext appdbcontext, IFileService fileService , IWebHostEnvironment webHostEnvironment)
        {
            _db = appdbcontext;
            _fileservice = fileService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(List<CategoryComponentIndexVM> model)
        {
           
            var categoryComponents = await _db.CategoryComponents.ToListAsync(); 
            return View(/*categoryComponentIndexVMs*/ categoryComponents);

        
        }

      
        [HttpGet]  

        public async Task<IActionResult> Create()

        {
            var model = new CategoryComponentCreateViewModels()
            {

                Categories = await _db.Categories
                .Select(ct => new SelectListItem()
                {
                    Text = ct.Title,
                    Value = ct.Id.ToString(),
                })
                .ToListAsync()
            };
            return View(model);

        }

        [HttpPost]

        public async Task<IActionResult> Create(CategoryComponentCreateViewModels model)
        {
            model.Categories = await _db.Categories
                .Select(ct => new SelectListItem()
                {
                    Text = ct.Title,
                    Value = ct.Id.ToString(),
                })
                .ToListAsync();

            //return View(model);
            if (!ModelState.IsValid) return View(model);
            bool isExist = await _db.CategoryComponents.AnyAsync(c => c.Name.ToLower().Trim() == model.Name.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Name", "Artıq mövcuddur.");
                return View(model);
            }

            if (!_fileservice.IsImage(model.Photo))
            {
                ModelState.AddModelError("Photo", "Fayl Image formatinda olmalıdır.")
                    ; return View(model);
            }

            int maxSize = 1024;
            if (!_fileservice.CheckSize(model.Photo, maxSize))
            {

                ModelState.AddModelError("Photo", $"Şəklin ölçüsü {maxSize} kb-dan böyükdür. ");
                return View(model);
            }

            if (model.Photo != null)
            {
            await _fileservice.UploadAsync(model.Photo);

            }

            var category = await _db.Categories.FindAsync(model.CategoryId);
            if (category == null) return NotFound();
            var categoryComponent = new CategoryComponent()
            {
                FilePath = await _fileservice.UploadAsync(model.Photo),
                Name = model.Name,
                Description = model.Description,
                CategoryId = category.Id,
            };
            await _db.CategoryComponents.AddAsync(categoryComponent);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            CategoryComponent categoryComponent = await _db.CategoryComponents.FindAsync(Id);
            if (categoryComponent == null) return NotFound();

            var model = new CategoryComponentUpdateVM
            {
                Id = categoryComponent.Id,
                Name = categoryComponent.Name,
                Description = categoryComponent.Description,
                FilePath = categoryComponent.FilePath,
                CategoryId = categoryComponent.CategoryId,
                Categories = await _db.Categories.Select(ct => new SelectListItem()
                {
                    Value = ct.Id.ToString(),
                    Text = ct.Title
                }).ToListAsync()
            };
            return View(model);
        }
    


        [HttpPost]
        public async Task<IActionResult> Update(int id, CategoryComponentUpdateVM model)
        {
            if (id != model.Id) return BadRequest();

            model.Categories = await _db.Categories.Select(ct => new SelectListItem
            {
                Value = ct.Id.ToString(),
                Text = ct.Title
            }).ToListAsync();

            if (!ModelState.IsValid) return View(model);

            bool isExist = await _db.CategoryComponents
                .AnyAsync(cc => cc.Name.ToLower().Trim() == model.Name.ToLower().Trim() && cc.Id != id);  

            if (isExist)
            {
                ModelState.AddModelError("Title", "Artiq movcuddur");
                return View(model);
            }
            var dbCategoryComponent = await _db.CategoryComponents.FindAsync(id);
            if (dbCategoryComponent == null) return NotFound();

            dbCategoryComponent.Name = model.Name;
            dbCategoryComponent.Description = model.Description;
            dbCategoryComponent.CategoryId = model.CategoryId;

            await _db.SaveChangesAsync();


            //if (model.Photo != null)
            //{
            //    if (!_fileService.IsImage(model.Photo))
            //    {
            //        ModelState.AddModelError("Photo", "Fayl image olmalidir");
            //        return View(model);
            //    }
            //    int maxSize = 60;
            //    if (!_fileService.CheckSize(model.Photo, maxSize))
            //    {
            //        ModelState.AddModelError("Photo", $"Şəkilin ölçüsü {maxSize} kb-dan çoxdur");
            //        return View(model);
            //    }

            //    var path = Path.Combine(_webHostEnvironment.WebRootPath, "assets/img", dbCategoryComponent.FilePath);
            //    _fileService.Delete(path);
            //    dbCategoryComponent.FilePath = await _fileService.UploadAsync(model.Photo);
            //}

            //await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
            public async Task<IActionResult> Delete(int Id)
            {
                var categoryComponents = await _db.CategoryComponents.FindAsync(Id);
                if (categoryComponents == null)
                { return NotFound(); }

                _db.CategoryComponents.Remove(categoryComponents);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
        }

    

 };


