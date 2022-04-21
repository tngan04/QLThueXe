using QuanLyThueXe.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThueXe.Controllers.Admin
{
    public class AdminVehicleController : Controller
    {
        VehicleDao vehicleDao = new VehicleDao();
        // GET: AdminVoucher
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = vehicleDao.getAll();
            return View();
        }
    }
}