using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TEST.Dtos;
using TEST.Mapper;
using TEST.Models;

namespace TEST.Controllers.Api
{
    public class MoviesController : ApiController
    {
        ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // /api/movies
        [HttpGet]
        public IHttpActionResult GetMovies()
        {
            var movies = _context.Movies.ToList();
            if (!movies.Any())
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            var moviesDto = movies.Select(MapperMgr.Instance.Mapper.Map<Movie, MovieDto>);

            return Ok(moviesDto);
        }

        // /api/movies/1
        [HttpGet]
        public IHttpActionResult GetMovies(int id)
        {
            var movie = _context.Movies.ToList().FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            var movieDto = MapperMgr.Instance.Mapper.Map<Movie, MovieDto>(movie);

            return Ok(movieDto);
        }

        // /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = MapperMgr.Instance.Mapper.Map<MovieDto, Movie>(movieDto);
            movie.AddedDate = DateTime.Now;
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        // /api/movie
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieInDb = _context.Movies.ToList().FirstOrDefault(m => m.Id == id);
            if(movieInDb == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            movieDto.Id = id;
            MapperMgr.Instance.Mapper.Map(movieDto,movieInDb);

            _context.SaveChanges();

            return Ok();
        }

        // /api/movie/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.ToList().FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return Ok();
        }
    }
}
