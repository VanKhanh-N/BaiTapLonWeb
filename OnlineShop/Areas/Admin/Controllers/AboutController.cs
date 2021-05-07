
using Common;
using Models_OnlineShop.DAO;
using Models_OnlineShop.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonConstants = OnlineShop.Common.CommonConstants;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class AboutController : BaseController
    {
        // GET: Admin/About
        public ActionResult Index(string searchString,int page=1,int pageSize=5)
        {
            ViewBag.searchString = searchString;
            var model = new AboutDao().ListAllPaging(searchString, page, pageSize);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(About about)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                about.CreatedBy = session.UserName;
                about.CreatedDate = DateTime.Now;
                about.MetaTiltle = StringHelper.ToUnsignString(about.Name);
                long id = new AboutDao().Insert(about);
                if (id > 0)
                {
                    setAlert("Thêm thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    setAlert("Thêm không thành công", "error");
                }

            }
            return View();
        }

        [HttpGet] 
        public ActionResult Edit(long id)
        {
            var about = new AboutDao().ViewDetail(id);
            return View(about);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(About about)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                about.ModifyBy = session.UserName;
                about.ModifyDate = DateTime.Now;
                about.MetaTiltle = StringHelper.ToUnsignString(about.Name);
                bool detail = new AboutDao().Update(about);
                if (detail)
                {
                    setAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    setAlert("Cập nhật không thành công", "error");
                }
            }
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new AboutDao().Delete(id);
            return RedirectToAction("Index");
        }

        public JsonResult changeStatus(long id)
        {
            var detail = new AboutDao().changeStatus(id);
            return Json(new
            {
                status = detail
            });
        }
    }
}