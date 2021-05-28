using Models_OnlineShop.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Models_OnlineShop.ViewModel;

namespace Models_OnlineShop.DAO
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<Product> ListNewProduct(int position, int top)
        {
            return db.Products.Where(x=>x.Status==true).OrderByDescending(x => x.CreatedDate).Skip(position).Take(top).ToList();
        }
        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public IEnumerable<Product> ListByProductCategory(long id, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            return model.Where(x => x.CategoryID == id && x.Status==true).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Product> ListByTag(long id, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            return model.Where(x => x.TagID == id && x.Status==true).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Product> ListByMainMenu(long id, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            return model.Where(x => x.MainMenuID == id && x.Status==true).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public List<Product> ListRelatedProduct(long productID,int top)
        {
            var product = db.Products.Find(productID);
            return db.Products.Where(x => x.ID != productID && x.CategoryID == product.CategoryID && x.Status==true).OrderByDescending(x=>x.CreatedDate).Take(top).ToList();
        }
        /// <summary>
        /// Get List Product By CategoryID
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public List<Product> ListByCategoryID(long categoryID, ref int totalRecord, int page = 1, int pageSize = 9)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            return db.Products.Where(x => x.CategoryID == categoryID && x.Status==true).OrderBy(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
        public IEnumerable<Product> ListAllPaging(string search, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.Contains(search) && x.Status==true); 
                return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            }
            else
            return model.Where(x=>x.Status==true).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public bool changeStatus(long id)
        {
            var product = db.Products.Find(id);
            if (product.Status)
            {
                product.Status = false;
            }
            else product.Status = true;
            db.SaveChanges();
            return product.Status;
        }
        public bool Delete(long id)
        {

            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public long insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool update(Product entity)
        {
            try
            {
                var model = db.Products.Find(entity.ID);
                model.Name = entity.Name;
                model.MetaTiltle = StringHelper.ToUnsignString(entity.Name);
                model.Code = entity.Code;
                model.Description = entity.Description;
                model.Image = entity.Image;
                model.Price = entity.Price;
                model.PromotionPrice = entity.PromotionPrice;
                model.IncludeVAT = entity.IncludeVAT;
                model.Quantity = entity.Quantity;
                model.CategoryID = entity.CategoryID;
                model.Detail = entity.Detail;
                model.Warranty = entity.Warranty;
                model.ModifyDate = DateTime.Now;
                model.ModifyBy = entity.ModifyBy;
                model.MetaKeywords = entity.MetaKeywords;
                model.MetaDescription = entity.MetaDescription;
                model.Status = entity.Status;
                model.ShowOnHome = entity.ShowOnHome;
                model.TopHot = entity.TopHot;
                model.ViewCount = entity.ViewCount;
                model.MainMenuID = entity.MainMenuID;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword) && x.Status==true).Select(x => x.Name).ToList();
        }
        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name.Contains(keyword) && x.Status==true).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.CategoryID equals b.ID
                         where a.Name.Contains(keyword) && a.Status==true
                         select new
                         {
                             CateMetaTitle = b.MetaTiltle,
                             CateName = b.Name,
                             CreatedDate = a.CreatedDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTiltle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price
                         });
          
            List<ProductViewModel> pro =model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return pro;
        }
        public void UpdateImages(long ProductID,string image)
        {
            var product = db.Products.Find(ProductID);
            product.MoreImage = image;
            db.SaveChanges();
        }
    }
}
