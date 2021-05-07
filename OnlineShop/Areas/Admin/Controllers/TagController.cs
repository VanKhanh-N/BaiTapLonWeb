using Common;
using Models_OnlineShop.DAO;
using Models_OnlineShop.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class TagController : BaseController
    {
        // GET: Admin/Tag
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            ViewBag.searchString = searchString;
            var tag = new TagDao().ListAllPaging(searchString, page, pageSize);
            return View(tag);
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
               
           
                long id = new TagDao().Insert(tag);
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
            var tag = new TagDao().ViewDetail(id);
            return View(tag);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Tag tag)
        {
            if (ModelState.IsValid)
            {
               

                bool detail = new TagDao().Update(tag);
                if (ModelState.IsValid)
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
            new TagDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}