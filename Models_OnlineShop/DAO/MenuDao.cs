using Models_OnlineShop.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models_OnlineShop.DAO
{
    public class MenuDao
    {
        OnlineShopDbContext db = null;
        public MenuDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<Menu> ListByGroupID(int group)
        {
            return db.Menus.Where(x => x.TypeID == group).OrderBy(x => x.TypeID).ToList();
        }
      
        public IEnumerable<Menu> ListAllPaging(string search,int page,int pageSize)
        {
            IQueryable<Menu> menu = db.Menus;
            if (!string.IsNullOrEmpty(search))
            {
                menu = menu.Where(x => x.Text.Contains(search));
            }
            return menu.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public List<Menu> ListAll()
        {
            return db.Menus.Where(x => x.Status == true).ToList();
        }
        public long Insert(Menu menu)
        {
            db.Menus.Add(menu);
            db.SaveChanges();
            return menu.ID;
        }
        public bool Delete(long id)
        {
            try
            {
                var menu = db.Menus.Find(id);
                db.Menus.Remove(menu);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
         
        }
        public Menu ViewDetail(long id)
        {
            return db.Menus.Find(id);
        }
        public bool Update(Menu entity)
        {
            try
            {
                var menu = db.Menus.Find(entity.ID);
                menu.Text = entity.Text;
                menu.Status = entity.Status;
                menu.Link = entity.Link;
                menu.TypeID = entity.TypeID;
                menu.ParentID = entity.ParentID;
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
            var menu = db.Menus.Find(id);
            if (menu.Status)
            {
                menu.Status = false;
            }
            else
            {
                menu.Status = true;
            }
            db.SaveChanges();
            return menu.Status;
        }
        //public Menu ViewDetailTopDisplay(long typeId)
        //{
        //    return db.Menus.Where(x => x.TypeID == typeId).OrderByDescending(x => x.DisplayOrder).Single(x => x.Status);
        //}
    }
}
