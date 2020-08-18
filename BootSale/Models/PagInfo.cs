using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootSale.Models
{
    public class PagInfo
    {
        public int PageNumber { get; set; } // номер текущей страницы
        public int PageSize { get; set; } // кол-во объектов на странице
        public int TotalItems { get; set; } // всего объектов
        public int TotalPages  // всего страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }
    public class IndexViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagInfo PagInfo { get; set; }
        public IEnumerable<Category>Categorys { get; set; }
    }
}