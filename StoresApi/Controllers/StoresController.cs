using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoresApi.Model;
using StoresApi;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace StoresApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {

        
        private static DataContext _context;
        public StoresController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Store>> Get()
        {
            var Stores = await _context.Stores.Include(e => e.Categories).ToListAsync();
            return Ok(Stores);

        }
        [HttpGet("id")]
        public async Task<ActionResult<Store>> Get(int id)
        {
            var Store = await _context.Stores.Include(e => e.Categories).Where(c => c.Id == id).FirstOrDefaultAsync();
            if (Store == null)
                return BadRequest("Store not Found");
            return Ok(Store);
        }
        [HttpGet("CategoryByStore")]
        public async Task<ActionResult<Store>> GetCategoryByStore(int StoreID)
        {
            var Store = await _context.Stores.Include(c => c.Categories).Where(s => s.Id == StoreID).FirstOrDefaultAsync();
            if (Store == null)
                return BadRequest("Store not Found");
            return Ok(Store);
        }
        //  var Category = _context.Categories.Include(p=>p.Products).Where(c => c.Id == Categ

        [HttpPost]
        public ActionResult<List<Store>> AddStore([FromBody] Store Store)
        {
            _context.Stores?.Add(Store);
            _context.SaveChanges();
            return Ok("Success");
        }
        [HttpPost("AddCategorytoStores ")]
        public ActionResult<List<Category>> AddCategoryToStores(int CategoryId, int StoreID)
        {
            var Category = _context.Categories.Where(c => c.Id == StoreID).FirstOrDefault();
            var Product = _context.Products.Where(p => p.Id == CategoryId).FirstOrDefault();
            Category.Products.Add(Product);
            _context.SaveChanges();
            return Ok("Success");
           
        }
        [HttpPut] //Update 
        public async Task<ActionResult<Store>> UpdateStore([FromForm] Store Store)
        {

            var MyStore = await _context.Stores.Where(h => h.Id == Store.Id).FirstOrDefaultAsync();
            if (MyStore == null)
                return BadRequest("Store not Found");
            MyStore.Name = Store.Name;
            MyStore.Address = Store.Address;
            await _context.SaveChangesAsync();
            return Ok(MyStore);
        }

        [HttpDelete("id")]
        public async Task<ActionResult<string>> DeleteStore(int id)
        {
            var MyStore = await _context.Stores.Where(h => h.Id == id).FirstOrDefaultAsync();
            if (MyStore == null)
                return BadRequest("Store not Found");
            _context.Stores.Remove(MyStore);
            await _context.SaveChangesAsync();
            return Ok("Success");
        }
    }
}














