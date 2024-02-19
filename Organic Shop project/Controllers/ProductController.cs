using Microsoft.AspNetCore.Mvc;
using Organic_Shop_project.DAL;

namespace Organic_Shop_project.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {  _context = context; }
        public IActionResult Index()
        {
            return View();
        }
    }
}
