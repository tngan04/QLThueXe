using QuanLyThueXe.Daos;
using QuanLyThueXe.Models;
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
        RentalDao rentalDao = new RentalDao();
        // GET: AdminVoucher
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = vehicleDao.getAll();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(vehicle vehicles)
        {
            var file = Request.Files["file"];
            string reName = DateTime.Now.Ticks.ToString() + file.FileName;
            file.SaveAs(Server.MapPath("~/Content/images/" + reName));
            vehicles.image = reName;
            vehicleDao.add(vehicles);
            return RedirectToAction("Index", new { mess = "1" });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(vehicle vehicles)
        {
            string reName = "";
            var objCourse = vehicleDao.GetVehicleById(vehicles.vehicle_id);
            var file = Request.Files["file"];
            if (file.FileName == "")
            {
                reName = objCourse.image;
            }
            else
            {
                reName = DateTime.Now.Ticks.ToString() + file.FileName;
                file.SaveAs(Server.MapPath("~/Content/images/" + reName));
            }
            vehicles.image = reName;
            vehicleDao.update(vehicles);
            return RedirectToAction("Index", new { mess = "1" });
        }

        public ActionResult Delete(vehicle vehicles)
        {
            var check = rentalDao.getRetalVehicle(vehicles.vehicle_id);
            if(check.Count == 0)
            {
                vehicleDao.delete(vehicles.vehicle_id);
                return RedirectToAction("Index", new { msg = "1" });
            }
            else
            {
                return RedirectToAction("Index", new { msg = "2" });
            }
        }
    }
}