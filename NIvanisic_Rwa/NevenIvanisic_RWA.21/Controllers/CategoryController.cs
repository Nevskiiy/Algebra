using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;

namespace NevenIvanisic_RWA._21.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            var model = Repo._GetCategories();
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                Repo._DeleteCategory(id);
                //return Json(new { message = "Category is succesfully deleted!" }, JsonRequestBehavior.AllowGet);
                return new HttpStatusCodeResult(HttpStatusCode.OK, "Category is succesfully deleted!");

            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Category is not deleted!");
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            var model = Repo._GetCategory(id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = Repo._GetCategory(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Kategorija kategorija)
        {
            try
            {
                Repo._UpdateCategory(kategorija);
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