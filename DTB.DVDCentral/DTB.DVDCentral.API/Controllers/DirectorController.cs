using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;

namespace DTB.DVDCentral.API.Controllers
{
    public class DirectorController : ApiController
    {
        // GET: api/Director
        public IEnumerable<Director> Get()
        {
            List<Director> directors = DirectorManager.Load();
            return directors;
        }

        // GET: api/Director/5
        public Director Get(int id)
        {
            Director director = DirectorManager.LoadById(id);
            return director;
        }

        // POST: api/Director
        public void Post([FromBody]Director director)
        {
            DirectorManager.Insert(director);
        }

        // PUT: api/Director/5
        public void Put(int id, [FromBody]Director director)
        {
            DirectorManager.Update(director);
        }

        // DELETE: api/Director/5
        public void Delete(int id)
        {
            DirectorManager.Delete(id);
        }
    }
}
