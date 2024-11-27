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

        //public DbSet<CategoriesBrand> CategoriesBrands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ProductDetail>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,3)"); // Hoặc dùng HasPrecision

            modelBuilder.Entity<ProductDetail>()
            .Property(p => p.Price)
            .HasPrecision(18, 3);

            modelBuilder.Entity<Category>().HasData(
            new Category
            {
                CategoryID = Guid.NewGuid(),
                CategoryName = "All Products"
            }
            );



        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.PendingModelChangesWarning));
        //}


    }
}














