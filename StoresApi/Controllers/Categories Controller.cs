using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoresApi.Model;
using StoresApi;
using System.ComponentModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;



namespace StoresApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        
        private static DataContext? _context;
        public CategoriesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Category>> Get()
        {
            var Categories = await _context.Categories.Include(e => e.Products).ToListAsync();
            return Ok(Categories);

        }
        [HttpGet("id")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var Categories = await _context.Categories.Include(e => e.Products).Where(c => c.Id == id).FirstOrDefaultAsync();
            if (Categories == null)
                return BadRequest("Category not Found");
            return Ok(Categories);
        }


        [HttpPost]
        public ActionResult<List<Category>> AddCategory([FromBody] Category Category)
        {
            _context.Categories?.Add(Category);
            _context.SaveChanges();
            return Ok("Success");
        }
        [HttpPost("AddProducttoCategory")]
        public ActionResult<List<Category>> AddProducttoCategory(int ProductId, int CategoryId)
        {
            var Category = _context.Categories.Where(c => c.Id == CategoryId).FirstOrDefault();
            var Product = _context.Products.Where(p => p.Id == ProductId).FirstOrDefault();
            Category.Products?.Add(Product);
            _context.SaveChanges();
            return Ok("Success");
        }

        [HttpGet("CategoryByStore")]
        public async Task<ActionResult<List<Category>>> GetCategoryByStore(int StoreID)
        {
            var Store = await _context.Stores.Include(c => c.Categories).Where(s => s.Id == StoreID).FirstOrDefaultAsync();
            if (Store == null)
                return BadRequest("Store not Found");
            return Ok(Store.Categories);
        }

        [HttpPut] //Update 
        public async Task<ActionResult<Category>> UpdateCategory([FromForm] Category Category)
        {

            var MyCategory = await _context.Categories.Where(h => h.Id == Category.Id).FirstOrDefaultAsync();
            if (MyCategory == null)
                return BadRequest("Category not Found");
            MyCategory.CategoryName = Category.CategoryName;
            MyCategory.Id = Category.Id;
            await _context.SaveChangesAsync();
            return Ok(MyCategory);
        }

        [HttpDelete("id")]
        public async Task<ActionResult<string>> DeleteCategory(int id)
        {
            var MyCategory = await _context.Categories.Where(h => h.Id == id).FirstOrDefaultAsync();
            if (MyCategory == null)
                return BadRequest("Category not Found");
            _context.Categories.Remove(MyCategory);
            await _context.SaveChangesAsync();
            return Ok("Success");
        }
    }
}























