using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NguyenPhuocTam.Models
{
    public class CartItem
    {
        public string Hinh { get; set; }
        public int SanPhamID { get; set; }
        public string TenSanPham { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public int ThanhTien {
            get {
                return (int)(SoLuong * DonGia);
            }
        }
    }
}