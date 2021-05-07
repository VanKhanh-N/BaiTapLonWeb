using Models_OnlineShop.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models_OnlineShop.DAO
{
    public class SlideDao
    {
        OnlineShopDbContext db = null;
        public SlideDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<Slide> ListAll()
        {
            return db.Slides.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public IEnumerable<Slide> ListAllPaging(string search,int page,int pageSize)
        {
            IQueryable<Slide> model = db.Slides;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Description.Contains(search));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public long Insert(Slide slide)
        {
            db.Slides.Add(slide);
            db.SaveChanges();
            return slide.ID;
        }
        public Slide ViewDetail(long id)
        {
            return db.Slides.Find(id);
        }
     
        public bool changeStatus(long id)
        {
            var slide = db.Slides.Find(id);
            if (slide.Status)
            {
                slide.Status = false;
            }
            else slide.Status = true;
            db.SaveChanges();
            return slide.Status;
        }
        public bool Delete(long id)
        {

            try
            {
                var slide = db.Slides.Find(id);
                db.Slides.Remove(slide);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
  
        public bool Update(Slide entity)
        {
            try
            {
                var model = db.Slides.Find(entity.ID);
                model.Image = entity.Image;
                model.ModifyBy = entity.Link;
                model.ModifyBy = entity.ModifyBy;
                model.ModifyDate = entity.ModifyDate;
                model.Description = entity.Description;
                model.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //public Slide ViewDetailTopDisplay()
        //{
        //    return db.Slides.OrderByDescending(x => x.DisplayOrder).SingleOrDefault();
        //}
    }
}
