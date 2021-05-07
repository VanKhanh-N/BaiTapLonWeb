using Models_OnlineShop.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models_OnlineShop.DAO
{
    public class FeedbackDao
    {
        OnlineShopDbContext db = null;
        public FeedbackDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<Feedback> ListAllPaging(string search,int page,int pageSize)
        {
            IQueryable<Feedback> feedback = db.Feedbacks;
            if (!string.IsNullOrEmpty(search))
            {
                feedback = feedback.Where(x => x.Content.Contains(search) || x.Email.Contains(search) || x.Phone.Contains(search));
            }
            return feedback.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
    }
}
