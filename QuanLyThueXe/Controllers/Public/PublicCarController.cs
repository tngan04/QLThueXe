using QuanLyThueXe.Daos;
using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThueXe.Controllers.Public
{
    public class PublicCarController : Controller
    {
        VehicleDao vehicleDao = new VehicleDao();
        VoucherDao voucherDao = new VoucherDao();
        QuanLyXeDbContext myDb = new QuanLyXeDbContext();
        RentalDao rentalDao = new RentalDao();
        // GET: PublicCar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetailCar(int id, string mess)
        {
            ViewBag.mess = mess;
            vehicle obj = vehicleDao.GetVehicleById(id);
            ViewBag.Vehicle = obj;
            ViewBag.ListVehicleRelated = vehicleDao.GetVehivleRelated(obj.type_vehicle);
            return View();
        }

        public ActionResult ListOto(int page)
        {
            if (page == 0)
            {
                page = 1;
            }
            ViewBag.List = vehicleDao.GetOto(page, 10);
            ViewBag.tag = page;
            ViewBag.pageSize = vehicleDao.GetNumberOto();
            return View();
        }

        public ActionResult ListXeMay(int page)
        {
            if (page == 0)
            {
                page = 1;
            }
            ViewBag.List = vehicleDao.GetXeMay(page, 10);
            ViewBag.tag = page;
            ViewBag.pageSize = vehicleDao.GetNumberXeMay();
            return View();
        }

        [HttpPost]
        public ActionResult Search(FormCollection form)
        {
            string name = form["name"];
            string idType = form["idType"];
            return RedirectToAction("Search", new { page = 0, name = name, idType = idType });
        }

        [HttpGet]
        public ActionResult Search(int page, string name, string idType)
        {
            if(idType == "1")
            {
                idType = "Xe ô tô";
            }
            else if (idType == "2")
            {
                idType = "Xe máy";
            }
            if (page == 0)
            {
                page = 1;
            }
            if (name == null && idType != null)
            {
                ViewBag.List = vehicleDao.SearchByType(page, 2, idType);
                ViewBag.tag = page;
                ViewBag.key = 1;
                ViewBag.pageSize = vehicleDao.GetNumberVehicleByType(idType);
                if (idType == "Xe ô tô")
                {
                    idType = "1";
                }
                else if (idType == "Xe máy")
                {
                    idType = "2";
                }
                ViewBag.idType = idType;
               
            }
            else if (name != null && idType == null)
            {
                ViewBag.List = vehicleDao.SearchByName(page, 2, name);
                ViewBag.tag = page;
                ViewBag.key = 2;
                ViewBag.name = name;
                ViewBag.pageSize = vehicleDao.GetNumberVehicleByName(name);
            }
            else if (name != null && idType != null)
            {
               
                ViewBag.List = vehicleDao.SearchByTypeAndName(page, 2, idType, name);
                ViewBag.tag = page;
                ViewBag.key = 3;
                ViewBag.name = name;
                ViewBag.pageSize = vehicleDao.GetNumberVehicleByNameAndType(name, idType);
                if (idType == "Xe ô tô")
                {
                    idType = "1";
                }
                else if (idType == "Xe máy")
                {
                    idType = "2";
                }
                ViewBag.idType = idType;
               
            }
            else if (name == null && idType == null)
            {
                RedirectToAction("Index", "PublicHome");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Booking(rental rental, string voucher_code,int quantity)
        {
            user user = (user)Session["USER"];
            string action = "DetailCar/" + rental.vehicle_id;
            if (user == null)
            {
                return RedirectToAction(action, new { mess = "ErrorLogin" });
            }
            else
            {
                voucher voucher = voucherDao.GetVoucherByName(voucher_code);
                if(voucher != null)
                {
                    if(voucher.quantity == 0)
                    {
                        return RedirectToAction(action, new { mess = "ErrorVoucherNumber" });
                    }else if (DateTime.Now > DateTime.Parse(voucher.date_expire) && voucher.quantity != 0)
                    {
                        return RedirectToAction(action, new { mess = "ErrorVoucherExpire" });
                    }
                    else if(DateTime.Now <= DateTime.Parse(voucher.date_expire) && voucher.quantity != 0)
                    {
                        voucher obj = myDb.vouchers.FirstOrDefault(x => x.name == voucher_code);
                        vehicle objVehicle = vehicleDao.GetVehicleById(rental.vehicle_id);
                        if (objVehicle.quantity > quantity)
                        {
                            int? amount = objVehicle.price * rental.number_day - objVehicle.price * rental.number_day * obj.discount / 100;
                            obj.quantity = obj.quantity - 1;
                            myDb.SaveChanges();
                            rental objRental = new rental
                            {
                                user_id = user.user_id,
                                vehicle_id = rental.vehicle_id,
                                voucher_id = voucher.voucher_id,
                                date_rental = rental.date_rental,
                                number_day = rental.number_day,
                                amount = amount,
                                status = 0
                            };
                            rentalDao.Add(objRental);
                            return RedirectToAction(action, new { mess = "Success" });
                        }
                        else
                        {
                            return RedirectToAction(action, new { mess = "ErrorQuantity" });
                        }
                       
                    }
                }
                else
                {
                    vehicle objVehicle = vehicleDao.GetVehicleById(rental.vehicle_id);
                    if (objVehicle.quantity > quantity)
                    {                       
                        rental objRental = new rental
                        {
                            user_id = user.user_id,
                            vehicle_id = rental.vehicle_id,
                            voucher_id = null,
                            date_rental = rental.date_rental,
                            number_day = rental.number_day,
                            amount = objVehicle.price* rental.number_day,
                            status = 0
                        };
                        rentalDao.Add(objRental);
                        return RedirectToAction(action, new { mess = "Success" });
                    }
                    else
                    {
                        return RedirectToAction(action, new { mess = "ErrorQuantity" });
                    }
                  
                }
                return RedirectToAction(action, new { mess = "Success" });
            }
        }
    }
}