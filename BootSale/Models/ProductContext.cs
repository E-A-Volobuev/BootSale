
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BootSale.Models
{
    public class ProductContext : IdentityDbContext<ApplicationUser>
    {
        public ProductContext():base("ProductIdentityDb")
        { }
        public static ProductContext Create()
        {
            return new ProductContext();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }

    }
}