using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootSale.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        //ссылка на изображение,хранящиеся на диске
        public string Image { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}