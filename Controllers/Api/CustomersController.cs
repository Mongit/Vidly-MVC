using AutoMapper;
using System;
using System.Linq;
using System.Web.Http;
using Vidly2.DTOs;
using Vidly2.Models;
using System.Data.Entity;

namespace Vidly2.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = _context.Customers
                .Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query) && c.IsDelinquentOnPayment == false);

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDTO>);
            return Ok(customerDtos);
        }


        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDTO>(customer));
        }

        // POST /api/customers
        [HttpPost] //This attribute Makes this action will ONLY be called if we send data in the req.body of the post.request
        public IHttpActionResult CreateCustomer(CustomerDTO customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDTO, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        // PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult Updtecustomer(int id, CustomerDTO customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerDto == null)
                NotFound();

            //if you have an existing object you can pass it as an argument.
            //We pass customerInDb, so _context can track all changes to this object 
            //We can get rid of generics parameters, because the compiler infers from the parameters objects, which is the source and the target
            //Mapper.Map<CustomerDTO, Customer>(customerDto, customerInDb);
            Mapper.Map(customerDto, customerInDb);
            
            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }


        // PUT /api/customers?customerId=1
        [HttpPut]
        public IHttpActionResult UpdateStatusPayment(int customerId)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == customerId);

            if (customerInDb == null)
                NotFound();

            customerInDb.IsDelinquentOnPayment = !customerInDb.IsDelinquentOnPayment;

            _context.SaveChanges();

            return Ok();
        }
    }
}
