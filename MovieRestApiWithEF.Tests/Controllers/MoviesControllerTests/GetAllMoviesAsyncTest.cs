using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.Controllers;
using MovieRestApiWithEF.Tests.Unit.Helpers;
using System.Collections;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MoviesControllerTests
{
    public class GetAllGenresAsyncTest
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly ILoggerManager _stubbedLogger;
        private readonly IMapper _mapper;

        public GetAllGenresAsyncTest()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _stubbedLogger = new Mock<ILoggerManager>().Object;
            _mapper = TestHelpers.GetTestAutoMapper();
        }

        [Fact]
        public async Task GetAllMoviesAsync_HasMovies_Returns200Status()
        {
            /// Arrange

            // Prepare MovieRepository mock
            var mockMovieRepo = new Mock<IMovieRepository>();
            mockMovieRepo.Setup(x => x.FindAllAsync()).ReturnsAsync(GenresMockData.GetMovies());

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.MovieRepository).Returns(mockMovieRepo.Object);

            // Prepare System under test (sut)
            var sut = new MoviesController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var result = await sut.GetAllMoviesAsync();

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllMoviesAsync_HasMovies_ReturnsMoviesWithDetails()
        {
            /// Arrange

            // Prepare MovieRepository mock
            var mockMovieRepo = new Mock<IMovieRepository>();
            mockMovieRepo.Setup(x => x.FindAllAsync()).ReturnsAsync(GenresMockData.GetMovies());

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.MovieRepository).Returns(mockMovieRepo.Object);

            // Prepare System under test (sut)
            var sut = new MoviesController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var okResult = await sut.GetAllMoviesAsync() as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<List<MovieWithDetailsResponse>>();
            okResult!.Value.Should().BeEquivalentTo(GenresMockData.GetMoviesResponse());
        }

        [Fact]
        public async Task GetAllMoviesAsync_HasNoMovies_Returns200Status()
        {
            /// Arrange

            // Prepare MovieRepository mock
            var mockMovieRepo = new Mock<IMovieRepository>();
            mockMovieRepo.Setup(x => x.FindAllAsync()).ReturnsAsync(GenresMockData.GetEmptyMovies());

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.MovieRepository).Returns(mockMovieRepo.Object);

            // Prepare System under test (sut)
            var sut = new MoviesController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var result = await sut.GetAllMoviesAsync();

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllMoviesAsync_HasNoMovies_ReturnsEmptyMovies()
        {
            /// Arrange

            // Prepare MovieRepository mock
            var mockMovieRepo = new Mock<IMovieRepository>();
            mockMovieRepo.Setup(x => x.FindAllAsync()).ReturnsAsync(GenresMockData.GetEmptyMovies());

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.MovieRepository).Returns(mockMovieRepo.Object);

            // Prepare System under test (sut)
            var sut = new MoviesController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var okResult = await sut.GetAllMoviesAsync() as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<List<MovieWithDetailsResponse>>();
            okResult!.Value.As<List<MovieWithDetailsResponse>>().Should().BeEmpty();
        }
    }
}
