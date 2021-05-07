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
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            ViewBag.Menutype = new MenuTypeDao().ListAll();
            ViewBag.ListMenu = new MenuDao().ListAll();
            ViewBag.searchString = searchString;
            var menu = new MenuDao().ListAllPaging(searchString, page, pageSize);
            return View(menu);
        }

        [HttpGet]
        public ActionResult Create()
        {
            setViewBag();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                menu.Link = "/" + StringHelper.ToUnsignString(menu.Text);
                menu.Target = "_self";
                //var model = new MenuDao().ViewDetailTopDisplay((long)menu.TypeID);
                //menu.DisplayOrder = model.DisplayOrder + 1;
                long id = new MenuDao().Insert(menu);
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
            setViewBag();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var menu = new MenuDao().ViewDetail(id);
            setViewBag();
            return View(menu);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                menu.Link = "/" + StringHelper.ToUnsignString(menu.Text);

                bool detail = new MenuDao().Update(menu);
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
            setViewBag();
            return View();
        }

        public JsonResult changeStatus(long id)
        {
            var detail = new MenuDao().changeStatus(id);
            return Json(new
            {
                status = detail
            });
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new MenuDao().Delete(id);
            return RedirectToAction("Index");
        }
        public void setViewBag(long? selectID = null)
        {
            var listMenuType = new MenuTypeDao().ListAll();
            ViewBag.TypeID = new SelectList(listMenuType, "ID", "Name", 0);
            var dao = new MenuDao();
            var menu = new Menu();
            menu.ID = 0;
            menu.Text = "(Trống)";
            List<Menu> menuList = dao.ListAll();
            menuList.Add(menu);
            ViewBag.ParentID = new SelectList(menuList, "ID", "Text", 0);
            //var listMenuParent = new MenuDao();
            //ViewBag.ParentID = new SelectList(listMenuParent, "ID", "Name", 0);
        }
    }
}