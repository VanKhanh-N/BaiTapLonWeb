using Models_OnlineShop.DAO;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var product1 = new ProductDao().ListNewProduct(0, 4);
            ViewBag.Product = new ProductDao().ListNewProduct(4, 4);
            ViewBag.Slide = new SlideDao().ListAll();
            return View(product1);
        }

        [ChildActionOnly]
        public ActionResult _Footer()
        {
            var footer = new FooterDao().GetFooter();
            return PartialView(footer);
        }

        [ChildActionOnly]
        public ActionResult _Slide()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult _TopMenu()
        {
            var topmenu = new MenuDao().ListByGroupID(2);
            return PartialView(topmenu);
        }

        [ChildActionOnly]
        public ActionResult _HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;

            }
            return PartialView(list);
        }

        [ChildActionOnly]
        public ActionResult _MainMenu()
        {
            var mainmenu = new MenuDao().ListByGroupID(1);
            return PartialView(mainmenu);
        }
    }
}