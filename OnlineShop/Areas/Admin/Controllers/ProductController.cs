using Models_OnlineShop.DAO;
using Models_OnlineShop.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using OnlineShop.Common;
using CommonConstants = OnlineShop.Common.CommonConstants;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            ViewBag.searchString = searchString;
            var product = new ProductDao().ListAllPaging(searchString, page, pageSize);
            return View(product);
        }
        [HttpGet]
        public ActionResult Create()
        {
            setViewBag();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                product.CreatedDate = DateTime.Now;
                product.MetaTiltle = StringHelper.ToUnsignString(product.Name);
                product.CreatedBy = session.UserID + "";
                product.ShowOnHome = false;
                var dao = new ProductDao();
                long id = dao.insert(product);

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
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDao().Delete(id);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public JsonResult changeStatus(long id)
        {
            var dao = new ProductDao();
            var result = dao.changeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            setViewBag();
            return View(product);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                product.ModifyDate = DateTime.Now;
                product.MetaTiltle = StringHelper.ToUnsignString(product.Name);
                product.ModifyBy = session.UserID + "";
                product.ShowOnHome = false;
                var dao = new ProductDao();
               var detail = dao.update(product);

                if (detail )
                {
                    setAlert("Sửa thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Sửa không thành công");

                }

            }
            setViewBag();
            return View();
        }
        public void setViewBag(long? selectID = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectID);
            var mainmenu = new MenuDao().ListByGroupID(1);
            ViewBag.MainMenuID = new SelectList(mainmenu, "ID", "Text", selectID);
            var tags = new TagDao().ListAll();
            ViewBag.TagID = new SelectList(tags, "ID", "Name", selectID);

        }
        public JsonResult LoadImages(long id)
        {
            ProductDao dao = new ProductDao();
            var product = dao.ViewDetail(id);
            var images = product.MoreImage;
            XElement xImages = XElement.Parse(images);
            List<string> listImagesReturn = new List<string>();

            foreach (XElement element in xImages.Elements())
            {
                listImagesReturn.Add(element.Value);
            }
            return Json(new
            {
                data = listImagesReturn
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveImages(long id, string images)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var listImages = serializer.Deserialize<List<string>>(images);

            XElement xElement = new XElement("Images");

            foreach (var item in listImages)
            {
                var subStringItem = item.Substring(23);
                xElement.Add(new XElement("Image", subStringItem));
            }
            ProductDao dao = new ProductDao();
            try
            {
                dao.UpdateImages(id, xElement.ToString());
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }

        }
    }
}