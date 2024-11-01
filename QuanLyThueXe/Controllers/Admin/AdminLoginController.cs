using QuanLyThueXe.Daos;
using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThueXe.Controllers.Admin
{
    public class AdminLoginController : Controller
    {
        UserDao userDao = new UserDao();
        // GET: AdminLogin
        public ActionResult Index()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            user Users = new user()
            {
                email = form["email"],
                password = userDao.md5(form["password"])
            };
             
            bool checkLogin = userDao.checkLogin(Users.email, Users.password);
            if (checkLogin)
            {
                var userInformation = userDao.getUserByEmail(Users.email);
                Session.Add("Admin", userInformation);
                if (userInformation.role_id == 2)
                {
                    ViewBag.mess = "Bạn không đủ quyền để truy cập vào trang quản trị";
                    return View("Index");
                }
                else
                {
                    return RedirectToAction("Index", "AdminHome");
                }

            }
            else
            {
                ViewBag.mess = "Thông tin tài khoản hoặc mật khẩu không chính xác";
                return View("Index");
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("Admin");
            return RedirectToAction("Index");
        }
    }
}