using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootSale.Models
{
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
    public class Cart:ICartRepository
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        private ProductContext db = new ProductContext();
        public void AddItem (Product p, int quantity )
        {
            
            CartLine line = lineCollection.Where(s => s.Product.Id == p.Id).FirstOrDefault();
            if(line==null)
            {
                lineCollection.Add(new CartLine { Product = p, Quantity=quantity }); ;

            }
            else
            {
                line.Quantity += quantity;
            }

        }
        public void RemoveItem(Product photo)
        {
            
            lineCollection.RemoveAll(s=>s.Product.Id==photo.Id);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
        public decimal ComputeValue()
        {
            return lineCollection.Sum(s => s.Product.Price * s.Quantity);
        }
        public Product GetProduct(int id)
        {
            return db.Products.Find(id);
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