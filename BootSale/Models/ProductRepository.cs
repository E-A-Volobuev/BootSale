using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BootSale.Models
{
    public class ProductRepository:IProductRepository,IDisposable
    {
        private ProductContext db = new ProductContext();
        public void SaveProduct(Product b)
        {
            db.Products.Add(b);
            db.SaveChanges();
        }
       
        public IEnumerable<Product>ListProducts()
        {
            return db.Products;
        }
       
        public Product GetProduct(int id)
        {
            return db.Products.Find(id);
        }
       
        public void EditProduct(int id, Product b)
        {
            if (id == b.Id)
            {
               db.Entry(b).State = EntityState.Modified;
               db.SaveChanges();
            }
           
           
        }
        public void DeleteProduct(int id)
        {
            var b = db.Products.FirstOrDefault(p=>p.Id==id);
            if(b!=null)
            {
                db.Products.Remove(b);
                db.SaveChanges();
            }
                
        }
        //LINQ 
        public IEnumerable<Product> NameCategory(string category)
        {
            var b = db.Products.Where(p => p.Category.Name == category);
            return b;
        }
      

        //ОЧИСТКА НЕУПРАВЛЯЕМЫХ РЕСУРСОВ ПРИ ОБРАЩЕНИИ К БД
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}