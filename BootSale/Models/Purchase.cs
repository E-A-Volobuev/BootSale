using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BootSale.Models
{
    public  class Purchase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите адрес")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Укажите как вас зовут")]
        public string Person { get; set; }

        [Required(ErrorMessage = "Укажите Email")]
        public string EmailPurchase { get; set; }
       

    }
}