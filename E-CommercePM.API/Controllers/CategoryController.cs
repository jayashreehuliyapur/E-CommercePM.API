using E_CommercePM.API.Data;
using E_CommercePM.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_CommercePM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly E_CommerceDbContext _e_commerceDbContext;

        public CategoryController(E_CommerceDbContext e_commerceDb)
        {
            this._e_commerceDbContext = e_commerceDb;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var category = await _e_commerceDbContext.Category.ToListAsync();
            return Ok(category);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _e_commerceDbContext.Category.FirstAsync(i => i.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryModel categorymodel)
        {
            if (categorymodel == null)
            {
                return BadRequest("no content");
            }
            _e_commerceDbContext.Category.Add(categorymodel);
            await _e_commerceDbContext.SaveChangesAsync();
            return Ok(categorymodel);

        }

        [HttpPut]
        public async Task<IActionResult> update(int id, [FromBody] CategoryModel inputcategorymodel)
        {
            if ((inputcategorymodel == null) || (id != inputcategorymodel.CategoryId))
            {
                return NotFound();
            }
            var exsistingmodel=await _e_commerceDbContext.Category.FindAsync(id);
            if (exsistingmodel == null)
            {
                return NotFound();
            }
            exsistingmodel.CategoryId = inputcategorymodel.CategoryId;
            exsistingmodel.CategoryName = inputcategorymodel.CategoryName;
            await _e_commerceDbContext.SaveChangesAsync();
            return NoContent();
        } 

        [HttpDelete("{CategoryId=int}")]
        public async Task<IActionResult> delete(int CategoryId)
        {
            var category=await _e_commerceDbContext.Category.FindAsync(CategoryId);
            if (category == null)
            { 
                return NotFound(); 
            }
            _e_commerceDbContext.Category.Remove(category);
            await _e_commerceDbContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpGet("search/name")]
        public async Task<IActionResult> search([FromQuery] string? CategoryName)
        {
            var query=_e_commerceDbContext.Category.AsQueryable();
            if(!string.IsNullOrEmpty(CategoryName))
            {
                query=query.Where(x=> x.CategoryName.Equals(CategoryName));
            }
            var result=await query.ToListAsync();
            return Ok(result);
        }

    }
}
