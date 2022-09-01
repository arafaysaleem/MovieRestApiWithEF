using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Requests;
using Entities.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRestApiWithEF.Exceptions;
using MovieRestApiWithEF.Filters;

namespace MovieRestApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public GenresController(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = loggerManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of all Genres
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Fetch all models
            var genres = await _repositoryManager.GenreRepository.GetAllGenres();
            _logger.LogInfo($"Returned all Genres from database.");

            // Convert Models to Response DTO
            var genresResult = _mapper.Map<IEnumerable<GenreResponse>>(genres);

            // Send 200 OK response
            return Ok(genresResult);
        }

        /// <summary>
        /// Get Genre by Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            // Fetch model from db
            var genre = await _repositoryManager.GenreRepository.GetGenreById(id);

            // Check if model found
            if (genre is null)
            {
                _logger.LogError($"Genre with id: {id}, hasn't been found in db.");
                throw new NotFoundException("Genre not found");
            }
            else
            {
                _logger.LogInfo($"Returned genre with id: {id}");

                // Convert Model to Response DTO
                var genreResult = _mapper.Map<GenreResponse>(genre);

                // Send 200 OK response
                return Ok(genreResult);
            }
        }

        /// <summary>
        /// Get All Movies Of a Genre by Id
        /// </summary>
        [HttpGet("{id}/movies")]
        public async Task<IActionResult> GetOneWithMovies(int id)
        {
            try
            {
                // Fetch model with nested properties from db
                var genre = await _repositoryManager.GenreRepository.GetGenreMovies(id);

                // Check if model exists
                if (genre is null)
                {
                    _logger.LogError($"Genre with id: {id}, hasn't been found in db.");
                    throw new NotFoundException("Genre not found");
                }
                else
                {
                    _logger.LogInfo($"Returned genre with id: {id}");

                    // Convert Model to Response DTO
                    var genreResult = _mapper.Map<GenreWithDetailsResponse>(genre);

                    // Send 200 OK response
                    return Ok(genreResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Get One Genre with Movies action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Create a new genre
        /// </summary>
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [ServiceFilter(typeof(ValidationFilter))] // Checks exists and validates data from client
        public async Task<IActionResult> Post([FromBody] GenreCreateRequest genreReq)
        {
            // Check if model exists in db
            var genreFound = await _repositoryManager.GenreRepository.GenreExists(genreReq.Name);
            if (genreFound)
            {
                _logger.LogError($"Genre with title \"{genreReq.Name}\" already exists in db.");
                throw new DuplicateEntryException("Genre already exists");
            }

            // Convert Models to Response DTO
            var genre = _mapper.Map<Genre>(genreReq);

            // Create model
            var success = await _repositoryManager.GenreRepository.CreateGenre(genre);

            if (!success)
            {
                _logger.LogError($"Genre with title \"{genreReq.Name}\" failed to be inserted.");
                throw new InternalServerException("Genre failed to be created");
            }

            // Convert new model to Response DTO
            var genreResponse = _mapper.Map<GenreResponse>(genre);

            // Send response with 201 OK and location of newly created resource and its id
            return CreatedAtAction(nameof(GetOne), new { id = genreResponse.Id }, genreResponse);
        }

        // <summary>
        /// Update a genre
        /// </summary>
        /// <return></return>
        [HttpPut("{genreId:int}")]
        [Authorize(Policy = "AdminOnly")]
        [ServiceFilter(typeof(ValidationFilter))] // Checks exists and validates data from client
        public async Task<IActionResult> Update(int genreId, [FromBody] GenreCreateRequest genreReq)
        {
            // Convert Request DTO to Model
            var genreEntity = _mapper.Map<Genre>(genreReq);
            genreEntity.Id = genreId;

            // Update model
            var success = await _repositoryManager.GenreRepository.UpdateGenre(genreEntity);

            if (!success)
            {
                _logger.LogError($"Genre with id: {genreId}, hasn't been found in db.");
                throw new NotFoundException("Genre not found");
            }

            // Send 204 response
            return NoContent();
        }

        /// <summary>
        /// Delete a genre
        /// </summary>
        /// <return></return>
        [HttpDelete("{genreId:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int genreId)
        {
            // Delete model
            var success = await _repositoryManager.GenreRepository.DeleteGenre(genreId);
            if (!success)
            {
                _logger.LogError($"Genre with id: {genreId}, hasn't been found in db.");
                throw new NotFoundException("Genre not found");
            }

            // Send 204 response
            return NoContent();
        }
    }
}
