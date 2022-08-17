using Microsoft.AspNetCore.Mvc;
using MovieRestApiWithEF.Models;
using MovieRestApiWithEF.Repositories;

namespace MovieRestApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepo;
        public MoviesController(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        /// <summary>
        /// Get list of all Movies
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var movies = await _movieRepo.GetAllMovies();
            return Ok(movies);
        }

        /// <summary>
        /// Get Movie by Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var movie = await _movieRepo.GetMovieById(id);
            return movie != null ? Ok(movie) : NotFound();
        }

        /// <summary>
        /// Create a new movie
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Movie movie)
        {
            if (movie == null)
            {
                return BadRequest(ModelState);
            }

            var movieFound = await _movieRepo.MovieExists(movie.Id);

            if (movieFound)
            {
                ModelState.AddModelError("", "Movie already Exist");
                return StatusCode(500, ModelState);
            }

            var createdMovie = await _movieRepo.CreateMovie(movie);
            if (!createdMovie)
            {
                ModelState.AddModelError("", $"Something went wrong while saving movie record of {movie.Title}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);

        }

        // <summary>
        /// Update a movie
        /// </summary>
        /// <return></return>
        [HttpPut("{movieId:int}")]
        public async Task<IActionResult> Update(int movieId, [FromBody] Movie movie)
        {
            if (movie == null || movieId != movie.Id)
            {
                return BadRequest(ModelState);
            }
            else if (!await _movieRepo.MovieExists(movieId))
            {
                return NotFound();
            }

            if (!await _movieRepo.UpdateMovie(movie))
            {
                ModelState.AddModelError("", $"Something went wrong while updating movie : {movie.Title}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Update a movie
        /// </summary>
        /// <return></return>
        [HttpDelete("{movieId:int}")]
        public async Task<IActionResult> Delete(int movieId)
        {
            if (!await _movieRepo.MovieExists(movieId))
            {
                return NotFound();
            }

            var movieObj = await _movieRepo.GetMovieById(movieId);

            if (!await _movieRepo.DeleteMovie(movieObj!))
            {
                ModelState.AddModelError("", $"Something went wrong while deleting movie : {movieObj!.Title}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
