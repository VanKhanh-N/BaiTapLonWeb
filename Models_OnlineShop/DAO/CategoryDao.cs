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
    public class CategoryDao
    {
        OnlineShopDbContext db = null;
        public CategoryDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<Category> listAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }

        public Category ViewDetail(long id)
        {
            return db.Categories.Find(id);
        }
        public IEnumerable<Category> ListAllPaging(string search, int page, int pageSize)
        {
            IQueryable<Category> pc = db.Categories;
            if (!string.IsNullOrEmpty(search))
            {
                pc = pc.Where(x => x.Name.Contains(search));
            }
            return pc.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public bool changeStatus(long id)
        {
            var category = db.Categories.Find(id);
            if (category.Status)
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
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool update(Category entity)
        {
            try
            {
                var model = db.Categories.Find(entity.ID);
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
        public long insert(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }

    }
}
