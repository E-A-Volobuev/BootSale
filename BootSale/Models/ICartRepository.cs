using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootSale.Models
{
    interface ICartRepository
    {
        void AddItem(Product p, int quantity);
        void RemoveItem(Product p);
        void Clear();
        IEnumerable<CartLine> Lines { get; }
        decimal ComputeValue();
        Product GetProduct(int id);
    }
}
