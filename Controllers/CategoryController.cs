using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> objCategoryList = _db.Categories.ToList();

            return Json(objCategoryList);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Category obj)
        {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return Json(new { msg = "Successfully Created an Obj", obj });
        }

        public IActionResult DeleteAll()
        {
            // Retrieve all categories from the database
            var categories = _db.Categories.ToList();

            if (categories.Count == 0)
            {
                return Json(new { msg = "No categories to delete." });
            }

            // Remove all categories
            _db.Categories.RemoveRange(categories);

            // Save changes to the database
            _db.SaveChanges();

            return Json(new { msg = $"{categories.Count} categories deleted." });
        }
    }
}