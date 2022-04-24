using QuanLyThueXe.Daos;
using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThueXe.Controllers.Public
{
    public class PublicHomeController : Controller
    {
        VehicleDao vehicleDao = new VehicleDao();
        // GET: PublicHome
        public ActionResult Index()
        {
            ViewBag.ListOto = vehicleDao.GetTop5Oto();
            ViewBag.ListXeMay = vehicleDao.GetTop5XeMay();
            return View();
        }
    }
}