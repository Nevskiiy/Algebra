using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NevenIvanisic_RWA._21.Controllers
{
    public class SubcategoryController : Controller
    {
        // GET: Subcategory
        public ActionResult Index()
        {
            var model = Repo._GetSubcategories();
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                Repo._DeleteSubcategory(id);
                //return Json(new { message = "Category is succesfully deleted!" }, JsonRequestBehavior.AllowGet);
                return new HttpStatusCodeResult(HttpStatusCode.OK, "Subcategory is succesfully deleted!");

            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Subcategory is not deleted!");
            }

            return View();
        }
        public ActionResult Details(int id)
        {
            var model = Repo._GetSubcategory(id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = Repo._GetSubcategory(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Potkategorija potkategorija)
        {
            try
            {
                Repo._UpdateSubcategory(potkategorija);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }
    }
}