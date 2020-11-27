using DTB.DVDCentral.BL;
using DTB.DVDCentral.BL.Models;
using DTB.DVDCentral.MVCUI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTB.DVDCentral.MVCUI.Controllers
{
    public class MovieController : Controller
    {
        List<DTB.DVDCentral.BL.Models.Movie> movies;
        // GET: Movie
        public ActionResult Index()
        {
            movies = MovieManager.Load();
            return View(movies);
        }
        public ActionResult Browse(int id)
        {
            movies = MovieGenreManager.Load(id);
            
            return View(movies);
        }
        public ActionResult Load(int id)
        {
            var movies = MovieManager.Load(id);
            return View("Index", movies);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            DTB.DVDCentral.BL.Models.Movie movie = new DTB.DVDCentral.BL.Models.Movie();
            movie = MovieManager.LoadById(id);
            return View(movie);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            MovieGenresDirectorsRatingsFormats mgdrf = new MovieGenresDirectorsRatingsFormats();
            mgdrf.Movie = new Movie();
            mgdrf.Directors = DirectorManager.Load();
            mgdrf.Formats = FormatManager.Load();
            mgdrf.Ratings = RatingManager.Load();
            mgdrf.Genres = GenreManager.Load();

            return View(mgdrf);
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(MovieGenresDirectorsRatingsFormats mgdrf)
        {
            try
            {
                if(mgdrf.File != null)
                {
                    mgdrf.Movie.ImagePath = mgdrf.File.FileName;
                    string target = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(mgdrf.File.FileName));

                    if (!System.IO.File.Exists(target))
                    {
                        mgdrf.File.SaveAs(target);
                        ViewBag.Message = "File Uploaded Successfully";

                    }
                    else
                    {
                        ViewBag.Message = "File Already Exists";
                    }
                }



                MovieManager.Insert(mgdrf.Movie);
                mgdrf.GenreIds.ToList().ForEach(m => MovieGenreManager.Add(mgdrf.Movie.Id, m));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            MovieGenresDirectorsRatingsFormats mgdrf = new MovieGenresDirectorsRatingsFormats();
            mgdrf.Movie = MovieManager.LoadById(id);
            mgdrf.Ratings = RatingManager.Load();
            mgdrf.Directors = DirectorManager.Load();
            mgdrf.Formats = FormatManager.Load();
            mgdrf.Genres = GenreManager.Load();
            mgdrf.Movie.Genres = GenreManager.Load(id);
            mgdrf.GenreIds = mgdrf.Movie.Genres.Select(a => a.Id);
            Session["genreids"] = mgdrf.GenreIds;
            /*Movie movie = new Movie();
            movie = MovieManager.LoadById(id);*/
            return View(mgdrf);
        }


        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MovieGenresDirectorsRatingsFormats mgdrf)
        {
            try
            {
                if (mgdrf.File != null)
                {
                    mgdrf.Movie.ImagePath = mgdrf.File.FileName;
                    string target = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(mgdrf.File.FileName));

                    if (!System.IO.File.Exists(target))
                    {
                        mgdrf.File.SaveAs(target);
                        ViewBag.Message = "File Uploaded Successfully";

                    }
                    else
                    {
                        ViewBag.Message = "File Already Exists";
                    }
                }


                IEnumerable<int> oldgenreids = new List<int>();
                if (Session["genreids"] != null)
                {
                    oldgenreids = (IEnumerable<int>)Session["genreids"];
                }

                IEnumerable<int> newgenreids = new List<int>();
                if (mgdrf.GenreIds != null)
                {
                    newgenreids = mgdrf.GenreIds;
                }

                // Identify deletes
                IEnumerable<int> deletes = oldgenreids.Except(newgenreids);

                // Identify Adds
                IEnumerable<int> adds = newgenreids.Except(oldgenreids);

                deletes.ToList().ForEach(d => MovieGenreManager.Delete(id, d));
                adds.ToList().ForEach(a => MovieGenreManager.Add(id, a));

                MovieManager.Update(mgdrf.Movie);
                return RedirectToAction("Index");
                /*
                MovieManager.Update(movie);
                return RedirectToAction("Index");
                */
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            Movie movie = new Movie();
            movie = MovieManager.LoadById(id);
            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Movie movie)
        {
            try
            {
                // TODO: Add delete logic here
                MovieManager.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}