using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NevenIvanisic_RWA._21.Controllers.api
{
    public class TownController : ApiController
    {
        // GET: api/Town
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(Repo._GetUnqTowns());
        }

        // GET: api/Town/5
        public IHttpActionResult Get(int id)
        {
            return Json(Repo._GetTown(id));
        }

        // POST: api/Town
        public void Post([FromBody]Grad value)
        {
        }

        // PUT: api/Town/5
        public void Put(int id, [FromBody]Grad value)
        {
        }

        // DELETE: api/Town/5
        public void Delete(int id)
        {
        }
    }
}
