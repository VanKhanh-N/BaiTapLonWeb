namespace Models_OnlineShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }

        [Required(ErrorMessage = "Chưa nhập tên")]
        [Display(Name = "Tên")]
        [StringLength(250)]
      
        public string Name { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Chưa nhập Mã")]
        [Display(Name = "Mã sản phẩm")]
        public string Code { get; set; }

        [StringLength(250)]
        public string MetaTiltle { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Chưa nhập phần mô tả")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        [Required(ErrorMessage = "Chưa nhập giá")]
        [Display(Name = "Giá")]
        public decimal? Price { get; set; }

      
        [Display(Name = "Giá khuyến mãi")]
        public decimal? PromotionPrice { get; set; }

        [Display(Name = "Bao gồm VAT")]
        public bool? IncludeVAT { get; set; }

        [Required(ErrorMessage = "Chưa nhập sô lượng")]
        [Display(Name = "Sô lượng")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "Chưa chọn danh mục")]
        [Display(Name = "Danh mục")]
        public long? CategoryID { get; set; }
        [Required(ErrorMessage = "Chưa chọn Menu Chính")]
        [Display(Name = "Menu Chính")]
        public long? MainMenuID { get; set; }

        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Chưa nhập Chi tiết")]
        [Display(Name = "Chi tiết")]
        public string Detail { get; set; }

       
        public int? Warranty { get; set; }

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

        public bool ShowOnHome { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }

        [Display(Name = "Nhãn")]
        public long? TagID { get; set; }
    }
}
