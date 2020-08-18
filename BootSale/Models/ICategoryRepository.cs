using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootSale.Models
{
    interface ICategoryRepository
    {
        void SaveCategory(Category c);
        void EditCategory(int id, Category c);
        void DeleteCategory(int id);

        IEnumerable<Category> ListCategorys();
        Category GetCategory(int id);
    }
}
