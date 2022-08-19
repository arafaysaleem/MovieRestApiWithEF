using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.RequestDtos;
using Entities.ResponseDtos;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                // Fetch all models
                var genres = await _repositoryManager.GenreRepository.GetAllGenres();
                _logger.LogInfo($"Returned all Genres from database.");

                // Convert Models to Response DTO
                var genresResult = _mapper.Map<IEnumerable<GenreDto>>(genres);
                
                // Send 200 OK response
                return Ok(genresResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Get All Genres action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get Genre by Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                // Fetch model from db
                var genre = await _repositoryManager.GenreRepository.GetGenreById(id);
                
                // Check if model found
                if (genre is null)
                {
                    _logger.LogError($"Genre with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned genre with id: {id}");

                    // Convert Model to Response DTO
                    var genreResult = _mapper.Map<GenreDto>(genre);
                    
                    // Send 200 OK response
                    return Ok(genreResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Get One Genre action: {ex.Message}");
                return StatusCode(500, "Internal server error");
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
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned genre with id: {id}");

                    // Convert Model to Response DTO
                    var genreResult = _mapper.Map<GenreWithDetailsDto>(genre);
                    
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
        public async Task<IActionResult> Post([FromBody] GenreCreateDto genre)
        {
            try
            {
                // Check if data from client is missing
                if (genre is null)
                {
                    _logger.LogError("Genre object sent from client is null.");
                    return BadRequest("Genre object is null");
                }

                // Validate data from client
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid genre object sent from client.");
                    return BadRequest("Invalid model object");
                }

                // Check if model exists in db
                var genreFound = await _repositoryManager.GenreRepository.GenreExists(genre.Name);
                if (genreFound)
                {
                    _logger.LogError($"Genre with title \"{genre.Name}\" already exists in db.");
                    ModelState.AddModelError("", "Genre already Exist");
                    return StatusCode(500, ModelState);
                }

                // Convert Models to Response DTO
                var genreEntity = _mapper.Map<Genre>(genre);
                
                // Create model
                _repositoryManager.GenreRepository.CreateGenre(genreEntity);
                await _repositoryManager.SaveAsync();

                // Convert new model to Response DTO
                var createdGenre = _mapper.Map<GenreDto>(genreEntity);

                // Send response with 201 OK and location of newly created resource and its id
                return CreatedAtAction(nameof(GetOne), new { id = createdGenre.Id }, createdGenre);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Post Genre action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        // <summary>
        /// Update a genre
        /// </summary>
        /// <return></return>
        [HttpPut("{genreId:int}")]
        public async Task<IActionResult> Update(int genreId, [FromBody] GenreCreateDto genre)
        {
            try
            {
                // Check if data from client is missing
                if (genre is null)
                {
                    _logger.LogError("Genre object sent from client is null.");
                    return BadRequest("Genre object is null");
                }

                // Validate data from client
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid genre object sent from client.");
                    return BadRequest("Invalid model object");
                }

                // Check if model exists in db
                var genreExists = await _repositoryManager.GenreRepository.GenreExists(genreId);
                if (!genreExists)
                {
                    _logger.LogError($"Genre with id: {genreId}, hasn't been found in db.");
                    return NotFound();
                }

                // Convert Request DTO to Model
                var genreEntity = _mapper.Map<Genre>(genre);
                
                // Update model
                _repositoryManager.GenreRepository.UpdateGenre(genreEntity);
                await _repositoryManager.SaveAsync();

                // Send 204 response
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Update genre action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Update a genre
        /// </summary>
        /// <return></return>
        [HttpDelete("{genreId:int}")]
        public async Task<IActionResult> Delete(int genreId)
        {
            try
            {
                // Check if model exists in db
                var genreExists = await _repositoryManager.GenreRepository.GenreExists(genreId);
                if (!genreExists)
                {
                    _logger.LogError($"Genre with id: {genreId}, hasn't been found in db.");
                    return NotFound();
                }

                // Delete model
                _repositoryManager.GenreRepository.DeleteGenre(genreId);
                await _repositoryManager.SaveAsync();
                
                // Send 204 response
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Delete genre action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
