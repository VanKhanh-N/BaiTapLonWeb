using Models_OnlineShop.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class FeedbackController : BaseController
    {
        // GET: Admin/Feedback
        public ActionResult Index(string searchString ,int page=1,int pageSize=10)
        {
            ViewBag.searchString = searchString;
            var feedback = new FeedbackDao().ListAllPaging(searchString,page,pageSize);
            return View(feedback);
        }
    }
}