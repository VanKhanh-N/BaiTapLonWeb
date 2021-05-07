using Common;
using Models_OnlineShop.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models_OnlineShop.DAO
{
    public class ContentDao
    {
        OnlineShopDbContext db = null;
        public ContentDao()
        {
            db = new OnlineShopDbContext();
        }
        public Content getByID(long id)
        {
            return db.Contents.Find(id);
        }
        public List<Content> ListNewProduct(int top)
        {
            return db.Contents.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<Content> ListFeatureProduct(int top)
        {
            return db.Contents.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<Content> ListRelatedProduct(long productID)
        {
            var content = db.Contents.Find(productID);
            return db.Contents.Where(x => x.ID != productID && x.CategotyID == content.CategotyID).ToList();
        }
        /// <summary>
        /// Get List Content By CategoryID
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public List<Content> ListByCategoryID(long categoryID, ref int totalRecord, int page = 1, int pageSize = 2)
        {
            totalRecord = db.Contents.Where(x => x.CategotyID == categoryID).Count();
            return db.Contents.Where(x => x.CategotyID == categoryID).OrderBy(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public Content ViewDetail(long id)
        {
            return db.Contents.Find(id);
        }
        public IEnumerable<Content> ListAllPaging(string search, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.Contains(search));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public bool changeStatus(long id)
        {
            var content = db.Contents.Find(id);
            if (content.Status)
            {
                content.Status = false;
            }
            else content.Status = true;
            db.SaveChanges();
            return content.Status;
        }
        public bool Delete(long id)
        {

            try
            {
                var content = db.Contents.Find(id);
                db.Contents.Remove(content);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public long insert(Content entity)
        {
            db.Contents.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool update(Content entity)
        {
            try
            {
                var model = db.Contents.Find(entity.ID);
                model.Name = entity.Name;
                model.MetaTiltle = StringHelper.ToUnsignString(entity.Name);
          
                model.Description = entity.Description;
                model.Image = entity.Image;
                model.CategotyID = entity.CategotyID;
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
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
