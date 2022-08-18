using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieRestApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public MoviesController(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _repository = repositoryManager;
            _logger = loggerManager;
        }

        /// <summary>
        /// Get list of all Movies
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var movies = await _repository.MovieRepository.GetAllMovies();
                _logger.LogInfo($"Returned all movies from database.");
                return Ok(movies);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Get All Movies action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get Movie by Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var movie = await _repository.MovieRepository.GetMovieById(id);
                if (movie is null)
                {
                    _logger.LogError($"Movie with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned movie with id: {id}");
                    return Ok(movie);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Get One Movie action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Create a new movie
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Movie movie)
        {
            try
            {
                if (movie == null)
                {
                    _logger.LogError("Movie object sent from client is null.");
                    return BadRequest("Movie object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid movie object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var movieFound = await _repository.MovieRepository.MovieExists(movie.Title);

                if (movieFound)
                {
                    _logger.LogError($"Movie with title \"{movie.Title}\" already exists in db.");
                    ModelState.AddModelError("", "Movie already Exist");
                    return StatusCode(500, ModelState);
                }

                _repository.MovieRepository.CreateMovie(movie);
                var success = await _repository.SaveAsync();

                if (!success)
                {
                    ModelState.AddModelError("", $"Something went wrong while saving movie record of {movie.Title}");
                    return StatusCode(500, ModelState);
                }

                return CreatedAtAction(nameof(GetOne), new { id = movie.Id }, movie);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Post Movie action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        // <summary>
        /// Update a movie
        /// </summary>
        /// <return></return>
        [HttpPut("{movieId:int}")]
        public async Task<IActionResult> Update(int movieId, [FromBody] Movie movie)
        {
            try
            {
                if (movie is null)
                {
                    _logger.LogError("Movie object sent from client is null.");
                    return BadRequest("Movie object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid movie object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var movieExists = await _repository.MovieRepository.MovieExists(movieId);
                if (!movieExists)
                {
                    _logger.LogError($"Movie with id: {movieId}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.MovieRepository.UpdateMovie(movie);
                var success = await _repository.SaveAsync();

                if (!success)
                {
                    ModelState.AddModelError("", $"Something went wrong while updating movie : {movie.Title}");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Update movie action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Update a movie
        /// </summary>
        /// <return></return>
        [HttpDelete("{movieId:int}")]
        public async Task<IActionResult> Delete(int movieId)
        {
            try
            {
                var movieExists = await _repository.MovieRepository.MovieExists(movieId);
                if (!movieExists)
                {
                    _logger.LogError($"Movie with id: {movieId}, hasn't been found in db.");
                    return NotFound();
                }

                var movieObj = await _repository.MovieRepository.GetMovieById(movieId);
                _repository.MovieRepository.DeleteMovie(movieObj!);
                var success = await _repository.SaveAsync();

                if (!success)
                {
                    ModelState.AddModelError("", $"Something went wrong while deleting movie : {movieObj!.Title}");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete movie action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
