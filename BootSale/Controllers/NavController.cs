using BootSale.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootSale.Controllers
{
    public class NavController : Controller
    {
        private ICategoryRepository repo;
        public NavController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            repo = ninjectKernel.Get<ICategoryRepository>();

        }
        public PartialViewResult Menu(string category)
        {
            ViewBag.SelectCategory = category;
            IEnumerable<string> categorys = repo.ListCategorys().Select(s => s.Name).Distinct().OrderBy(x => x);
            return PartialView(categorys);
        }
        
      
    }
}