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

namespace MovieRestApiWithEF.Tests.Unit.Controllers.GenresControllerTests
{
    public class GetOneGenreWithMoviesAsyncTest
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly ILoggerManager _stubbedLogger;
        private readonly IMapper _mapper;

        public GetOneGenreWithMoviesAsyncTest()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _stubbedLogger = new Mock<ILoggerManager>().Object;
            _mapper = TestHelpers.GetTestAutoMapper();
        }

        [Fact]
        public async Task GetOneGenreWithMoviesAsync_GenreFound_HasMovies_Returns200Status()
        {
            /// Arrange
            var mockGenre = GenresMockData.GetGenreWithMovies();
            var genreId = mockGenre.Id;

            // Prepare GenreRepository mock
            var mockGenreRepo = new Mock<IGenreRepository>();
            mockGenreRepo.Setup(x => x.FindGenreMoviesAsync(genreId)).ReturnsAsync(mockGenre);

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(mockGenreRepo.Object);

            // Prepare System under test (sut)
            var sut = new GenresController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var result = await sut.GetOneGenreWithMoviesAsync(genreId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneGenreWithMoviesAsync_GenreFound_HasMovies_ReturnsGenreWithGivenIdWithMovies()
        {
            /// Arrange
            var mockGenre = GenresMockData.GetGenreWithMovies();
            var genreId = mockGenre.Id;

            // Prepare GenreRepository mock
            var mockGenreRepo = new Mock<IGenreRepository>();
            mockGenreRepo.Setup(x => x.FindGenreMoviesAsync(genreId)).ReturnsAsync(mockGenre);

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(mockGenreRepo.Object);

            // Prepare System under test (sut)
            var sut = new GenresController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var okResult = await sut.GetOneGenreWithMoviesAsync(genreId) as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<GenreWithDetailsResponse>();
            (okResult.Value as GenreWithDetailsResponse)!.Id.Should().Be(genreId);
            (okResult.Value as GenreWithDetailsResponse)!.Should().BeEquivalentTo(GenresMockData.GetGenreWithMoviesResponse());
        }

        [Fact]
        public async Task GetOneGenreWithMoviesAsync_GenreFound_HasNoMovies_Returns200Status()
        {
            /// Arrange
            var mockGenre = GenresMockData.GetGenre();
            var genreId = mockGenre.Id;

            // Prepare GenreRepository mock
            var mockGenreRepo = new Mock<IGenreRepository>();
            mockGenreRepo.Setup(x => x.FindGenreMoviesAsync(genreId)).ReturnsAsync(mockGenre);

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(mockGenreRepo.Object);

            // Prepare System under test (sut)
            var sut = new GenresController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var result = await sut.GetOneGenreWithMoviesAsync(genreId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneGenreWithMoviesAsync_GenreFound_HasNoMovies_ReturnsGenreWithGivenIdWithoutMovies()
        {
            /// Arrange
            var mockGenre = GenresMockData.GetGenre();
            var genreId = mockGenre.Id;

            // Prepare GenreRepository mock
            var mockGenreRepo = new Mock<IGenreRepository>();
            mockGenreRepo.Setup(x => x.FindGenreMoviesAsync(genreId)).ReturnsAsync(mockGenre);

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(mockGenreRepo.Object);

            // Prepare System under test (sut)
            var sut = new GenresController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var okResult = await sut.GetOneGenreWithMoviesAsync(genreId) as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<GenreWithDetailsResponse>();
            (okResult.Value as GenreWithDetailsResponse)!.Id.Should().Be(genreId);
            (okResult.Value as GenreWithDetailsResponse)!.Should()
                                                         .BeEquivalentTo(GenresMockData.GetGenreWithEmptyMoviesResponse());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(5000)]
        public async Task GetOneGenreWithMoviesAsync_GenreNotFound_ThrowsNotFoundException(int genreId)
        {
            /// Arrange

            // Prepare GenreRepository mock
            var mockGenreRepo = new Mock<IGenreRepository>();
            mockGenreRepo.Setup(x => x.FindGenreMoviesAsync(It.IsAny<int>())).ReturnsAsync((Genre?)null);

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(mockGenreRepo.Object);

            // Prepare System under test (sut)
            var sut = new GenresController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var act = () => sut.GetOneGenreWithMoviesAsync(genreId);

            /// Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Genre*");
        }
    }
}
