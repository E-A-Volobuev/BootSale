using BootSale.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootSale.Controllers
{
    
    public class HomeController : Controller
    {
        private ICategoryRepository repo;
        private IProductRepository _repo;
        public HomeController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            repo = ninjectKernel.Get<ICategoryRepository>();

            ninjectKernel.Bind<IProductRepository>().To<ProductRepository>();
            _repo = ninjectKernel.Get<IProductRepository>();
        }
        // GET: Home
        public ActionResult Index(string category,int page = 1)
        {
            var prod = _repo.NameCategory(category);
            int pageSize = 3; // количество объектов на страницу
            if (category==null)
            {
                IEnumerable<Product> photosPerPages = _repo.ListProducts().Skip((page - 1) * pageSize).Take(pageSize);
                PagInfo pageInfo = new PagInfo { PageNumber = page, PageSize = pageSize, TotalItems = _repo.ListProducts().Count() };
                IndexViewModel ivm = new IndexViewModel { PagInfo = pageInfo, Products = photosPerPages };
                return View(ivm);
            }
            else 
            {
                IEnumerable<Product> photosPerPages = prod.Skip((page - 1) * pageSize).Take(pageSize);
                PagInfo pageInfo = new PagInfo
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalItems = category == null ? _repo.ListProducts().Count() : prod.Count()
                };
                IndexViewModel ivm = new IndexViewModel { PagInfo = pageInfo, Products = photosPerPages };
                return View(ivm);
            }
           
        }

        //Добавление нового продукта
        [HttpGet]
        public ActionResult  AddItem()
        {
            SelectList categorys = new SelectList(repo.ListCategorys(), "Id", "Name");
            ViewBag.Category = categorys;
            return View();
        }
        [HttpPost]
        public JsonResult Uploading(Product b)
        {
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    b.Image = fileName;
                    _repo.SaveProduct(b);
                    // сохраняем файл в папку Files в проекте
                    upload.SaveAs(Server.MapPath("~/Files/" + fileName));
                }
            }
            return Json("файл загружен");
           
        }


    }
}