using CodeExercise.DataAcccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeExcercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public ActionResult GetAllCategories()
        {
            var categories = _db.Categories.AsNoTracking();
            return Ok(categories);
        } 
    }
}
