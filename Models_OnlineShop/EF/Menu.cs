namespace Models_OnlineShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Chưa nhập tên")]
        [Display(Name ="Tên")]
        public string Text { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(50)]
        public string Target { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
        [Display(Name = "Kiểu")]
        public int? TypeID { get; set; }
        [Display(Name = "Danh mục")]
        public int? ParentID { get; set; }
    }
}
