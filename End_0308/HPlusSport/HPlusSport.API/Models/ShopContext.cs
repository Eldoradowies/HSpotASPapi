using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API.Models
{
    // DbContext for the application, representing a session with the database
    public class ShopContext : DbContext
    {
        // Constructor that accepts DbContextOptions and passes them to the base class constructor
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

        // Method to configure the model and its relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configures the relationship between Category and Product
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products) // A Category has many Products
                .WithOne(a => a.Category) // Each Product has one Category
                .HasForeignKey(a => a.CategoryId); // The foreign key is CategoryId

            // Seeds initial data into the database (if the Seed method is defined)
            modelBuilder.Seed();
        }

        // DbSet representing the Products table
        public DbSet<Product> Products { get; set; }

        // DbSet representing the Categories table
        public DbSet<Category> Categories { get; set; }
    }
}
