using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThueXe.Controllers.Admin
{
    public class AdminHomeController : Controller
    {
        // GET: AdminHome
        public ActionResult Index()
        {
            var Users = (user)Session["Admin"];
            if(Users != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "AdminLogin");
            }
        }
    }
}