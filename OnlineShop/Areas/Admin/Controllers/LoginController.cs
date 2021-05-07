using Models_OnlineShop.DAO;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel Model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(Model.Username,Encrypter.MD5Hash(Model.Password),true);
                if (result==1)
                {
                    var user = dao.getByID(Model.Username);
                    var usersession = new UserLogin();
                    usersession.UserID = user.ID;
                    usersession.UserName = user.UserName;
                    usersession.GroupID = user.GroupID;
                    var listCredentials = dao.GetListCredential(Model.Username);

                    Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredentials);
                    Session.Add(CommonConstants.USER_SESSION,usersession);

                    return RedirectToAction("Index", "Home");
                }
                if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Sai mật khẩu");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Tài khoản không có quyền truy cập");
                }
                else
                {
                    ModelState.AddModelError("","Nhập thông tin chưa đúng");
        
                }               
            }
            return View("Index");
        }
    }
}