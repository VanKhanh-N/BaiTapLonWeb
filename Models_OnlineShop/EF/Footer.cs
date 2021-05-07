namespace Models_OnlineShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Footer")]
    public partial class Footer
    {
        public long ID { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name="Nội dung")]
        [Required(ErrorMessage ="Bạn chưa nhập nội dung")]
        public string Content { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
