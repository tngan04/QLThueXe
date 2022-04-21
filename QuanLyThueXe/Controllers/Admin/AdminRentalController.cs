using QuanLyThueXe.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThueXe.Controllers.Admin
{
    public class AdminRentalController : Controller
    {
        RentalDao rentalDao = new RentalDao();
        // GET: AdminVoucher
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = rentalDao.getAll();
            return View();
        }
    }
}