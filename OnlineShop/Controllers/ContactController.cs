using Models_OnlineShop.DAO;
using Models_OnlineShop.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var contact = new ContactDao().getActivateContact();
            return View(contact);
        }
        public JsonResult Feedback(string feedbackName, string feedbackPhone, string feedbackAddress, string feedbackEmail, string feedbackContent)
        {
            Feedback feedback = new Feedback();
            feedback.Name = feedbackName;
            feedback.Phone = feedbackPhone;
            feedback.Content = feedbackContent;
            feedback.Address = feedbackAddress;
            feedback.Email = feedbackEmail;
            feedback.CreatedDate = DateTime.Now;
            var detail = new ContactDao().insertFeedback(feedback);

            if (detail > 0)
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                }) ;
            }

        }
    }
}