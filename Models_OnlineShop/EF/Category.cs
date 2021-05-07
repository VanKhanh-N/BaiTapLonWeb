namespace Models_OnlineShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public int ID { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage ="Chưa nhập tên danh mục")]
        [Display(Name ="Tên danh mục")]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTiltle { get; set; }

        [Display(Name = "Danh mục")]
        public long? ParentID { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(250)]
        public string SeoTiltle { get; set; }

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

        public bool? ShowOnHome { get; set; }
    }
}
