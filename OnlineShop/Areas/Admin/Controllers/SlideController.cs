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
    public class SlideController : BaseController
    {
        // GET: Admin/Slide
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            ViewBag.searchString = searchString;
            var slide = new SlideDao().ListAllPaging(searchString, page, pageSize);
            return View(slide);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                slide.CreatedDate = DateTime.Now;
                slide.CreatedBy = session.UserID + "";
                //var model = new SlideDao().ViewDetailTopDisplay();
                //slide.DisplayOrder = model.DisplayOrder + 1;
                if (string.IsNullOrEmpty(slide.Link)) slide.Link = "/";
                long id = new SlideDao().Insert(slide);

                if (id > 0)
                {
                    setAlert("Thêm thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công");

                }

            }
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new SlideDao().Delete(id);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public JsonResult changeStatus(long id)
        {
            var dao = new SlideDao();
            var result = dao.changeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var slide = new SlideDao().ViewDetail(id);
            return View(slide);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                slide.ModifyDate = DateTime.Now;
                slide.ModifyBy = session.UserID + "";
                if (string.IsNullOrEmpty(slide.Link)) slide.Link = "/";
                var dao = new SlideDao();
                var detail = dao.Update(slide);

                if (detail)
                {
                    setAlert("Sửa thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Sửa không thành công");

                }

            }
            return View();
        }
    }
}