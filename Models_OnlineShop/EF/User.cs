namespace Models_OnlineShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [StringLength(32)]
        [Display(Name = "Mật khẩu")]
        public string PassWord { get; set; }

        [StringLength(20)]
        public string GroupID { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Số ĐT")]
        public string Phone { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        [StringLength(50)]
        public string ModifyBy { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
