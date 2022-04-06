using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NevenIvanisic_RWA._21.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.gradovi = Repo._GetUnqTowns();
            return View(Repo._GetCustomers(id));
        }

        [HttpPost]
        public ActionResult Edit(Kupac kupac)
        {
            if (ModelState.IsValid)
            {
                Repo._UpdateCustomer(kupac);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.gradovi = Repo._GetUnqTowns();
                return View(kupac);
            }
        }

    }
}