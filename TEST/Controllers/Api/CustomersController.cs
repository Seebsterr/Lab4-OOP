using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Http;
using TEST.Dtos;
using TEST.Mapper;
using TEST.Models;

namespace TEST.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // /api/customers
        [HttpGet]
        public IHttpActionResult GetCustomers()
        {
            var customersInDb = _context.Customers.ToList();
            if (!customersInDb.Any())
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            var customersDto = customersInDb.Select(MapperMgr.Instance.Mapper.Map<Customer, CustomerDto>);

            return Ok(customersDto);
        }

        // /api/customers/1
        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            var customerDto = MapperMgr.Instance.Mapper.Map<Customer, CustomerDto>(customer);

            return Ok(customerDto);
        }

        // /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Customer customer = MapperMgr.Instance.Mapper.Map<CustomerDto, Customer>(customerDto);
        
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        // /api/customer
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            customerDto.Id = id;
            MapperMgr.Instance.Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();

            return Ok();
        }

        // /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return Ok();
        }
    }
}
