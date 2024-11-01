using QuanLyThueXe.Daos;
using QuanLyThueXe.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThueXe.Controllers.Public
{
    public class PublicAuthenticationController : Controller
    {
        // GET: PublicAuthentication
        UserDao userDao = new UserDao();
        // GET: AdminAuthentication
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            user user1 = new user()
            {
                email = form["email"],
                password = form["password"]
            };
            string passwordMd5 = userDao.md5(form["password"]);
            bool checkLogin = userDao.checkLogin(user1.email, passwordMd5);
            if (checkLogin)
            {
                var userInformation = userDao.getUserByEmail(user1.email);
                Session.Add("USER", userInformation);
                return RedirectToAction("Index", "PublicHome");
            }
            else
            {
                ViewBag.mess = "Email hoặc mật khẩu không chính xác";
                return View("Login");
            }

        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(user user, FormCollection form, HttpPostedFileBase profile_picture)
        {
            string rePassword = form["rePassword"];
            user.gender = form["gender"]; // Lấy giới tính từ form

            // Kiểm tra sự tồn tại của email và CCCD trong cơ sở dữ liệu
            bool checkExistUserName = userDao.CheckExistEmail(user.email);
            bool checkExistCCCD = userDao.CheckExistCCCD(user.cccd);

            // Nếu email hoặc CCCD đã tồn tại, không cho phép đăng ký
            if (checkExistUserName || checkExistCCCD)
            {
                if (checkExistUserName && checkExistCCCD)
                {
                    ViewBag.Message = "Email và CCCD đã tồn tại. Vui lòng đăng nhập.";
                }
                else if (checkExistUserName)
                {
                    ViewBag.Message = "Email đã tồn tại. Vui lòng đăng nhập.";
                }
                else
                {
                    ViewBag.Message = "CCCD đã tồn tại. Vui lòng đăng nhập.";
                }
                return View("Register", user); // Trả về view đăng ký với thông tin đã nhập
            }

            // Kiểm tra mật khẩu
            if (!user.password.Equals(rePassword))
            {
                ViewBag.Message = "Mật khẩu không khớp.";
                return View("Register", user);
            }

            // Xử lý lưu ảnh
            if (profile_picture != null && profile_picture.ContentLength > 0)
            {
                string fileName = Path.GetFileName(profile_picture.FileName);
                string uploadPath = Server.MapPath("~/UploadedImages");

                // Kiểm tra nếu thư mục chưa tồn tại thì tạo mới
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string path = Path.Combine(uploadPath, fileName); // Thay đổi đường dẫn nếu cần
                profile_picture.SaveAs(path);
                user.profile_picture = fileName; // Lưu tên tệp hoặc đường dẫn vào model
            }

            // Mã hóa mật khẩu trước khi lưu
            user.password = userDao.md5(user.password);
            user.role_id = 2; // Giả sử đây là role người dùng thường
            userDao.add(user); // Thêm người dùng mới vào cơ sở dữ liệu

            ViewBag.Message = "Đăng ký thành công!";
            return RedirectToAction("Login");
        }




        public ActionResult Logout()
        {
            Session.Remove("User");
            return Redirect("/PublicHome/Index");
        }
    }
}