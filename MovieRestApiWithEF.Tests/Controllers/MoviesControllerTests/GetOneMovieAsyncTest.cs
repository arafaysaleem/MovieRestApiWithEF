using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.Controllers;
using MovieRestApiWithEF.Exceptions;
using MovieRestApiWithEF.Tests.Unit.Helpers;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MoviesControllerTests
{
    public class GetOneMovieAsyncTest
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly ILoggerManager _stubbedLogger;
        private readonly IMapper _mapper;

        public GetOneMovieAsyncTest()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _stubbedLogger = new Mock<ILoggerManager>().Object;
            _mapper = TestHelpers.GetTestAutoMapper();
        }

        [Fact]
        public async Task GetOneMovieAsync_MovieFound_Returns200Status()
        {
            /// Arrange
            var mockMovie = MoviesMockData.GetMovie();
            var movieId = mockMovie.Id;

            // Prepare MovieRepository mock
            var mockMovieRepo = new Mock<IMovieRepository>();
            mockMovieRepo.Setup(x => x.FindByIdAsync(movieId)).ReturnsAsync(mockMovie);

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.MovieRepository).Returns(mockMovieRepo.Object);

            // Prepare System under test (sut)
            var sut = new MoviesController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var result = await sut.GetOneMovieAsync(movieId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneMovieAsync_MovieFound_ReturnsMovieWithGivenId()
        {
            /// Arrange
            var mockMovie = MoviesMockData.GetMovie();
            var movieId = mockMovie.Id;

            // Prepare MovieRepository mock
            var mockMovieRepo = new Mock<IMovieRepository>();
            mockMovieRepo.Setup(x => x.FindByIdAsync(movieId)).ReturnsAsync(mockMovie);

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.MovieRepository).Returns(mockMovieRepo.Object);

            // Prepare System under test (sut)
            var sut = new MoviesController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var okResult = await sut.GetOneMovieAsync(movieId) as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<MovieWithDetailsResponse>();
            (okResult.Value as MovieWithDetailsResponse)!.Id.Should().Be(movieId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(5000)]
        public async Task GetOneMovieAsync_MovieNotFound_ThrowsNotFoundException(int movieId)
        {
            /// Arrange

            // Prepare MovieRepository mock
            var mockMovieRepo = new Mock<IMovieRepository>();
            mockMovieRepo.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync((Movie?)null);

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.MovieRepository).Returns(mockMovieRepo.Object);

            // Prepare System under test (sut)
            var sut = new MoviesController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var act = () => sut.GetOneMovieAsync(movieId);

            /// Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Movie*");
        }
    }
}
