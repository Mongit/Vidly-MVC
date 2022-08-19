using System;
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

                movie.NumberAvailable--;

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
    }
}
