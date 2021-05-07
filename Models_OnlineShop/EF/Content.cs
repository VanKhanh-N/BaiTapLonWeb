namespace Models_OnlineShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage ="Chưa nhập tên nội dung")]
        [Display(Name="Tên nội đung")]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTiltle { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Chưa nhập mô tả")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Chưa nhập ảnh")]
        [Display(Name = "Ảnh")]
        public string Image { get; set; }

        [Display(Name = "Danh mục")]
        public long? CategotyID { get; set; }

        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Chưa nhập chi tiết")]
        [Display(Name = "Chi tiết")]
        public string Detail { get; set; }

        [StringLength(10)]
        [Display(Name = "Bảo đảm")]
        public string Warranty { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        [StringLength(50)]
        public string ModifyBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescription { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [Display(Name = "Đưa lên trang chủ")]
        public bool ShowOnHome { get; set; }

        [Display(Name = "Lên Top")]
        public DateTime? TopHot { get; set; }

        [Display(Name = "Số lượt xem")]
        public int? ViewCount { get; set; }

        [StringLength(500)]
        public string Tags { get; set; }
    }
}
