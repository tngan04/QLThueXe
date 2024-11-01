using QuanLyThueXe.Daos;
using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace QuanLyThueXe.Controllers.Admin
{
    public class AdminVehicleController : Controller
    {
        private readonly QuanLyThueXeContext db = new QuanLyThueXeContext();
        private readonly VehicleDao vehicleDao = new VehicleDao();
        private readonly RentalDao rentalDao = new RentalDao();

        // GET: AdminVehicle
        public ActionResult Index(string msg, string seatFilter = null)
        {
            TempData["Msg"] = msg;

            // Lấy tất cả xe
            var vehicles = vehicleDao.GetAll().ToList();

            // Nếu có bộ lọc số chỗ ngồi
            if (!string.IsNullOrEmpty(seatFilter))
            {
                vehicles = vehicles.Where(v => v.number_vehicle.ToString() == seatFilter).ToList();
            }

            var manufacturers = vehicleDao.GetAllManufacturer();
            if (manufacturers == null || !manufacturers.Any())
            {
                // Log hoặc xử lý nếu không có hãng xe nào được lấy
                Console.WriteLine("Không có hãng xe nào được lấy.");
            }
            ViewBag.manufacturer_ids = manufacturers;

            // Phân loại xe ô tô thành xe 4 chỗ, 5 chỗ và 7 chỗ
            ViewBag.List4SeaterCars = vehicles.Where(v => v.type_vehicle == "Xe ô tô" && v.number_vehicle == 4).ToList();
            ViewBag.List5SeaterCars = vehicles.Where(v => v.type_vehicle == "Xe ô tô" && v.number_vehicle == 5).ToList();
            ViewBag.List7SeaterCars = vehicles.Where(v => v.type_vehicle == "Xe ô tô" && v.number_vehicle == 7).ToList();
            ViewBag.ListMotorbikes = vehicles.Where(v => v.type_vehicle == "Xe máy").ToList();

            return View(vehicles); // Trả về danh sách xe để hiển thị trong view
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(vehicle vehicle)
        {
            // Lấy manufacturer_id từ form
            var manufacturers = db.Manufacturer.ToList(); // Lấy danh sách hãng xe
            ViewBag.Manufacturers = new SelectList(manufacturers, "manufacturer_id", "name"); // Gán danh sách vào ViewBag

            // Kiểm tra dữ liệu hợp lệ
            if (!ModelState.IsValid)
            {
                ViewBag.manufacturer_ids = vehicleDao.GetAllManufacturer();
                return View(vehicle); // Trả về view nếu dữ liệu không hợp lệ
            }

            // Kiểm tra tệp hình ảnh
            var file = Request.Files["file"];
            if (file != null && file.ContentLength > 0)
            {
                string reName = DateTime.Now.Ticks + Path.GetFileName(file.FileName); // Tạo tên tệp duy nhất
                file.SaveAs(Server.MapPath("~/Content/images/" + reName));
                vehicle.image = reName;
            }

            // Gán loại nhiên liệu
            vehicle.fuel_type = Request.Form["fuel_type"]; // Nhận từ form

            // Kiểm tra và xử lý loại xe
            if (vehicle.type_vehicle == "Xe ô tô")
            {
                // Nếu là xe ô tô, chỉ chấp nhận 4, 5 hoặc 7 chỗ
                if (vehicle.number_vehicle == 4 || vehicle.number_vehicle == 5 || vehicle.number_vehicle == 7)
                {
                    vehicleDao.Add(vehicle); // Thêm vào cơ sở dữ liệu
                    TempData["Msg"] = "Thêm mới thành công!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Số chỗ ngồi không hợp lệ cho xe ô tô. Chỉ chấp nhận 4, 5 hoặc 7 chỗ.");
                }
            }
            else if (vehicle.type_vehicle == "Xe máy")
            {
                // Mặc định số chỗ ngồi cho xe máy là 2
                if (vehicle.number_vehicle == 2)
                {
                    vehicleDao.Add(vehicle); // Thêm vào cơ sở dữ liệu
                    TempData["Msg"] = "Thêm mới thành công!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Số chỗ ngồi không hợp lệ cho xe máy. Chỉ chấp nhận 2 chỗ.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Loại xe không hợp lệ.");
            }

            // Nếu có lỗi, trả về view với thông báo lỗi
            ViewBag.manufacturer_ids = vehicleDao.GetAllManufacturer(); // Đảm bảo danh sách hãng xe vẫn được truyền vào view
            return View(vehicle);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(vehicle updatedVehicle, HttpPostedFileBase file)
        {
            // Validate the model state before processing
            if (!ModelState.IsValid)
            {
                TempData["Msg"] = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại thông tin.";
                ViewBag.manufacturer_ids = vehicleDao.GetAllManufacturer();
                return RedirectToAction("Index");
            }

            // Find the vehicle by vehicle_id
            var vehicle = db.Vehicles.Find(updatedVehicle.vehicle_id);
            if (vehicle != null)
            {
                // Cập nhật các thuộc tính chỉ khi có giá trị hợp lệ
                vehicle.name = !string.IsNullOrEmpty(updatedVehicle.name) ? updatedVehicle.name : vehicle.name;
                vehicle.type_vehicle = !string.IsNullOrEmpty(updatedVehicle.type_vehicle) ? updatedVehicle.type_vehicle : vehicle.type_vehicle;
                vehicle.quantity = updatedVehicle.quantity >= 0 ? updatedVehicle.quantity : vehicle.quantity;
                vehicle.number_vehicle = updatedVehicle.number_vehicle > 0 ? updatedVehicle.number_vehicle : vehicle.number_vehicle;
                vehicle.price = updatedVehicle.price >= 0 ? updatedVehicle.price : vehicle.price;
                vehicle.fuel_type = !string.IsNullOrEmpty(updatedVehicle.fuel_type) ? updatedVehicle.fuel_type : vehicle.fuel_type;
                vehicle.description = !string.IsNullOrEmpty(updatedVehicle.description) ? updatedVehicle.description : vehicle.description;

                // Cập nhật hãng xe nếu có giá trị hợp lệ
                if (updatedVehicle.manufacturer_id != null && updatedVehicle.manufacturer_id > 0)
                {
                    vehicle.manufacturer_id = updatedVehicle.manufacturer_id;
                }

                // Xử lý tệp ảnh mới nếu có
                if (file != null && file.ContentLength > 0)
                {
                    // Xóa tệp hình ảnh cũ
                    var existingImagePath = Path.Combine(Server.MapPath("~/Content/images/"), vehicle.image);
                    if (System.IO.File.Exists(existingImagePath))
                    {
                        System.IO.File.Delete(existingImagePath); // Xóa ảnh cũ
                    }

                    // Lưu tệp ảnh mới
                    string newFileName = DateTime.Now.Ticks + Path.GetFileName(file.FileName); // Tạo tên tệp duy nhất cho ảnh mới
                    var newPath = Path.Combine(Server.MapPath("~/Content/images/"), newFileName);
                    file.SaveAs(newPath);
                    vehicle.image = newFileName; // Cập nhật tên tệp ảnh
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();
                TempData["Msg"] = "Cập nhật thành công!"; // Success message
            }
            else
            {
                TempData["Msg"] = "Không tìm thấy xe để cập nhật."; // Error message
            }

            return RedirectToAction("Index"); // Redirect to the vehicle list page
        }

        public ActionResult Delete(int id)
        {
            var check = rentalDao.getRetalVehicle(id);
            if (!check.Any())
            {
                vehicleDao.Delete(id);
                TempData["Msg"] = "Xóa thành công!";
            }
            else
            {
                TempData["Msg"] = "Không thể xóa xe đã được cho thuê.";
            }

            return RedirectToAction("Index");
        }

        private void SetManufacturerId(vehicle vehicle)
        {
            var manufacturerIdValue = Request.Form["manufacturer_id"];
            if (!string.IsNullOrEmpty(manufacturerIdValue))
            {
                vehicle.manufacturer_id = Convert.ToInt32(manufacturerIdValue);
            }
        }


        private bool ValidateVehicle(vehicle vehicle, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (!ModelState.IsValid)
            {
                errorMsg = "Dữ liệu không hợp lệ.";
                return false;
            }

            if (vehicle.type_vehicle == "Xe ô tô" && (vehicle.number_vehicle != 4 && vehicle.number_vehicle != 5 && vehicle.number_vehicle != 7))
            {
                errorMsg = "Số chỗ ngồi không hợp lệ cho xe ô tô. Chỉ chấp nhận 4, 5 hoặc 7 chỗ.";
                return false;
            }

            if (vehicle.type_vehicle == "Xe máy" && vehicle.number_vehicle != 2)
            {
                errorMsg = "Số chỗ ngồi không hợp lệ cho xe máy. Chỉ chấp nhận 2 chỗ.";
                return false;
            }

            return true;
        }

        private void HandleImageUpload(vehicle vehicle)
        {
            var file = Request.Files["file"];
            if (file != null && file.ContentLength > 0)
            {
                string reName = DateTime.Now.Ticks + Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath("~/Content/images/" + reName));
                vehicle.image = reName;
            }
        }

        private void UpdateVehicleProperties(vehicle vehicle, vehicle updatedVehicle)
        {
            vehicle.name = !string.IsNullOrEmpty(updatedVehicle.name) ? updatedVehicle.name : vehicle.name;
            vehicle.type_vehicle = !string.IsNullOrEmpty(updatedVehicle.type_vehicle) ? updatedVehicle.type_vehicle : vehicle.type_vehicle;
            vehicle.quantity = updatedVehicle.quantity >= 0 ? updatedVehicle.quantity : vehicle.quantity;
            vehicle.number_vehicle = updatedVehicle.number_vehicle > 0 ? updatedVehicle.number_vehicle : vehicle.number_vehicle;
            vehicle.price = updatedVehicle.price >= 0 ? updatedVehicle.price : vehicle.price;
            vehicle.fuel_type = !string.IsNullOrEmpty(updatedVehicle.fuel_type) ? updatedVehicle.fuel_type : vehicle.fuel_type;
            vehicle.description = !string.IsNullOrEmpty(updatedVehicle.description) ? updatedVehicle.description : vehicle.description;

            if (updatedVehicle.manufacturer_id != null)
            {
                vehicle.manufacturer_id = updatedVehicle.manufacturer_id;
            }
        }

        private void HandleImageUpdate(vehicle vehicle, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var existingImagePath = Path.Combine(Server.MapPath("~/Content/images/"), vehicle.image);
                if (System.IO.File.Exists(existingImagePath))
                {
                    System.IO.File.Delete(existingImagePath);
                }

                string newFileName = DateTime.Now.Ticks + Path.GetFileName(file.FileName);
                var newPath = Path.Combine(Server.MapPath("~/Content/images/"), newFileName);
                file.SaveAs(newPath);
                vehicle.image = newFileName;
            }
        }
    }
}
