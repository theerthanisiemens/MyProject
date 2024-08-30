using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Models;

namespace MyProject.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _context;
        public CategoryController(ApplicationDbContext db, IHttpContextAccessor context) {
            _db = db;
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public void OnGet()
        {
            var categories = _db.Categories.ToList();
            var categoriesString = string.Join(",", categories.Select(c => c.Name));
            HttpContext.Session.SetString("Categories", categoriesString);
            ViewData["Categories"] = categories;
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            obj.Id = 0; // Ensure the Id is not set explicitly
            _db.Categories.Add(obj);
            _db.SaveChanges();
            OnGet();
            TempData["Success"] = "Category created successfully";  
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            Category categoryFromDb = _db.Categories.Find(id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            obj.Id = 0; // Ensure the Id is not set explicitly
            _db.Categories.Update(obj);
            _db.SaveChanges();
            OnGet();
            TempData["Success"] = "Category Edited successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            OnGet();
            TempData["Success"] = "Category Deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
