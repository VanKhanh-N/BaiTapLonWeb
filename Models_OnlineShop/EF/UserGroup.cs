using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Models_OnlineShop.EF
{
    [Table("UserGroup")]
    public partial class UserGroup
    {
        [StringLength(20)]
        public string ID { get; set; }

        [StringLength(20)]
        public string Name { get; set; }
    }
}
