﻿using Models_OnlineShop.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    [Serializable]
    public class CartItem
    {
        public Product product { get; set; }
        public int Quantity { get; set; }

    }
}