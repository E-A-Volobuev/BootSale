using BootSale.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BootSale.Controllers
{
    public class CartController : Controller
    {
        ICartRepository repo;
        public CartController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<ICartRepository>().To<Cart>();
            repo = ninjectKernel.Get<ICartRepository>();
        }
        
        public RedirectToRouteResult AddToCart(Cart cart,int id,string returnUrl)
        {
            Product photo = repo.GetProduct(id);
            if(photo!=null)
            {
                cart.AddItem(photo, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToRouteResult RemoveFromCart(Cart cart,int id, string returnUrl)
        {
            Product photo = repo.GetProduct(id);
            if (photo != null)
            {
                cart.RemoveItem(photo);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View( new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
         public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        public ViewResult Checkout(Cart cart, Purchase purchase)
        {
            if(cart.Lines.Count()==0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }
            if (ModelState.IsValid)
            {
                EmailMessage(cart, purchase);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(purchase);
            }
        }
        static void EmailMessage(Cart cart, Purchase purchase)
        {
            StringBuilder body = new StringBuilder()
                    .AppendLine("Новый заказ обработан")
                    .AppendLine("---")
                    .AppendLine("Товары:");

            foreach (var line in cart.Lines)
            {
                var subtotal = line.Product.Price * line.Quantity;
                body.AppendFormat("{0} x {1} (итого: {2:c}",
                    line.Quantity, line.Product.Name, subtotal);
            }
            body.AppendFormat("Общая стоимость: {0:c}", cart.ComputeValue());
            body.AppendLine("---");
            body.AppendLine("Доставка:");
            body.AppendLine(purchase.Person);
            body.AppendLine(purchase.Adress);

            MailAddress from = new MailAddress("E-A-Volobuev@yandex.ru");
            MailAddress to = new MailAddress(purchase.EmailPurchase);
            MailMessage message = new MailMessage(from, to);
            message.Body = body.ToString();
            SmtpClient client = new SmtpClient("smtp.yandex.ru", 587);
            client.Credentials = new NetworkCredential("E-A-Volobuev@yandex.ru", "maximus1993");
            client.EnableSsl = true;
            client.Send(message);
        }
    }
}