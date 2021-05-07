using BotDetect.Web.Mvc;
using Facebook;
using Models_OnlineShop.DAO;
using Models_OnlineShop.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class UserController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                var user = new User();
                user.Email = email;
                user.Status = true;
                user.Name = firstname + " " + middlename + " " + lastname;
                user.UserName = user.Name;
                user.CreatedDate = DateTime.Now;
                var resultInsert = new UserDao().InsertForFacebook(user);
                if (resultInsert > 0)
                {
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                }
            }
            return Redirect("/");
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return RedirectToAction("Login", "User");
        }

        public ActionResult Logout_()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return RedirectToAction("Register", "User");
        }


        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encrypter.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.getByID(model.UserName);
                    var usersession = new UserLogin();
                    usersession.UserID = user.ID;
                    usersession.UserName = user.UserName;
                    Session.Add(CommonConstants.USER_SESSION, usersession);

                    /*  // return RedirectToAction("Index", "Home");*/
                    return Redirect("/");
                }
                if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa");
                }
                if (result == -2)
                {
                    ModelState.AddModelError("", "Sai mật khẩu");
                }
                else
                {
                    ModelState.AddModelError("", "Nhập thông tin chưa đúng");

                }
            }
            return View(model);
        }


        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "registerCaptcha", "Mã xác nhận không đúng!")]
        public ActionResult Register(RegisterModel model)
        {

            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                if (dao.checkUsername(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại.");
                }
                else if (dao.checkEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại.");
                }

                else
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.Name = model.Name;
                    user.PassWord = Encrypter.MD5Hash(model.PassWord);
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;
                    var result = dao.Insert(user);

                    if (result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công.";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ViewBag.KiemTra = "Đã thực hiện đăng ký";
                        MvcCaptcha.ResetCaptcha("registerCaptcha");
                    }
                   
                }
            }
            return View(model);
        }
    }
}