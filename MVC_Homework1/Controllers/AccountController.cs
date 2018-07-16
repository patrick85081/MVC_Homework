using MVC_Homework1.Models;
using MVC_Homework1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC_Homework1.Utils;

namespace MVC_Homework1.Controllers
{
    public class AccountController : Controller
    {
        private readonly I客戶資料Repository customerRespository;

        public AccountController()
        {
            customerRespository = RepositoryHelper.Get客戶資料Repository();
        }

        // GET: Acount
        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                string strLoginID = User.Identity.Name;
                Response.Write($"{strLoginID} 您現在是已登入狀態。");
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            LoginViewModel login = new LoginViewModel();
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel data)
        {
            var account = data.Account;
            var password = data.Password.ToMD5Hash();

            var customer = customerRespository.Login(account, password);
            var isPersistent = false;
            if (customer != null)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    customer.客戶名稱,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    isPersistent,
                    "Customer",
                    FormsAuthentication.FormsCookiePath); 
                
                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessage = "帳號密碼錯誤！！";
            return View(data);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}