using QuanLyThueXe.Daos;
using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity; // Thêm dòng này


namespace QuanLyThueXe.Controllers.Public
{
    public class PublicCarController : Controller
    {
        private readonly QuanLyThueXeContext db = new QuanLyThueXeContext();
        VehicleDao vehicleDao = new VehicleDao();
        VoucherDao voucherDao = new VoucherDao();
        QuanLyThueXeContext myDb = new QuanLyThueXeContext();
        RentalDao rentalDao = new RentalDao();
        // GET: PublicCar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetailCar(int id, string mess)
        {
            // Lấy thông tin xe và bao gồm thông tin hãng sản xuất
            var vehicle = db.Vehicles.Include(v => v.Manufacturer).FirstOrDefault(v => v.vehicle_id == id);

            // Kiểm tra nếu không tìm thấy xe
            if (vehicle == null)
            {
                return HttpNotFound();
            }

            // Gán thông điệp và các thông tin cần thiết vào ViewBag
            ViewBag.mess = mess; // Thông điệp từ tham số
            ViewBag.Vehicle = vehicle; // Xe hiện tại
            ViewBag.ListVehicleRelated = vehicleDao.GetVehicleRelated(vehicle.type_vehicle); // Danh sách xe liên quan
            ViewBag.Manufacturer = vehicle.Manufacturer; // Thông tin hãng sản xuất

            return View(); // Trả về view
        }

        public ActionResult ListOto(int? page, string seatingCapacity, string manufacturer)
        {
            int pageSize = 6; // Số lượng xe hiển thị trên mỗi trang
            int pageNumber = page ?? 1; // Nếu không có giá trị trang, mặc định là trang 1

            // Khởi tạo danh sách xe
            IQueryable<vehicle> Vehicles = db.Vehicles;

            // Lọc danh sách xe theo số chỗ ngồi
            if (!string.IsNullOrEmpty(seatingCapacity))
            {
                if (seatingCapacity == "4")
                {
                    Vehicles = Vehicles.Where(v => v.number_vehicle == 4);
                }
                else if (seatingCapacity == "5")
                {
                    Vehicles = Vehicles.Where(v => v.number_vehicle == 5);
                }
                else if (seatingCapacity == "7")
                {
                    Vehicles = Vehicles.Where(v => v.number_vehicle == 7);
                }

                else
                {
                    // Nếu giá trị không hợp lệ, có thể xử lý hoặc thông báo
                    ViewBag.Message = "Số chỗ ngồi không hợp lệ.";
                    Vehicles = Enumerable.Empty<vehicle>().AsQueryable(); // Trả về danh sách rỗng
                }
            }
            if (!string.IsNullOrEmpty(manufacturer))
            {
                Vehicles = Vehicles.Where(v => v.Manufacturer.name == manufacturer);
            }

            // Sắp xếp danh sách xe trước khi thực hiện phân trang
            Vehicles = Vehicles.OrderBy(v => v.vehicle_id); // Sắp xếp theo id xe (hoặc thuộc tính khác phù hợp)

            // Thực hiện phân trang
            int totalVehicles = Vehicles.Count();
            ViewBag.totalPages = (int)Math.Ceiling((double)totalVehicles / pageSize);
            ViewBag.currentPage = pageNumber;
            ViewBag.List = Vehicles.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(); // Lấy danh sách xe theo trang

            return View();
        }



        public ActionResult ListXeMay(int page = 1, string manufacturer = null)
        {
            // Đặt số lượng xe trên mỗi trang
            int pageSize = 6;

            // Lấy danh sách xe máy
            var listXeMay = vehicleDao.GetXeMay(page, pageSize);

            // Lọc theo hãng xe nếu có
            if (!string.IsNullOrEmpty(manufacturer))
            {
                listXeMay = listXeMay.Where(v => v.Manufacturer.name == manufacturer).ToList();
            }

            ViewBag.List = listXeMay;

            // Tính toán số trang tổng cộng
            int totalVehicles = vehicleDao.GetNumberXeMay();
            ViewBag.pageSize = totalVehicles;
            ViewBag.totalPages = (int)Math.Ceiling((double)totalVehicles / pageSize); // Số trang tối đa
            ViewBag.currentPage = page; // Trang hiện tại
            ViewBag.Msg = TempData["Msg"]; // Lấy thông báo từ TempData

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
            if (idType == "1")
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
        public ActionResult Booking(rental rental, string voucher_code, int quantity)
        {
            user user = (user)Session["USER"];
            string action = "DetailCar/" + rental.vehicle_id;

            if (user == null)
            {
                return RedirectToAction(action, new { mess = "ErrorLogin" });
            }

            vehicle objVehicle = vehicleDao.GetVehicleById(rental.vehicle_id);
            if (objVehicle == null)
            {
                return RedirectToAction(action, new { mess = "ErrorVehicleNotFound" });
            }

            // Kiểm tra giá trị
            if (!objVehicle.price.HasValue || !rental.number_day.HasValue)
            {
                return RedirectToAction(action, new { mess = "ErrorInvalidValues" });
            }

            decimal baseAmount = objVehicle.price.Value * rental.number_day.Value * quantity; // Tính giá trị cơ bản
            decimal tiendatcocxeAmount = baseAmount * 0.2m; // Tính 20% đặt cọc

            // Biến amount
            decimal? finalAmount = baseAmount; // Khởi tạo biến để tính toán số tiền cuối cùng

            // Kiểm tra voucher
            voucher voucher = voucherDao.GetVoucherByName(voucher_code);
            if (voucher != null)
            {
                if (voucher.quantity == 0)
                {
                    return RedirectToAction(action, new { mess = "ErrorVoucherNumber" });
                }
                else if (DateTime.Now > DateTime.Parse(voucher.date_expire))
                {
                    return RedirectToAction(action, new { mess = "ErrorVoucherExpire" });
                }
                else if (objVehicle.quantity >= quantity)
                {
                    finalAmount = baseAmount - (baseAmount * voucher.discount / 100); // Tính toán với voucher
                    voucher.quantity--; // Cập nhật số lượng voucher
                    myDb.SaveChanges();
                }
                else
                {
                    return RedirectToAction(action, new { mess = "ErrorQuantity" });
                }
            }

            // Tạo đối tượng rental
            rental objRental = new rental
            {
                user_id = user.user_id,
                vehicle_id = rental.vehicle_id,
                voucher_id = voucher?.voucher_id, // Dùng toán tử null-coalescing
                date_rental = rental.date_rental,
                number_day = rental.number_day,
                amount = rental. amount, // Gán giá trị amount ở đây
                number_vehicle = quantity,
                status = 0,
                tiendatcocxe = tiendatcocxeAmount // Lưu giá trị đặt cọc
            };

            rentalDao.Add(objRental);
            return RedirectToAction(action, new { mess = "Success" });
        }
    }
}