using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NevenIvanisic_RWA._21.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        public ActionResult GetAutoCompleteCustomers(string term)
        {
            var podaci = Repo._Get_Customers()
                .Where(k => k.Ime.ToLower().Contains(term) || k.Prezime.ToLower().Contains(term))
                .Select(k => new
                {
                    label = k.ToString(),
                    value = k.IDKupac
                });
            return Json(podaci, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateCustomer(Kupac k)
        {
            if (Repo._UpdateCustomer(k) > 0) return new HttpStatusCodeResult(HttpStatusCode.OK);
            else return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult GetInvoiceByCustomer(int id)
        {
            return Json(Repo._GetInvoiceByCustomer(id), JsonRequestBehavior.AllowGet);
        }
    }
}