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

        // GET: AdminRental
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = rentalDao.getAll();
            return View();
        }

        public ActionResult Update(rental Rentals)
        {
            // Lấy thông tin booking từ ID
            rental booking = rentalDao.getRentalID(Rentals.rental_id);
            vehicle Vehicles = vehicleDao.GetVehicleById(booking.vehicle_id);

            // Tính tổng tiền thuê
          
            // Cập nhật trạng thái và số lượng xe
            if (Rentals.status == 1) // Đang thuê
            {
                Vehicles.quantity -= Rentals.number_vehicle; // Giảm số lượng xe
            }
            else if (Rentals.status == 3) // Đã trả
            {
                Vehicles.quantity += Rentals.number_vehicle; // Tăng số lượng xe
            }

  
    

            // Cập nhật số lượng xe trong kho
            vehicleDao.Update(Vehicles);
            rentalDao.update(Rentals); // Cập nhật booking

            return RedirectToAction("Index", new { msg = "Cập nhật thành công" });
        }


    }

}