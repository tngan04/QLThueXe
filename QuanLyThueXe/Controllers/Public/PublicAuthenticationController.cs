using QuanLyThueXe.Daos;
using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThueXe.Controllers.Public
{
    public class PublicAuthenticationController : Controller
    {
        // GET: PublicAuthentication
        UserDao userDao = new UserDao();
        // GET: AdminAuthentication
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            user user1 = new user()
            {
                email = form["email"],
                password = form["password"]
            };
            string passwordMd5 = userDao.md5(form["password"]);
            bool checkLogin = userDao.checkLogin(user1.email, passwordMd5);
            if (checkLogin)
            {
                var userInformation = userDao.getUserByEmail(user1.email);
                Session.Add("USER", userInformation);
                return RedirectToAction("Index", "PublicHome");
            }
            else
            {
                ViewBag.mess = "Error";
                return View("Login");
            }

        }

        [HttpPost]
        public ActionResult Register(user user, FormCollection form)
        {
            string rePassword = form["rePassword"];
            bool checkExistUserName = userDao.checkExistEmail(user.email);
            if (checkExistUserName)
            {
                ViewBag.mess = "ErrorExist";
                return View("Login");
            }
            else
            {
                if (!user.password.Equals(rePassword))
                {
                    ViewBag.mess = "ErrorPassword";
                    return View("Login");
                }
                else
                {
                    user.password = userDao.md5(user.password);
                    user.role_id = 2;
                    userDao.add(user);
                    ViewBag.mess = "Success";
                    return View("Login");
                }
            }
        }
        public ActionResult Logout()
        {
            Session.Remove("User");
            return Redirect("/PublicHome/Index");
        }
    }
}