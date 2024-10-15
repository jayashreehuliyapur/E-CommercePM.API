using E_CommercePM.API.Model;
using Microsoft.EntityFrameworkCore;

namespace E_CommercePM.API.Data
{
    public class E_CommerceDbContext : DbContext
    {
        public E_CommerceDbContext(DbContextOptions<E_CommerceDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<ProductModel> Product { get; set; }
        public DbSet<CategoryModel> Category { get; set; }

       
    }
} 
