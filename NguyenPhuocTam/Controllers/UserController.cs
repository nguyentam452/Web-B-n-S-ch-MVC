using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using NguyenPhuocTam.Models;
namespace NguyenPhuocTam.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        BanHang2Entities db = new BanHang2Entities();
        // ĐĂNG KÝ
        public ActionResult Register()
        {
            return View();
        }
        
        [ValidateAntiForgeryToken]
        public bool checkUserName(string username)
        {
            return db.Accounts.Count(x => x.Username == username) > 0;
        }
        public bool checkEmail(string Email)
        {
            return db.Accounts.Count(x => x.Email == Email) > 0;
        }

            [HttpPost]
        public ActionResult Register([Bind(Include = "Id,Username,Password,Email,Phone,Address,Fullname,IsAdmin,Avatar,Status")] Account account)
        {

                 if (ModelState.IsValid)
                    {
                        account.Password = EncodeMD5(account.Password);
                        if (checkUserName(account.Username))
                        {
                            
                            ViewBag.checkname ="Tài khoảng đã tồn tại!";
                        }
                        else if (checkEmail(account.Email))
                        {
                            ViewBag.checkemail = "Email đã tồn tại!";
                        }
                        else
                        {
                            db.Accounts.Add(account);
                            db.SaveChanges();
                            ViewBag.success = "Đăng ký thành công!";
                            account = new Account();
                        }    
                    }
            return View(account);
        }

        public ActionResult Dangnhap()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap( dangnhap objUser)
        {
            if (ModelState.IsValid)
            {
                using (BanHang2Entities db = new BanHang2Entities())
                {
                    
                    objUser.Password = EncodeMD5(objUser.Password);
                    var obj = db.Accounts.Where(a=>a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        if ((bool)(Session["IsAdmin"] = obj.IsAdmin ==true))
                        {
                            Session["User"] = obj;
                            //Session["UserEmail"] = obj.Email.ToString();
                            Session["UserName"] = obj.Username.ToString();
                            return RedirectToAction("Index", "admin/HomeAdmin");
                        }
                        else if ((bool)(Session["IsAdmin"] = obj.IsAdmin == false))
                        {
                            Session["User"] = obj;
                            //Session["UserEmail"] = obj.Email.ToString();
                            Session["UserName"] = obj.Username.ToString();
                            return RedirectToAction("Index", "Home");

                        }

                    }
                    else
                    {
                        ViewBag.Fail = "Tài khoảng hoặc mật khẩu không đúng!";
                        return View("DangNhap");
                    }
                }
            }
            return View(objUser);
        }
        public ActionResult DangXuat()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");

        }

        public static string EncodeMD5(string pass)
        {
            //   pass = "$%*" + pass + "!~";//salt
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(pass);
            bs = md5.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();

            foreach (byte b in bs)
            {
                s.Append(b.ToString("x1").ToLower());
            }
            pass = s.ToString();
            return pass;
        }
    }
}