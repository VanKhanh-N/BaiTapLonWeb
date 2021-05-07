using Models_OnlineShop.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Models_OnlineShop.DAO
{
    public class AboutDao
    {
        OnlineShopDbContext db = null;
        public AboutDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<About> ListAllPaging(string search, int page, int pageSize)
        {
            IQueryable<About> model = db.Abouts;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.Contains(search) || x.Description.Contains(search));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public About ViewDetail(long id)
        {
            return db.Abouts.Find(id);
        }

        public bool Update(About entity)
        {
            try
            {
                var about = db.Abouts.Find(entity.ID);
                about.Name = entity.Name;
                about.Description = entity.Description;
                about.MetaTiltle = entity.MetaTiltle;
                about.Image = entity.Image;
                about.Detail = entity.Detail;
                about.ModifyBy = entity.ModifyBy;
                about.ModifyDate = entity.ModifyDate;
                about.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public long Insert(About about)
        {
            db.Abouts.Add(about);
            db.SaveChanges();
            return about.ID;
        }

        public bool Delete(long id)
        {
            try
            {
                var about = db.Abouts.Find(id);
                db.Abouts.Remove(about);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool changeStatus(long id)
        {
            var about = db.Abouts.Find(id);
            if (about.Status)
            {
                about.Status = false;
            }
            else about.Status = true;
            db.SaveChanges();
            return about.Status;
        }
    }
}
