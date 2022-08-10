using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using Vidly2.Models;
using Vidly2.ViewModels;
using System.Data.Entity.Validation;

namespace Vidly2.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            //var movies = GetMovies();
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie { Id = 1, Name = "Shrek" },
        //        new Movie { Id = 2, Name = "Wall-e" }
        //    };
        //}

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genres,
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var genres = _context.Genres.ToList();
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            var viewModel = new MovieFormViewModel
            {
                Genres = genres,
                Movie = movie
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel()
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}


// using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Vidly2.Models;
//using Vidly2.ViewModels;

//namespace Vidly2.Controllers
//{
//    public class MoviesController : Controller
//    {

//        public ActionResult Index(int? pageIndex, string sortBy)
//        {
//            var movies = new List<Movie>()
//            {
//                new Movie() { Id = 1, Name = "Shrek" },
//                new Movie() { Id = 2, Name = "Wall-e" }
//            };

//            var viewModel = new MovieViewModel()
//            {
//                Movies = movies
//            };

//            return View(viewModel);
//        }

//        //// GET: Movies
//        //public ActionResult Index(int? pageIndex, string sortBy)
//        //{
//        //    if (!pageIndex.HasValue)
//        //        pageIndex = 1;
//        //    if (string.IsNullOrWhiteSpace(sortBy))
//        //        sortBy = "name";

//        //    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
//        //}

//        // GET: Movies/Random
//        //public ActionResult Random()
//        //{
//        //    /*
//        //     * 
//        //     * Sending the model to the view, means that internally assigns the model object to the property Model of ViewResult.ViewData.Model, as follows 
//        //     *
//        //    var viewResult = new ViewResult();
//        //    viewResult.ViewData.Model;

//        //     */
//        //    var movie = new Movie() { Name = "Shrek!" };
//        //    return View(movie);

//        //    /*
//        //     * Three ways to send data
//        //     * 1. Through ViewData (Default)
//        //     * 2. Using ViewBag (dynamic type), its an improved of ViewData
//        //     * 3. Passing a Model objecto to the view.
//        //    var movie = new Movie() { Name = "Shrek!" };
//        //    ViewData["Movie"] = movie;
//        //    //ViewBag.RandomMovie = movie; //ViewBag is other way to send data.
//        //    return View();*/




//        //    /*
//        //     * Some type of datatype return
//        //     * 
//        //    return Content("Hello world");
//        //    return HttpNotFound();
//        //    return new EmptyResult();
//        //    return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" }); */
//        //}

//        public ActionResult Random()
//        {
//            var movie = new Movie() { Name = "Shrek!" };
//            var customers = new List<Customer>
//            {
//                new Customer { Name = "Customer 1" },
//                new Customer { Name = "Customer 2" }
//            };

//            var viewModel = new RandomMovieViewModel();
//            viewModel.Movie = movie;    
//            viewModel.Customers = customers;

//            return View(viewModel);
//        }

//        public ActionResult Edit(int id)
//        {
//            return Content("id=" + id);
//        }

//        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
//        public ActionResult ByReleaseDate(int year, int month)
//        {
//            return Content(year + "/" + month);
//        }
//    }
//}