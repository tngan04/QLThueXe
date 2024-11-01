using System;
using System.Linq;
using System.Web.Mvc;
using QuanLyThueXe.Models; // Thêm không gian tên của mô hình
using PagedList;
using System.Data.Entity;

public class RevenueController : Controller
{
    private readonly QuanLyThueXeContext _context; // Thay thế bằng context của bạn

    public RevenueController()
    {
        _context = new QuanLyThueXeContext(); // Khởi tạo context
    }

    public ActionResult Index(int? page, DateTime? fromDate, DateTime? toDate, int? month, int? year)
    {
        // Kiểm tra ngày nhập vào
        if (fromDate.HasValue && fromDate.Value > DateTime.Now)
        {
            ModelState.AddModelError("fromDate", "Ngày bắt đầu không được lớn hơn ngày hiện tại.");
        }

        if (toDate.HasValue && toDate.Value > DateTime.Now)
        {
            ModelState.AddModelError("toDate", "Ngày kết thúc không được lớn hơn ngày hiện tại.");
        }

        var rentals = _context.Rentals.AsQueryable();

        // Lọc theo thời gian
        if (fromDate.HasValue && toDate.HasValue)
        {
            rentals = rentals.Where(r => r.date_rental >= fromDate.Value && r.date_rental <= toDate.Value);
        }
        else if (month.HasValue && year.HasValue)
        {
            rentals = rentals.Where(r => r.date_rental.Month == month.Value && r.date_rental.Year == year.Value);
        }

        var totalRevenue = rentals.Sum(r => (decimal?)r.amount) ?? 0; // Xử lý giá trị null trong amount
        ViewBag.TotalRevenue = totalRevenue;

        int pageSize = 10;
        int pageNumber = (page ?? 1);

        var pagedRentals = rentals.OrderBy(r => r.date_rental).ToPagedList(pageNumber, pageSize);

        ViewBag.FromDate = fromDate;
        ViewBag.ToDate = toDate;
        ViewBag.Month = month;
        ViewBag.Year = year;

        return View(pagedRentals);
    }



}
