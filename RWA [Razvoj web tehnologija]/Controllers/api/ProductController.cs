using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NevenIvanisic_RWA._21.Controllers.api
{
    public class ProductController : ApiController
    {
        // GET      /api/products    - all products
        // GET      /api/product/1   - product (id=1)
        // POST     /api/products    - add product
        // PUT      /api/products/1  - edit product
        // DELETE   /api/products/1  - delete

        // POST & DELETE 

        [HttpGet]
        public IHttpActionResult Get()
        {
            //var products = Repo._GetProducts();
            //return Ok(products);
            return Json(Repo._GetProducts());
        }

        [HttpPost]
        public IHttpActionResult CreateProduct(Proizvod proizvod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(); // 400
            }

            else
            {
                Repo._InsertProduct(proizvod);
                return Ok(proizvod);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var proizvod = Repo._GetProduct(id);
            if (proizvod == null)
            {
                return NotFound();
            }

            else
            {
                Repo._DeleteProduct(id);
                return Ok("Product successfuly deleted!");
            }
        }
    }
}
