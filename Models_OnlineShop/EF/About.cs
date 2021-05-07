namespace Models_OnlineShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("About")]
    public partial class About
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage ="Chưa nhập tên")]
        [Display(Name ="Tên")]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTiltle { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Chưa nhập phần mô tả")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Chưa nhập tên")]
        [Display(Name = "Tên")]
        public string Image { get; set; }

        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Chưa nhập chi tiết")]
        [Display(Name = "Chi tiết")]
        public string Detail { get; set; }

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
    }
}
