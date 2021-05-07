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
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string searchString,int page=1,int pageSize=10)
        {
            ViewBag.searchString = searchString;
            var model = new ProductCategoryDao().ListAllPaging(searchString, page, pageSize);

            return View(model);
        }
        public JsonResult changeStatus(long id)
        {
            var dao = new ProductCategoryDao();
            var result = dao.changeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductCategoryDao().Delete(id);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var category = new ProductCategoryDao().ViewDetail(id);
            setViewBag();
            return View(category);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductCategory category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedDate = DateTime.Now;
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                category.MetaTiltle = StringHelper.ToUnsignString(category.Name);
                category.CreatedBy = session.UserName;
                var dao = new ProductCategoryDao();
                var detail = dao.update(category);

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
            setViewBag();
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            setViewBag();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProductCategory category)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                category.CreatedDate = DateTime.Now;
                category.CreatedBy = session.UserName;
                category.DisplayOrder = 1;
                category.MetaTiltle = StringHelper.ToUnsignString(category.Name);
                long id = new ProductCategoryDao().insert(category);
                if (id > 0)
                {
                    setAlert("Thêm thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            setViewBag();
            return View();
          


            
        }
        public void setViewBag(long? selectID = null)
        {
            var dao = new ProductCategoryDao();
            var category = new ProductCategory();
            category.ID = 0;
            category.Name = "(Trống)";
            List<ProductCategory> categoryList = dao.ListAll();
            categoryList.Add(category);
            ViewBag.ParentID = new SelectList(categoryList, "ID", "Name", 0); 
        }

    }
}