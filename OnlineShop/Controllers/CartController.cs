using Common;
using Models_OnlineShop.DAO;
using Models_OnlineShop.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CommonConstants = OnlineShop.Common.CommonConstants;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;

            }
            return View(list);
        }

        [HttpGet]
        public ActionResult Payment()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Payment(string shipName, string shipPhone, string shipAddress, string shipEmail)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.ShipAddress = shipAddress;
            order.ShipEmail = shipEmail;
            order.ShipMobile = shipPhone;
            order.ShipName = shipName;
            try
            {
                var id = new OrderDao().Insert(order);
                var detaildao = new OrderDetailDao();
                var cart = (List<CartItem>)Session[CommonConstants.CartSession];

                decimal total = 0;
                foreach (var item in cart)
                {
                    var product = new ProductDao().ViewDetail(item.product.ID);
                    product.Quantity -= item.product.Quantity;
                    new ProductDao().update(product);

                    var detail = new OrderDetail();
                    detail.Quantity = item.Quantity;
                    detail.ProductID = item.product.ID;
                    detail.Price = item.product.Price;
                    detail.OrderID = id;
                    detaildao.Insert(detail);
                    total += (item.product.Price.GetValueOrDefault(0) * item.Quantity);
                }

                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", shipPhone);
                content = content.Replace("{{Email}}", shipEmail);
                content = content.Replace("{{Address}}", shipAddress);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(shipEmail, "Đơn hàng mới từ OnlineShop", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
                Session[CommonConstants.CartSession] = null;
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }
            return Json(new
            {
                status = true
            }) ;
        }

        public ActionResult Error()
        {
            return View();
        }
        public JsonResult AddItem(long productID, int Quantity)
        {
            var cart = Session[CommonConstants.CartSession];
            var product = new ProductDao().ViewDetail(productID);
            if (cart != null)
            {

                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.product.ID == productID))
                {
                    foreach (var item in list)
                    {
                        if (item.product.ID == productID)
                        {
                            item.product.Quantity += Quantity; 
                            item.Quantity += Quantity;
                        }
                    }


                }
                else
                {
                    //tạo mới cartItem
                    var item = new CartItem();
                    item.product = product;
                    item.product.Quantity = Quantity;
                    item.Quantity = Quantity;
                    list.Add(item);

                }
                Session[CommonConstants.CartSession] = list;
            }
            else
            {
                //tạo mới cartItem
                var item = new CartItem();
                item.product = product;
                item.product.Quantity = Quantity;
                item.Quantity = Quantity;
                var list = new List<CartItem>();
                list.Add(item);

                //gán vào Session
                Session[CommonConstants.CartSession] = list;
            }
            return Json(new
            {
                status = true
            });
        }
      
        public ActionResult Success()
        {
            return View();
        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];
            sessionCart.RemoveAll(x => x.product.ID == id);
            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });

        }
        public JsonResult Update(string cartModel)
        {
            var cart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];
            foreach (var item in sessionCart)
            {
                var jsonItem = cart.SingleOrDefault(x => x.product.ID == item.product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                    item.product.Quantity = jsonItem.Quantity;
                }
            }
            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });

        }
        public JsonResult DeleteAll()
        {

            Session[CommonConstants.CartSession] = null;
            return Json(new
            {
                status = true
            });

        }
    }
}