using QuanLyThueXe.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThueXe.Controllers.Admin
{
    public class AdminUserController : Controller
    {
        // GET: AdminUser
        UserDao userDao = new UserDao();
        // GET: AdminVoucher
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = userDao.getAllUser();
            return View();
        }
    }
}