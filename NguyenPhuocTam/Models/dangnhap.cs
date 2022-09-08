using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NguyenPhuocTam.Models
{
    public class dangnhap
    {
        public int Id { get; set; }
        [Display(Name = "Tên Đăng Nhập")]
        [Required(ErrorMessage = "Bạn phải nhập tài khoảng")]
        public string Username { get; set; }
        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}