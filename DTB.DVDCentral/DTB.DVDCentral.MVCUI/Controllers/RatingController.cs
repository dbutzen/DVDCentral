using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTB.DVDCentral.MVCUI.Controllers
{
    public class RatingController : Controller
    {
        List<Rating> ratings;
        // GET: Rating
        public ActionResult Index()
        {
            ratings = RatingManager.Load();
            return View(ratings);
        }

        // GET: Rating/Details/5
        public ActionResult Details(int id)
        {
            Rating rating = new Rating();
            rating = RatingManager.LoadById(id);
            return View(rating);
        }

        // GET: Rating/Create
        public ActionResult Create()
        {
            Rating rating = new Rating();
            return View(rating);
        }

        // POST: Rating/Create
        [HttpPost]
        public ActionResult Create(Rating rating)
        {
            try
            {
                // TODO: Add insert logic here
                RatingManager.Insert(rating);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rating/Edit/5
        public ActionResult Edit(int id)
        {
            Rating rating = new Rating();
            rating = RatingManager.LoadById(id);
            return View(rating);
        }

        // POST: Rating/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Rating rating)
        {
            try
            {
                // TODO: Add update logic here
                RatingManager.Update(rating);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rating/Delete/5
        public ActionResult Delete(int id)
        {
            Rating rating = new Rating();
            rating = RatingManager.LoadById(id);
            return View(rating);
        }

        // POST: Rating/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Rating rating)
        {
            try
            {
                // TODO: Add delete logic here
                RatingManager.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}