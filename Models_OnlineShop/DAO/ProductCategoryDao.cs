using Models_OnlineShop.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Common;

namespace Models_OnlineShop.DAO
{
    public class ProductCategoryDao
    {
        OnlineShopDbContext db = null;
        public ProductCategoryDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public IEnumerable<ProductCategory> ListAllPaging(string search,int page,int pageSize)
        {
            IQueryable<ProductCategory> pc = db.ProductCategories;
            if (!string.IsNullOrEmpty(search))
            {
               pc = pc.Where(x => x.Name.Contains(search));
            }
            return pc.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public bool changeStatus(long id)
        {
            var category = db.ProductCategories.Find(id);
            if( category.Status)
            {
                category.Status = false;
            }
            else category.Status = true;
            db.SaveChanges();
            return category.Status;
        }
        public bool Delete(long id)
        {

            try
            {
                var category = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool update(ProductCategory entity)
        {
            try
            {
                var model = db.ProductCategories.Find(entity.ID);
                model.Name = entity.Name;
                model.MetaTiltle = StringHelper.ToUnsignString(entity.Name);
                model.ParentID = entity.ParentID;               
                model.ModifyDate = DateTime.Now;
                model.ModifyBy = entity.ModifyBy;
                model.MetaKeywords = entity.MetaKeywords;
                model.MetaDescription = entity.MetaDescription;
                model.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public long insert(ProductCategory category)
        {
            db.ProductCategories.Add(category);
            db.SaveChanges();
            return category.ID;
        }

    }
}
