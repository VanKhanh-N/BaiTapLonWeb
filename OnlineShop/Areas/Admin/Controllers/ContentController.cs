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
    public class ContentController : BaseController
    {

        // GET: Admin/Content
        public ActionResult Index(string searchString,int page=1,int pageSize=5)
        {
            var model = new ContentDao().ListAllPaging(searchString, page, pageSize);
            ViewBag.CategoryList = new CategoryDao().listAll();
            setViewBag();
            ViewBag.searchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            setViewBag();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content content)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                content.CreatedDate = DateTime.Now;
                content.MetaTiltle = StringHelper.ToUnsignString(content.Name);
                content.CreatedBy = session.UserName;
                var dao = new ContentDao();
                long id = dao.insert(content);

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
            setViewBag();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ContentDao();
            var content = dao.getByID(id);
            setViewBag(content.ID);
            return View(content);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Content content)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                content.ModifyDate = DateTime.Now;
                content.ModifyBy = session.UserName;
                content.MetaTiltle = StringHelper.ToUnsignString(content.Name);
                bool detail = new ContentDao().update(content);
                if (detail)
                {
                    setAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");

                }


            }
            setViewBag(content.ID);
            return View();
        }

        public JsonResult changeStatus(long id)
        {
            var detail = new ContentDao().changeStatus(id);
            return Json(new
            {
                status = detail
            });
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new ContentDao().Delete(id);
            return RedirectToAction("Index");
        }
        public void setViewBag(long? selectID = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategotyID = new SelectList(dao.listAll(), "ID", "Name", selectID);
        }
    }
}