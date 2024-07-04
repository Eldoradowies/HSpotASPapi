namespace HPlusSport.API.Models
{
    // Represents a category of products
    public class Category
    {
        // Unique identifier for the category
        public int Id { get; set; }
        
        // Name of the category, initialized to an empty string
        public string Name { get; set; } = string.Empty;

        // Navigation property representing the list of products in this category
        // 'virtual' keyword enables lazy loading by Entity Framework Core
        public virtual List<Product> Products { get; set; }
    }
}
