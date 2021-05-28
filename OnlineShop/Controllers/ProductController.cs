using Models_OnlineShop.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(long id, int type = 1, int PageNumber = 1, int PageSize = 9)
        {
            if (type == 1)
            {
                var ListProduct = new ProductDao().ListByProductCategory(id, PageNumber, PageSize);
                return View(ListProduct);
            }
            else if(type == 2)
            {
                var ListProduct = new ProductDao().ListByMainMenu(id, PageNumber, PageSize);
                return View(ListProduct);
            }
            else
            {
                var ListProduct = new ProductDao().ListByTag(id, PageNumber, PageSize);
                return View(ListProduct);
            }
    
            
        }
        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            },JsonRequestBehavior.AllowGet); 
        }
        public ActionResult Search(string keyword, int page = 1, int pageSize = 4)
        {       
            int totalRecord = 0;
            var product = new ProductDao().Search(keyword, ref totalRecord, page, pageSize);
            //tổng sán phẩm
            ViewBag.Total = totalRecord;
            //trang được chọn
            ViewBag.Page = page;

            ViewBag.Keyword = keyword;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (totalRecord%pageSize==0)?totalRecord/pageSize:totalRecord/pageSize+1;
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(product);
        }
    
        [ChildActionOnly]
        public ActionResult Seller()
        {
            var ListProduct = new ProductDao().ListAllPaging("", 1, 4);
            return PartialView(ListProduct);
        }
        [ChildActionOnly]
        public ActionResult ProductCategory()
        {
            var productcategory = new ProductCategoryDao().ListAll();
            return PartialView(productcategory);
        }
      
        [ChildActionOnly]
        public ActionResult Tags()
        {

            return PartialView();
        }
       
        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.RelatedProduct = new ProductDao().ListRelatedProduct(id,3);
            if (product.MoreImage != null)
            {
                ViewBag.ListImages = LoadImages(id);
            }
   
            return View(product);
        }
        public List<string> LoadImages(long id)
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
            return listImagesReturn;
        }
    }
}