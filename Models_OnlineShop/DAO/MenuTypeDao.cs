using Models_OnlineShop.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models_OnlineShop.DAO
{
    public class MenuTypeDao
    {
        OnlineShopDbContext db = null;
        public MenuTypeDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<MenuType> ListAll()
        {
            return db.MenuTypes.OrderBy(x=>x.ID).ToList();
        }
    }
}
