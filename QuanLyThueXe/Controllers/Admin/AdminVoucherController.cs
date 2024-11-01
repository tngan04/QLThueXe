using QuanLyThueXe.Daos;
using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThueXe.Controllers.Admin
{
    public class AdminVoucherController : Controller
    {
        VoucherDao voucherDao = new VoucherDao(); 
        // GET: AdminVoucher
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = voucherDao.getAll();
            return View();
        }
        public ActionResult Add(voucher Vouchers)
        {
            Vouchers.status = 1;
            voucherDao.add(Vouchers);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult Update(voucher Vouchers)
        {
            voucherDao.update(Vouchers);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult Delete(voucher Vouchers)
        {
            voucherDao.delete(Vouchers.voucher_id);
            return RedirectToAction("Index", new { msg = "1" });
        }
    }
}