using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            try
            {
                List<Category> objCategoryList = _db.Categories.ToList();

                return Ok(objCategoryList);
            }
            catch (System.Exception ex)
            {

                return StatusCode(500, $"{ex.Message}");
            }
        }



        [HttpPost("create")]
        public IActionResult Create([FromBody] Category obj)
        {
            try
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return Json(new { msg = "Successfully Created an Obj", obj });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");

            }
        }

        [HttpDelete("delete-all")]
        public IActionResult DeleteAll()
        {
            try
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
            catch (System.Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");

            }
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(int? id)
        {
            try
            {
                var categoryObject = _db.Categories.FirstOrDefault(c => c.Id == id);
                if (categoryObject == null) return NotFound();
                return Ok(categoryObject);
            }
            catch (System.Exception ex)
            {

                return StatusCode(500, $"msg: {ex.Message}");
            }
        }

        [HttpPut("edit/{id}")]
        public IActionResult Edit(int? id, [FromBody] Category updatedCategory)
        {
            try
            {
                var categoryObject = _db.Categories.FirstOrDefault(u => u.Id == id);
                if (categoryObject == null) return NotFound();

                categoryObject.Name = updatedCategory.Name;
                categoryObject.DisplayOrder = updatedCategory.DisplayOrder;
                _db.SaveChanges();

                return Ok(new { msg = "New Category created", categoryObject });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int? id)
        {
            try
            {
                var categoryObject = _db.Categories.FirstOrDefault(u => u.Id == id);
                if (categoryObject == null) return NotFound();

                _db.Categories.Remove(categoryObject);
                _db.SaveChanges();

                return Ok(new { msg = "Category deleted", categoryObject });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }
    }
}