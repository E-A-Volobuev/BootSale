using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BootSale.Models;
using Ninject;

namespace BootSale.Controllers
{
    //[Authorize]
    public class ProductsController : ApiController
    {
        //интерфейс продукта
        private IProductRepository repo;
        
        public ProductsController()
        {
            //установка зависимостей
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IProductRepository>().To<ProductRepository>();
            repo = ninjectKernel.Get<IProductRepository>();
        }


        [HttpGet]
        public Product Product(int id)
        {
            return repo.GetProduct(id);
        }

        [HttpPost]
        
        public IHttpActionResult CreateProduct([FromBody]Product b)
        {
            
            if (b.Name == "")
                ModelState.AddModelError("b.Name", "Введите название ");
            if (b.Price == 0)
                ModelState.AddModelError("p.Adress", "Введите стоимость");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            repo.SaveProduct(b);
            return Ok();
        }

        
        [HttpPut]
        
        public void EditProduct(int id, [FromBody] Product b)
        {
            repo.EditProduct(id, b);
        }
       
        [HttpDelete]
        
        public void RemoveProduct(int id)
        {
            repo.DeleteProduct(id);
        }

       
       

    }
}
