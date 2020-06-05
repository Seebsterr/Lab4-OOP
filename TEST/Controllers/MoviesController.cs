using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEST.Models;
using System.Data.Entity;
using TEST.ViewModels;
using AutoMapper;
using System.Web.ModelBinding;

namespace TEST.Controllers
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

        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genres).ToList();

            return View(movies);
        }

        public ActionResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        [Route("Movies/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Include(m => m.Genres).ToList().SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return new HttpNotFoundResult();
            }

            var viewModel = new MovieFormViewModel();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Movie, MovieFormViewModel>());
            var mapper = new AutoMapper.Mapper(config);
            mapper.Map(movie, viewModel);

            var genres = _context.Genres.ToList();
            viewModel.Genres = genres;

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<Movie, MovieFormViewModel>());
                var mapper = new AutoMapper.Mapper(config);
                mapper.Map(movie, viewModel);

                var genres = _context.Genres.ToList();
                viewModel.Genres = genres;

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.AddedDate = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var moviesInDb = _context.Movies.Single(m => m.Id == movie.Id);

                var config = new MapperConfiguration(cfg => cfg.CreateMap<Movie, Movie>());
                var mapper = new AutoMapper.Mapper(config);
                mapper.Map(movie, moviesInDb);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
}