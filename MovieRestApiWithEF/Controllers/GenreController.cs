using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRestApiWithEF.API.Filters;
using MovieRestApiWithEF.Core.Models;
using MovieRestApiWithEF.Core.Requests;
using MovieRestApiWithEF.Core.Responses;
using MovieRestApiWithEF.Exceptions;
using MovieRestApiWithEF.Infrastructure;

namespace MovieRestApiWithEF.API.Controllers
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
        public async Task<IActionResult> GetAllGenresAsync()
        {
            // Fetch all models
            var genres = await _repositoryManager.GenreRepository.FindAllAsync();
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
        public async Task<IActionResult> GetOneGenreAsync(int id)
        {
            // Fetch model from db
            var genre = await _repositoryManager.GenreRepository.FindByIdAsync(id);

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
        public async Task<IActionResult> GetOneGenreWithMoviesAsync(int id)
        {
            // Fetch model with nested properties from db
            var genre = await _repositoryManager.GenreRepository.FindGenreMoviesAsync(id);

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

        /// <summary>
        /// Create a new genre
        /// </summary>
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [ServiceFilter(typeof(ValidationFilter))] // Checks exists and validates data from client
        public async Task<IActionResult> PostGenreAsync([FromBody] GenreCreateRequest genreReq)
        {
            // Check if model exists in db
            var genreFound = await _repositoryManager.GenreRepository.ExistsWithNameAsync(genreReq.Name);
            if (genreFound)
            {
                _logger.LogError($"Genre with title \"{genreReq.Name}\" already exists in db.");
                throw new DuplicateEntryException("Genre already exists");
            }

            // Convert Models to Response DTO
            var genre = _mapper.Map<Genre>(genreReq);

            // Create model
            _repositoryManager.GenreRepository.Create(genre);
            await _repositoryManager.SaveAsync();

            // Convert new model to Response DTO
            var genreResponse = _mapper.Map<GenreResponse>(genre);

            // Send response with 201 OK and location of newly created resource and its id
            return CreatedAtAction(nameof(GetOneGenreAsync), new { id = genreResponse.Id }, genreResponse);
        }

        // <summary>
        /// Update a genre
        /// </summary>
        /// <return></return>
        [HttpPut("{genreId:int}")]
        [Authorize(Policy = "AdminOnly")]
        [ServiceFilter(typeof(ValidationFilter))] // Checks exists and validates data from client
        public async Task<IActionResult> UpdateGenreAsync(int genreId, [FromBody] GenreCreateRequest genreReq)
        {
            // Check if model exists in db
            var genreExists = await _repositoryManager.GenreRepository.ExistsWithIdAsync(genreId);
            if (!genreExists)
            {
                _logger.LogError($"Genre with id: {genreId}, hasn't been found in db.");
                throw new NotFoundException("Genre not found");
            }

            // Convert Request DTO to Model
            var genreEntity = _mapper.Map<Genre>(genreReq);
            genreEntity.Id = genreId;

            // Update model
            _repositoryManager.GenreRepository.Update(genreEntity);
            await _repositoryManager.SaveAsync();

            // Send 204 response
            return NoContent();
        }

        /// <summary>
        /// Update a genre
        /// </summary>
        /// <return></return>
        [HttpDelete("{genreId:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteGenreAsync(int genreId)
        {
            // Check if model exists in db
            var genreExists = await _repositoryManager.GenreRepository.ExistsWithIdAsync(genreId);
            if (!genreExists)
            {
                _logger.LogError($"Genre with id: {genreId}, hasn't been found in db.");
                throw new NotFoundException("Genre not found");
            }

            // Delete model
            _repositoryManager.GenreRepository.Delete(genreId);
            await _repositoryManager.SaveAsync();

            // Send 204 response
            return NoContent();
        }
    }
}
