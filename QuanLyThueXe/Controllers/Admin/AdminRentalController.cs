using QuanLyThueXe.Daos;
using QuanLyThueXe.Models;
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
        VehicleDao vehicleDao = new VehicleDao();
        // GET: AdminVoucher
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = rentalDao.getAll();
            return View();
        }
        public ActionResult Update(rental rentals)
        {
            rental booking = rentalDao.getRentalID(rentals.rental_id);
            vehicle vehicles = vehicleDao.GetVehicleById(booking.vehicle_id);
            if (rentals.status == 1)
            {
                vehicles.quantity = vehicles.quantity - booking.number_vehicle;
            }
            if (rentals.status == 3)
            {
                vehicles.quantity = vehicles.quantity + booking.number_vehicle;
            }
            vehicleDao.updateQuantity(vehicles);
            rentalDao.update(rentals);
            return RedirectToAction("Index", new { msg = "1" });
        }
    }
}