using MVC_Homework1.Models;
using MVC_Homework1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC_Homework1.Utils;
using Newtonsoft.Json;

namespace MVC_Homework1.Controllers
{
    public class AccountController : Controller
    {
        private readonly I客戶資料Repository customerRespository;

        //public AccountController()
        //{
        //    customerRespository = RepositoryHelper.Get客戶資料Repository();
        //}

        public AccountController(I客戶資料Repository customerRespository)
        {
            this.customerRespository = customerRespository;
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
            if(User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            LoginViewModel login = new LoginViewModel();
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel data)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = string.Join(", ", ModelState.Values
                    .Where(v => v.Errors.Any())
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)) + "!!!";
                return View(data);
            }

            var userModel = IsLogin(data);
            var isPersistent = false;
            if (userModel != null)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    userModel.UserName,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    isPersistent,
                    JsonConvert.SerializeObject(userModel),
                    FormsAuthentication.FormsCookiePath); 
                
                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                return RedirectToAction("Index");
            }
            else
                ViewBag.ErrorMessage = "帳號密碼錯誤！！";
            return View(data);
        }

        private UserModel IsLogin(LoginViewModel data)
        {
            var account = data.Account;
            var password = data.Password.ToMD5Hash();

            客戶資料 customer = null;

            if (account == "admin" && data.Password == "admin" && data.PasswordConfirm == "admin")
                return new UserModel() {Id = -1, UserName = "Admin", Roles = new string[] {"Admin"}};
            if((customer = customerRespository.Login(account, password))!= null)
                return new UserModel() {Id = customer.Id, UserName = customer.客戶名稱, Roles = new[] {"Customer"}};
            return null;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "Customer")]
        public ActionResult ProfileEdit()
        {
            FormsIdentity id = (FormsIdentity)User.Identity;
            var user = JsonConvert.DeserializeObject<UserModel>(id.Ticket.UserData);

            if (user == null)
                return this.HttpNotFound();

            var customer = customerRespository.Find(user.Id);

            if(customer == null)
                return RedirectToAction("Index", "Home");

            var profileViewModel = new ProfileViewModel()
            {
                Id = customer.Id,
                帳號 = customer.帳號,
                客戶名稱 = customer.客戶名稱,
                統一編號 = customer.統一編號,
                Email = customer.Email,
                電話 = customer.電話,
                傳真 = customer.傳真,
                地址 = customer.地址,
                密碼 = "",
                確認密碼 = ""
            };

            return View(profileViewModel);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileEdit(ProfileViewModel profileViewModel)
        {
            var customer = customerRespository.Find(profileViewModel.Id);

            if (customer == null)
                return HttpNotFound();

            if (ModelState.IsValid)
            {
                customer.電話 = profileViewModel.電話;
                customer.傳真 = profileViewModel.傳真;
                customer.地址 = profileViewModel.地址;
                customer.Email = profileViewModel.Email;

                if (!string.IsNullOrWhiteSpace(profileViewModel.密碼))
                    customer.密碼 = profileViewModel.密碼.ToMD5Hash();

                customerRespository.UnitOfWork.Commit();

                return RedirectToAction("Index", "Home");
            }

            return View(profileViewModel);
        }
    }
}