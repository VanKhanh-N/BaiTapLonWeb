    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models_OnlineShop.DAO;
using Models_OnlineShop.EF;
using OnlineShop.Common;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        //[HasCredential(RoleID ="VIEW_USER")]
        public ActionResult Index(string searchString,int page = 1, int pageSize = 5)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString,page, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }
        [HttpGet]
        //[HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        //[HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int id)
        {
            var user = new UserDao().viewDetalt(id);
            return View(user);
        }
        [HttpPost]
        //[HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var EncryptMD5Pas = Encrypter.MD5Hash(user.PassWord);
                user.PassWord = EncryptMD5Pas;
                user.CreatedDate = DateTime.Now;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    setAlert("Thêm thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công");
                }

            }
            return View("Index");

        }
        [HttpPost]
        //[HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!string.IsNullOrEmpty(user.PassWord))
                {
                    var EncryptMD5Pas = Encrypter.MD5Hash(user.PassWord);
                    user.PassWord = EncryptMD5Pas;
                }
                var result = dao.update(user);
                if (result)
                {
                    setAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }

            }
            return View("Index");

        }
        [HttpDelete]
        //[HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");

        }

        [HttpPost]
        //[HasCredential(RoleID = "EDIT_USER")]
        public JsonResult changeStatus(long id)
        {
            var dao = new UserDao();
            var result = dao.changeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}