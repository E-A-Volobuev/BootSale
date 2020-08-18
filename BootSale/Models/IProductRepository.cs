using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootSale.Models
{
    public interface IProductRepository
    {
        void SaveProduct(Product b);
        void EditProduct(int id, Product b);
        void DeleteProduct(int id);
        IEnumerable<Product> NameCategory(string category);

        IEnumerable<Product> ListProducts();
        Product GetProduct(int id);
    }
}
