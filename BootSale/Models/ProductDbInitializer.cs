using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BootSale.Models
{
    public class ProductDbInitializer: DropCreateDatabaseAlways<ProductContext>
    {
        protected override void Seed(ProductContext db)
        {
            Category c1 = new Category { Name = "Фотоаппараты" };
            db.Categorys.Add(c1);
            db.SaveChanges();
            
            Product b1 = new Product { Name = "Nikon", Price = 15000,Category=c1  };
            Product b2 = new Product { Name = "Canon", Price = 8000, Category = c1 };
            Product b3 = new Product { Name = "Samsung", Price = 13000, Category = c1 };
            db.Products.AddRange(new List<Product> { b1, b2, b3 });
            db.SaveChanges();
            base.Seed(db);
        }
    }
}