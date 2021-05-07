using Models_OnlineShop.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class TagController : Controller
    {
        // GET: Tag
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Tags()
        {
            var tags = new TagDao().ListAll();
            return PartialView(tags);
        }
    }
}