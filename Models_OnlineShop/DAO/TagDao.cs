using Models_OnlineShop.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models_OnlineShop.DAO
{
    public class TagDao
    {
        OnlineShopDbContext db = null;
        public TagDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<Tag> ListAll()
        {
            return db.Tags.ToList();
        }
        public IEnumerable<Tag> ListAllPaging(string search,int page,int pageSize)
        {
            IQueryable<Tag> model = db.Tags;
            if (!String.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.Name.Contains(search));
            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);

        }
        public Tag ViewDetail(long id)
        {
            return db.Tags.Find(id);
        }
        public bool Update(Tag entity)
        {
            try
            {
                var tag = db.Tags.Find(entity.ID);
                tag.Name = entity.Name;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
         
        }
        public long Insert(Tag entity)
        {
            db.Tags.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Delete(long id)
        {
            try
            {
                var tag = db.Tags.Find(id);
                db.Tags.Remove(tag);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
