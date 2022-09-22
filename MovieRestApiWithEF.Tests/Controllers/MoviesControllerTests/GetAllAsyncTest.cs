using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.Controllers;
using System.Collections;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MoviesControllerTests
{
    public class GetAllAsyncTest
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly ILoggerManager _stubbedLogger;
        private readonly IMapper _mapper;

        public GetAllAsyncTest()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _stubbedLogger = new Mock<ILoggerManager>().Object;
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = new Mapper(configuration);
        }

        [Fact]
        public async Task GetAllAsync_FindsMovies_Returns200Status()
        {
            /// Arrange

            // Prepare MovieRepository mock
            var mockMovieRepo = new Mock<IMovieRepository>();
            mockMovieRepo.Setup(x => x.FindAllAsync()).ReturnsAsync(MoviesMockData.GetMovies());

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.MovieRepository).Returns(mockMovieRepo.Object);

            // Prepare System under test (sut)
            var sut = new MoviesController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var result = await sut.GetAllAsync();

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllAsync_FindsMovies_ReturnsMoviesWithDetails()
        {
            /// Arrange

            // Prepare MovieRepository mock
            var mockMovieRepo = new Mock<IMovieRepository>();
            mockMovieRepo.Setup(x => x.FindAllAsync()).ReturnsAsync(MoviesMockData.GetMovies());

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.MovieRepository).Returns(mockMovieRepo.Object);

            // Prepare System under test (sut)
            var sut = new MoviesController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var okResult = await sut.GetAllAsync() as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(MoviesMockData.GetMoviesResponse());
        }

        [Fact]
        public async Task GetAllAsync_NoMovies_Returns200Status()
        {
            /// Arrange

            // Prepare MovieRepository mock
            var mockMovieRepo = new Mock<IMovieRepository>();
            mockMovieRepo.Setup(x => x.FindAllAsync()).ReturnsAsync(MoviesMockData.GetEmptyMovies());

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.MovieRepository).Returns(mockMovieRepo.Object);

            // Prepare System under test (sut)
            var sut = new MoviesController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var result = await sut.GetAllAsync();

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllAsync_NoMovies_ReturnsEmptyMovies()
        {
            /// Arrange

            // Prepare MovieRepository mock
            var mockMovieRepo = new Mock<IMovieRepository>();
            mockMovieRepo.Setup(x => x.FindAllAsync()).ReturnsAsync(MoviesMockData.GetEmptyMovies());

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.MovieRepository).Returns(mockMovieRepo.Object);

            // Prepare System under test (sut)
            var sut = new MoviesController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var okResult = await sut.GetAllAsync() as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(MoviesMockData.GetEmptyMoviesResponse());
        }
    }
}
