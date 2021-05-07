using Models_OnlineShop.DAO;
using Models_OnlineShop.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class FooterController : BaseController
    {
        // GET: Admin/Footer
        public ActionResult Index( int page = 1, int pageSize = 1)
        {
            var model = new FooterDao().ListAllPaging( page, pageSize);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Footer footer)
        {
            if (ModelState.IsValid)
            {
                long id = new FooterDao().Insert(footer);
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
            var model = new FooterDao().ViewDetail(id);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Footer footer)
        {
            if (ModelState.IsValid)
            {
             
                bool detail = new FooterDao().Update(footer);
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
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new FooterDao().Delete(id);
            return RedirectToAction("Index");
        }

        public JsonResult changeStatus(long id)
        {
            var detail = new FooterDao().changeStatus(id);
            return Json(new
            {
                status = detail
            });
        }
    }
}