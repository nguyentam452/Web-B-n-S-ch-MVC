using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenPhuocTam.Models;

namespace NguyenPhuocTam.Controllers
{
    public class CartController : Controller
    {
        // GET: Carts
        BanHang2Entities db = new BanHang2Entities();
        public ActionResult Index()
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            return View(giohang);

        }
       
        public RedirectToRouteResult ThemVaoGio(int SanPhamID)
        {
            if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["giohang"] = new List<CartItem>();  // Khởi tạo Session["giohang"] là 1 List<CartItem>
            }

            List<CartItem> giohang = Session["giohang"] as List<CartItem>;  // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID) == null) // ko co sp nay trong gio hang
            {
                Product sp = db.Products.Find(SanPhamID);  // tim sp theo sanPhamID

                CartItem newItem = new CartItem() {
                    SanPhamID = SanPhamID,
                    TenSanPham = sp.Productname,
                    SoLuong = 1,
                    Hinh = sp.Image,
                    DonGia = sp.Price,
                    

                };  // Tạo ra 1 CartItem mới
                giohang.Add(newItem);  // Thêm CartItem vào giỏ 
        
            }
            else
            {
                CartItem cardItem = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
                cardItem.SoLuong++;
              
            }
            
            return RedirectToAction("Index", "Cart", new { id = SanPhamID });
        }
        public RedirectToRouteResult SuaSoLuong(int SanPhamID, int soluongmoi)
        {
            // tìm carditem muon sua
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemSua = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
            if (itemSua != null)
            {
                itemSua.SoLuong = soluongmoi;
            }
            return RedirectToAction("Index");

        }
        public RedirectToRouteResult XoaKhoiGio(int SanPhamID)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemXoa = giohang.FirstOrDefault(m => m.SanPhamID == SanPhamID);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            return RedirectToAction("Index");
        }
        public PartialViewResult cartIteam()
        {
            int item=0;
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
             
            if (giohang != null)
                item = giohang.Sum(m => m.SoLuong);
                ViewBag.cartSL = item;
                return PartialView("cartIteam");
            
        }

    }
}