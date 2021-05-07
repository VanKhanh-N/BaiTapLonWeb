using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage ="Chưa nhập tên tài khoản")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 đến 12 kí tự")]
        [Required(ErrorMessage = "Chưa nhập tên mật khẩu")]
        public string PassWord { get; set; }

        [Display(Name ="Xác nhật mật khẩu")]
        [Compare("PassWord",ErrorMessage ="Xác nhật mật khẩu không đúng")]
        public string confirmPassword { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Chưa nhập tên")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Chưa nhập địa chỉ")]
        public string Address { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Chưa nhập Email")]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Số ĐT")]
        public string Phone { get; set; }

        
    }
}