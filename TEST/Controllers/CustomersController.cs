using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEST.Models;
using System.Data.Entity;
using TEST.ViewModels;
using AutoMapper;

namespace TEST.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Route("Customers")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Customers/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.ToList().SingleOrDefault(x => x.Id == id);
            var movies = _context.Movies.ToList();
            if (customer == null || movies == null)
            {
                return new HttpNotFoundResult();
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList(),
                Movies = movies
            };

            return View("CustomerForm", viewModel);
        }


        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var movies = _context.Movies.ToList();


            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes,
                Movies = movies
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if(customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                var config = new MapperConfiguration(cfg => cfg.CreateMap<Customer, Customer>());
                var mapper = new AutoMapper.Mapper(config);
                mapper.Map(customer, customerInDb);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
    }
}