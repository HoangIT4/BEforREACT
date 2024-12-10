using BEforREACT.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BEforREACT.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }


        public DbSet<CategoriesBrand> CategoriesBrands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ProductDetail>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,3)");

            modelBuilder.Entity<ProductDetail>()
            .Property(p => p.Price)
            .HasPrecision(18, 3);





        }



    }
}














