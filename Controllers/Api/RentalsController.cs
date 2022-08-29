using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Vidly2.DTOs;
using Vidly2.Models;

namespace Vidly2.Controllers.Api
{
    public class RentalsController : ApiController
    {
        public ApplicationDbContext _context { get; set; }

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }
        
        [HttpPost]
        public IHttpActionResult RentMovie(RentalDTO model)
        {
            var customer = _context.Customers.Single(c => c.Id == model.CustomerId);
            var movies = _context.Movies.Where(m => model.Movies.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");

                if (customer.NumberOfRentedMovies >= 3)
                    return BadRequest("You already have the limit of retned movies");

                movie.NumberAvailable--;
                customer.NumberOfRentedMovies++;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();//We dont use created(), because ask for URL to the new object, and here we arre creating multiple objects.
        }


        //GET /api/rentals?customerId=1
        public IHttpActionResult GetRentalsByCustomerId(int customerId)
        {
            if (customerId == 0)
                return BadRequest();

            var movies = _context.Rentals
                .Include(m => m.Movie)
                .Include(m => m.Customer)
                .Where(m => m.Customer.Id == customerId && m.DateReturned == null)
                .ToList();

            return Ok(JsonConvert.SerializeObject(movies));
        }

        //GET /api/rentals
        [HttpPut]
        public IHttpActionResult ReturnMovie(RentalDTO model)
        {
            //model.Movies are used for the Ids of the rentals
            var rentalsInDb = _context.Rentals.Include(r => r.Movie).Where(r => model.Movies.Contains(r.Id)).ToList();
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == model.CustomerId);

            foreach (var r in rentalsInDb)
            {
                var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == r.Movie.Id);
                r.DateReturned = DateTime.Now;
                movieInDb.NumberAvailable++;
                customerInDb.NumberOfRentedMovies--;
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
