namespace Models_OnlineShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public int ID { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage ="Chưa chọn ảnh")]
        [Display(Name ="Ảnh")]
        public string Image { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(250)]
        [Display(Name = "Đường dẫn")]
        public string Link { get; set; }

        [StringLength(50)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

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
