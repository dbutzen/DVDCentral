using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;
using DTB.DVDCentral.MVCUI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DTB.DVDCentral.MVCUI.Controllers
{
    public class DirectorController : Controller
    {
        List<Director> directors;
        // GET: Director
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated())
            {
                directors = DirectorManager.Load();
                return View(directors);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Director/Details/5
        public ActionResult Details(int id)
        {
            Director director = new Director();
            director = DirectorManager.LoadById(id);
            return View(director);
        }

        // GET: Director/Create
        public ActionResult Create()
        {
            if (Authenticate.IsAuthenticated())
            {
                Director director = new Director();
                return View(director);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Director/Create
        [HttpPost]
        public ActionResult Create(Director director)
        {
            try
            {
                // TODO: Add insert logic here
                DirectorManager.Insert(director);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Director/Edit/5
        public ActionResult Edit(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Director director = new Director();
                director = DirectorManager.LoadById(id);
                return View(director);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Director/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Director director)
        {
            try
            {
                // TODO: Add update logic here
                DirectorManager.Update(director);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Director/Delete/5
        public ActionResult Delete(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Director director = new Director();
                director = DirectorManager.LoadById(id);
                return View(director);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Director/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Director director)
        {
            try
            {
                // TODO: Add delete logic here
                DirectorManager.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        #region "WebAPI"

        private static HttpClient InitializationClient()
        {
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://localhost:44317/api/");

            client.BaseAddress = new Uri("http://dtbprogdecapi.azurewebsites.net/api/");
            return client;
        }

        public ActionResult Get()
        {
            HttpClient client = InitializationClient();

            // Do the actual call to the WebAPI
            HttpResponseMessage reponse = client.GetAsync("Director").Result;
            //Parse the result
            string result = reponse.Content.ReadAsStringAsync().Result;
            //Parse the result into generic objects
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            //Pase the items into a list of program
            List<Director> directors = items.ToObject<List<Director>>();

            ViewBag.Source = "Get";
            return View("Index", directors);

        }

        public ActionResult GetOne(int id)
        {
            HttpClient client = InitializationClient();

            // Do the actual call to the WebAPI
            HttpResponseMessage reponse = client.GetAsync("Director/" + id).Result;
            //Parse the result
            string result = reponse.Content.ReadAsStringAsync().Result;
            //Parse the result into generic objects
            Director director = JsonConvert.DeserializeObject<Director>(result);

            return View("Details", director);
        }

        public ActionResult Insert()
        {
            HttpClient client = InitializationClient();

            Director director = new Director();
            return View("Create", director);
        }
        [HttpPost]
        public ActionResult Insert(Director director)
        {
            try
            {
                HttpClient client = InitializationClient();
                HttpResponseMessage response = client.PostAsJsonAsync("director", director).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Create", director);
            }

        }

        public ActionResult Update(int id)
        {
            HttpClient client = InitializationClient();


            HttpResponseMessage response = client.GetAsync("Director/" + id).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            Director director = JsonConvert.DeserializeObject<Director>(result);

            return View("Edit", director);
        }

        

        [HttpPost]
        public ActionResult Update(int id, Director director)
        {
            try
            {
                HttpClient client = InitializationClient();
                HttpResponseMessage response = client.PutAsJsonAsync("Director/" + id, director).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Edit", director);
            }

        }

        public ActionResult Remove(int id)
        {
            HttpClient client = InitializationClient();
            HttpResponseMessage response = client.GetAsync("Director/" + id).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            Director director = JsonConvert.DeserializeObject<Director>(result);
            return View("Delete", director);
        }

        [HttpPost]
        public ActionResult Remove(int id, Director director)
        {
            try
            {
                HttpClient client = InitializationClient();
                HttpResponseMessage response = client.DeleteAsync("Director/" + id).Result;
                return RedirectToAction("Get");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Delete", director);
            }

        }

        #endregion

    }
}
