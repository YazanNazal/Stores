using Microsoft.EntityFrameworkCore;
using StoresApi.Model;
using System.Data.Common;
using System.Reflection.Emit;

namespace StoresApi.DB

{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Store>? Stores { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder Builder)
        {

            Builder.Entity<Category>()
                        .HasMany<Product>(p => p.Products)
                        .WithMany(c => c.Categories);
                         

        }
    }

    

}

