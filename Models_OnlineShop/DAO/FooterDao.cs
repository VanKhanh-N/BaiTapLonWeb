using Models_OnlineShop.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models_OnlineShop.DAO
{
    public class FooterDao
    {
        OnlineShopDbContext db = null;
        public FooterDao()
        {
            db = new OnlineShopDbContext();
        }

        public Footer GetFooter()
        {
            return db.Footers.SingleOrDefault(x => x.Status == true);
        }

        public IEnumerable<Footer> ListAllPaging( int page, int pageSize)
        {
            IQueryable<Footer> model = db.Footers;
            return model.OrderByDescending(x=>x.ID).ToPagedList(page, pageSize);
        }
        public Footer ViewDetail(long id)
        {
            return db.Footers.Find(id);
        }

        public bool Update(Footer entity)
        {
            try
            {
                var about = db.Footers.Find(entity.ID);
                about.Content = entity.Content;
                about.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public long Insert(Footer about)
        {
            db.Footers.Add(about);
            db.SaveChanges();
            return about.ID;
        }

        public bool Delete(long id)
        {
            try
            {
                var about = db.Footers.Find(id);
                db.Footers.Remove(about);
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
            var about = db.Footers.Find(id);
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
