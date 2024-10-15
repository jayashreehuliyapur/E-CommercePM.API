using E_CommercePM.API.Data;
using E_CommercePM.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_CommercePM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    { 
        private readonly E_CommerceDbContext _e_commerceDbContext;
      
        public ProductController(E_CommerceDbContext e_commerceDb)
        {
            this._e_commerceDbContext = e_commerceDb;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var product = await _e_commerceDbContext.Product.ToListAsync();
            return Ok(product);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var products = await _e_commerceDbContext.Product.FirstAsync(i => i.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductModel productmodel)
        {
            if (productmodel == null)
            {
                return BadRequest("no content");
            }
            _e_commerceDbContext.Product.Add(productmodel);
            await _e_commerceDbContext.SaveChangesAsync();
            return Ok(productmodel);

        }

        [HttpPut]
        public async Task<IActionResult> update(int id, [FromBody] ProductModel inputproductmodel)
        {
            if ((inputproductmodel == null) || (id != inputproductmodel.ProductId))
            {
                return NotFound();
            }
            var exsistingmodel = await _e_commerceDbContext.Product.FindAsync(id);
            if (exsistingmodel == null)
            {
                return NotFound();
            }
            exsistingmodel.ProductId = inputproductmodel.ProductId;
            exsistingmodel.Name = inputproductmodel.Name;
            exsistingmodel.Price = inputproductmodel.Price;
            exsistingmodel.Stock = inputproductmodel.Stock;
            exsistingmodel.CategoryId = inputproductmodel.CategoryId;
            exsistingmodel.Description = inputproductmodel.Description;
            await _e_commerceDbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{ProductId=int}")]
        public async Task<IActionResult> delete(int ProductId)
        {
            var product= await _e_commerceDbContext.Product.FindAsync(ProductId);
            if (product == null)
            {
                return NotFound();
            }
            _e_commerceDbContext.Product.Remove(product);
            await _e_commerceDbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("search/name")]
        public async Task<IActionResult> search([FromQuery] string? Name)
        {
            var query = _e_commerceDbContext.Product.AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(x => x.Name.Equals(Name));
            }
            var result = await query.ToListAsync();
            return Ok(result);
        }

    }
}

