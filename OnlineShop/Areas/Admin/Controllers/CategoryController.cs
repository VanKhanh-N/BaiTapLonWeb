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
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString,int page=1,int pageSize=10)
        {
            ViewBag.searchString = searchString;
            var category = new CategoryDao().ListAllPaging(searchString,page,pageSize);
            setViewBag();
            return View(category);
        }
        [HttpGet]
        
        public ActionResult Create()
        {
            setViewBag();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                category.CreatedDate = DateTime.Now;
                category.CreatedBy = session.UserID+"";
                category.MetaTiltle = StringHelper.ToUnsignString(category.Name);

                long id = new CategoryDao().insert(category);
                if (id > 0)
                {
                    setAlert("Thêm thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    setAlert("Thêm thất bại", "error");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var category = new CategoryDao().ViewDetail(id);
            setViewBag();
            return View(category);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                category.ModifyDate = DateTime.Now;
                category.ModifyBy = session.UserName;
                category.MetaTiltle = StringHelper.ToUnsignString(category.Name);
                
                bool detail = new CategoryDao().update(category);
                if (detail)
                {
                    setAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    setAlert("Cập nhật thất bại", "error");
                }

            }
            return View();

        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new CategoryDao().Delete(id);
            return RedirectToAction("Index");
        }

        public JsonResult changeStatus(long id)
        {
            var detail = new CategoryDao().changeStatus(id);
            return Json(new
            {
                status = detail
            });
        }

        public void setViewBag(long? selectID = null)
        {
            var dao = new CategoryDao();
            var category = new Category();
            category.ID = 0;
            category.Name = "(Trống)";
            List<Category> categoryList = dao.listAll();
            categoryList.Add(category);
            ViewBag.ParentID = new SelectList(categoryList, "ID", "Name", 0);
        }

    }
}