using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoresApi.Model;
using StoresApi;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace StoresApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static DataContext? _context;
        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Product>> Get()
        {
            var Products = await _context.Products.Include(e => e.Categories).ToListAsync();
            return Ok(Products);

        }
        [HttpGet("id")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var Product = await _context.Products.Include(e => e.Categories).Where(c => c.Id == id).FirstOrDefaultAsync();
            if (Product == null)
                return BadRequest("Product not Found");
            return Ok(Product);
        }

        [HttpPost]
        public ActionResult<List<Product>> AddProduct([FromBody] Product Product)
        {
            _context.Products?.Add(Product);
            _context.SaveChanges();
            return Ok("Success");
        }
        [HttpGet("ProductByStore")]
        public async Task<ActionResult<List<Product>>> GetProductByStore(int StoreID)
        {
            var cats = await _context.Stores.Include(c => c.Categories).Where(s=>s.Id == StoreID).Select(c=>c.Categories).FirstOrDefaultAsync();
            var products = await _context.Products.Include(c => c.Categories.Where(c => cats.Contains(c))).ToListAsync();
            
            return Ok(products);
        }

        [HttpPut] //Update 
        public async Task<ActionResult<Product>> UpdateProduct([FromForm] Product Product)
        {

            var MyProduct = await _context.Products.Where(h => h.Id == Product.Id).FirstOrDefaultAsync();
            if (MyProduct == null)
                return BadRequest("Product not Found");
            MyProduct.ProductName = Product.ProductName;
            MyProduct.ProductDescription = Product.ProductDescription;
            await _context.SaveChangesAsync();
            return Ok(MyProduct);
        }

        [HttpDelete("id")]
        public async Task<ActionResult<string>> DeleteProduct(int id)
        {
            var MyProduct = await _context.Products.Where(h => h.Id == id).FirstOrDefaultAsync();
            if (MyProduct == null)
                return BadRequest("Product not Found");
            _context.Products.Remove(MyProduct);
            await _context.SaveChangesAsync();
            return Ok("Success");
        }
    }
}
