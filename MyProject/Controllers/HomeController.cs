using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Models;
using System.Diagnostics;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult Index1(string categoryName)
        {
            var articles = _context.Article
                .Where(a => a.Category.Name == categoryName)
                .ToList();

            return View(articles);
        }
        public void OnGet()
        {
            var categories = _context.Categories.ToList();
            var categoriesString = string.Join(",", categories.Select(c => c.Name));
            HttpContext.Session.SetString("Categories", categoriesString);
            ViewData["Categories"] = categories;
        }
        public IActionResult Index()
        {
            var articles = _context.Article.ToList(); // Assuming _context is your DbContext
            OnGet();
            return View(articles);
                     
            
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
