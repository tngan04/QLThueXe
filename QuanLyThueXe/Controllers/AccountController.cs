using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThueXe.Controllers
{
    public class AccountController : Controller
    {
        private QuanLyThueXeContext db = new QuanLyThueXeContext();

        public ActionResult Profile(int? userId)
        {
            // Lấy thông tin người dùng hiện tại từ Session
            user currentUser = (user)Session["USER"];

            // Nếu người dùng chưa đăng nhập (không có trong Session), chuyển hướng đến trang lỗi hoặc yêu cầu đăng nhập
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Auth"); // Redirect đến trang login (tuỳ thuộc vào logic của bạn)
            }

            // Nếu không có userId hoặc userId không tồn tại trong cơ sở dữ liệu, trả về lỗi 404
            if (!userId.HasValue)
            {
                return HttpNotFound("User ID không hợp lệ.");
            }

            var requestedUser = db.Users.Find(userId.Value);

            // Kiểm tra xem người dùng có tồn tại trong database hay không
            if (requestedUser == null)
            {
                return HttpNotFound("Người dùng không tồn tại.");
            }

            // Truyền thông tin người dùng vào ViewBag để sử dụng trong view
            ViewBag.userInformation = requestedUser;

            // Logic hiển thị hồ sơ người dùng
            return View(requestedUser);
        }


        [HttpGet]
        public ActionResult Edit(int userId)
        {
            var user = db.Users.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }

            var model = new user
            {
                user_id = user.user_id,
                profile_picture = user.profile_picture,
                fullname = user.fullname,
                email = user.email,
                password = user.password,
                phonenumber = user.phonenumber,
                role_id = user.role_id,
                cccd = user.cccd,
                gender = user.gender ,
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(user model, HttpPostedFileBase profile_picture)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(model.user_id);
                if (user != null)
                {
                    // Cập nhật thông tin người dùng
                    user.fullname = model.fullname;
                    user.email = model.email;
                    user.password = model.password;
                    user.phonenumber = model.phonenumber;
                    user.cccd = model.cccd;
                    user.gender = model.gender;

                    // Xử lý ảnh mới nếu có
                    if (profile_picture != null && profile_picture.ContentLength > 0)
                    {
                        // Xóa ảnh cũ nếu cần (nếu không cần xóa, bỏ qua phần này)
                        if (!string.IsNullOrEmpty(user.profile_picture))
                        {
                            string oldImagePath = Path.Combine(Server.MapPath("~/UploadedImages"), user.profile_picture);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        // Lưu ảnh mới
                        string fileName = Path.GetFileName(profile_picture.FileName);
                        string path = Path.Combine(Server.MapPath("~/UploadedImages"), fileName);
                        profile_picture.SaveAs(path);
                        user.profile_picture = fileName; // Cập nhật tên tệp ảnh mới vào model
                    }
                    else
                    {
                        // Nếu không có ảnh mới, giữ nguyên ảnh cũ
                        user.profile_picture = user.profile_picture; // Không thay đổi
                    }

                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();

                    // Cập nhật thông tin người dùng trong Session hoặc ViewBag
                    Session["userInfomatiom"] = user; // Lưu thông tin người dùng vào Session

                    return RedirectToAction("Profile", new { userId = model.user_id });
                }
            }

            return View(model); // Trả về View với model nếu có lỗi
        }

    }
}