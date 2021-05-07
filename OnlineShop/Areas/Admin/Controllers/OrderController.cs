using Models_OnlineShop.DAO;
using Models_OnlineShop.EF;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            ViewBag.searchString = searchString;
            var order = new OrderDao().ListAllPaging(searchString, page, pageSize);
            List<Order> list = new OrderDao().ListAll();
            List<OrderModel> listOrder = new List<OrderModel>();
        
            foreach(var item in list)
            {
                OrderModel model = new OrderModel();
                model.OrderID = item.ID;
                model.Price = new OrderDao().SumPriceByOrderID(item.ID);
                listOrder.Add(model);
            }
            ViewBag.ListOrder = listOrder;
            return View(order);
        }
    }
}