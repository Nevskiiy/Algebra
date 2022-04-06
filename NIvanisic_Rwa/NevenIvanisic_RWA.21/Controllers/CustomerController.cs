using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NevenIvanisic_RWA._21.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult CustomerList()
        {
            var model = Repo._GetCustomers(50);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult GetGrad(int id)
        {
            var grad = Repo._GetTown(id);
            return Content(grad.Naziv);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.towns = Repo._GetUnqTowns();
            return View(Repo.GetCustomer(id));
        }        
        
        [HttpPost]
        public ActionResult Edit(Kupac k)
        {
            if (Repo._UpdateCustomer(k) > 0)
            {
                return RedirectToAction("CustomerList");
            }

            else return RedirectToAction("Error");
        }
    }
}