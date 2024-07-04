using HPlusSport.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API.Controllers
{
    // Defines the route for this controller as "api/products"
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;

        // Constructor to inject the ShopContext dependency
        public ProductsController(ShopContext context)
        {
            _context = context;
            
            // Ensures the database is created, if it does not exist
            _context.Database.EnsureCreated();
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            // Asynchronously fetches all products from the database and returns them
            return Ok(await _context.Products.ToArrayAsync());
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            // Asynchronously fetches a product by its ID
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                // Returns 404 if the product is not found
                return NotFound();
            }
            // Returns the product if found
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            /*
            // Validates the model state, uncomment if you have validation rules
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            */
            // Adds the new product to the context
            _context.Products.Add(product);
            // Saves the changes to the database
            await _context.SaveChangesAsync();

            // Returns a 201 Created response with a link to the newly created product
            return CreatedAtAction(
                "GetProduct",
                new { id = product.Id },
                product);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, Product product)
        {
            // Checks if the provided ID matches the product's ID
            if (id != product.Id)
            {
                return BadRequest();
            }

            // Marks the product entity as modified
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                // Attempts to save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Checks if the product still exists in the database
                if (!_context.Products.Any(p => p.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    // Throws if another exception occurs
                    throw;
                }
            }

            // Returns 204 No Content if successful
            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            // Finds the product by ID
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                // Returns 404 if the product is not found
                return NotFound();
            }

            // Removes the product from the context
            _context.Products.Remove(product);
            // Saves the changes to the database
            await _context.SaveChangesAsync();

            // Returns the deleted product
            return product;
        }

        // POST: api/products/Delete?ids=1&ids=2&ids=3
        [HttpPost]
        [Route("Delete")]
        public async Task<ActionResult> DeleteMultiple([FromQuery] int[] ids)
        {
            var products = new List<Product>();
            foreach (var id in ids)
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                // Adds the product to the list of products to be deleted
                products.Add(product);
            }

            // Removes all the products in the list from the context
            _context.Products.RemoveRange(products);
            // Saves the changes to the database
            await _context.SaveChangesAsync();

            // Returns the deleted products
            return Ok(products);
        }
    }
}
