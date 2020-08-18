using BootSale.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

namespace BootSale.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        public async Task<ActionResult>Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email,EmailConfirmed=false };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    MailAddress from = new MailAddress("E-A-Volobuev@yandex.ru");
                    MailAddress to = new MailAddress(user.Email);
                    MailMessage message = new MailMessage(from, to);
                    message.Body = string.Format("Для завершения регистрации перейдите по ссылке:" +
                            "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>",
                Url.Action("ConfirmEmail", "Account", new { Token = user.Id, Email = user.Email }, Request.Url.Scheme));
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient("smtp.yandex.ru", 587);
                    client.Credentials = new NetworkCredential("E-A-Volobuev@yandex.ru", "maximus1993");
                    client.EnableSsl = true;
                    client.Send(message);

                    return RedirectToAction("Confirm", "Account", new { user.Email });
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
                
            }
            return View(model);
        }
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string Token, string Email)
        {
            ApplicationUser user = this.UserManager.FindById(Token);
            if (user != null)
            {
                if (user.Email == Email)
                {
                    user.EmailConfirmed = true;

                    await UserManager.UpdateAsync(user);
                    await UserManager.AddToRoleAsync(user.Id, "user");

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    return RedirectToAction("Confirm", "Account", new {  user.Email });
                }
            }
            else
            {
                return RedirectToAction("Confirm", "Account", new { Email="" });
            }
        }
        [AllowAnonymous]
        public string Confirm(string Email)
        {
            return "На почтовый адрес " + Email + " Вам высланы дальнейшие" +
                    "инструкции по завершению регистрации";
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
                if(user==null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
                else if(user.EmailConfirmed==false)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
        public PartialViewResult YouAccount(LoginModel model)
        {
            return PartialView(model);
        }

    }
}