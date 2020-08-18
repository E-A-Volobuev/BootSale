using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BootSale.Models
{
    public class CategoryRepository:ICategoryRepository,IDisposable
    {
        private ProductContext db = new ProductContext();
        public void SaveCategory(Category b)
        {
            db.Categorys.Add(b);
            db.SaveChanges();
        }

        public IEnumerable<Category> ListCategorys()
        {
            return db.Categorys;
        }

        public Category GetCategory(int id)
        {
            return db.Categorys.Find(id);
        }

        public void EditCategory(int id, Category b)
        {
            if (id == b.Id)
            {
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
            }


        }
        public void DeleteCategory(int id)
        {
            //установка возможности CategoryId == null, для возможности удаления категории
            db.Database.ExecuteSqlCommand("ALTER TABLE dbo.Products ADD CONSTRAINT Products_Categorys FOREIGN KEY (CategoryId) REFERENCES dbo.Categorys (Id) ON DELETE SET NULL");

            var b = db.Categorys.FirstOrDefault(p => p.Id == id);
            if (b != null)
            {
                db.Categorys.Remove(b);
                db.SaveChanges();
            }

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