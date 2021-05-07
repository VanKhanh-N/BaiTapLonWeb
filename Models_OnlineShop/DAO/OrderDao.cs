using Models_OnlineShop.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models_OnlineShop.DAO
{
    public class OrderDao
    {
        OnlineShopDbContext db = null;
        public OrderDao()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }

        public float SumPriceByOrderID(long id)
        {
            float price = (float)db.OrderDetails.Where(x=>x.OrderID==id).Sum(x=>x.Price);
            return price;
        }
        public IEnumerable<Order> ListAllPaging(string search, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.ShipName.Contains(search) || x.ShipMobile.Contains(search) || 
                x.ShipAddress.Contains(search) || x.ShipEmail.Contains(search));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public List<Order> ListAll()
        {
            return db.Orders.ToList();
        }

    }
}
