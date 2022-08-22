using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Requests;
using Entities.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieRestApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public MoviesController(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = loggerManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of all Movies
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Fetch all movies
                var movies = await _repositoryManager.MovieRepository.GetAllMovies();
                _logger.LogInfo($"Returned all movies from database.");

                // Convert Model to Response DTO
                var moviesResult = _mapper.Map<IEnumerable<MovieWithDetailsResponse>>(movies);
                return Ok(moviesResult);
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
                // Fetch movie
                var movie = await _repositoryManager.MovieRepository.GetMovieById(id);

                // Check if movie not exists
                if (movie is null)
                {
                    _logger.LogError($"Movie with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned movie with id: {id}");

                    // Convert Model to Response DTO
                    var movieResult = _mapper.Map<MovieWithDetailsResponse>(movie);
                    return Ok(movieResult);
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
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Post([FromBody] MovieCreateRequest movieReq)
        {
            try
            {
                // Check empty data from client
                if (movieReq is null)
                {
                    _logger.LogError("Movie object sent from client is null.");
                    return BadRequest("Movie object is null");
                }

                // Validate data from client
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid movie object sent from client.");
                    return BadRequest("Invalid model object");
                }

                // Check if already exists
                var movieFound = await _repositoryManager.MovieRepository.MovieExists(movieReq.Title);
                if (movieFound)
                {
                    _logger.LogError($"Movie with title \"{movieReq.Title}\" already exists in db.");
                    ModelState.AddModelError("", "Movie already Exist");
                    return StatusCode(500, ModelState);
                }

                // Get Nested Models using Nested Ids From DTO
                var cast = await _repositoryManager.MovieWorkerRepository
                    .GetAllMovieWorkers(e => movieReq.CastIds.Contains(e.Id), tracking: true);

                // Convert Request DTO to EFCore Model
                var movie = _mapper.Map<Movie>(movieReq);

                // Save Nested Models into Parent Model
                movie.Cast = (ICollection<MovieWorker>)cast;

                // Create Movie
                _repositoryManager.MovieRepository.CreateMovie(movie);
                await _repositoryManager.SaveAsync();

                // Convert created movie to a Response DTO
                var movieResponse = _mapper.Map<MovieWithDetailsResponse>(movie);

                // Send response along with the location of the newly created resource and its id
                return CreatedAtAction(nameof(GetOne), new { id = movieResponse.Id }, movieResponse);
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
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(int movieId, [FromBody] MovieCreateRequest movieReq)
        {
            try
            {
                // Check empty data from client
                if (movieReq is null)
                {
                    _logger.LogError("Movie object sent from client is null.");
                    return BadRequest("Movie object is null");
                }

                // Validate data from client
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid movie object sent from client.");
                    return BadRequest("Invalid model object");
                }

                // Check if already exists
                var movieExists = await _repositoryManager.MovieRepository.MovieExists(movieId);
                if (!movieExists)
                {
                    _logger.LogError($"Movie with id: {movieId}, hasn't been found in db.");
                    return NotFound();
                }

                // Convert Request DTO to EFCore Model
                var movieEntity = _mapper.Map<Movie>(movieReq);
                movieEntity.Id = movieId;

                // Get Nested Models using Nested Ids From DTO
                var cast = await _repositoryManager.MovieWorkerRepository
                    .GetAllMovieWorkers(e => movieReq.CastIds.Contains(e.Id), tracking: true);

                // Save Nested Models into Parent Model
                movieEntity.Cast = (ICollection<MovieWorker>)cast;

                // Update movie
                _repositoryManager.MovieRepository.UpdateMovie(movieEntity);
                await _repositoryManager.SaveAsync();

                // return 204 response
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
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int movieId)
        {
            try
            {
                // Check if movie exists
                var movieExists = await _repositoryManager.MovieRepository.MovieExists(movieId);
                if (!movieExists)
                {
                    _logger.LogError($"Movie with id: {movieId}, hasn't been found in db.");
                    return NotFound();
                }

                // Delete movie
                _repositoryManager.MovieRepository.DeleteMovie(movieId);
                await _repositoryManager.SaveAsync();

                // Return 204 response
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
