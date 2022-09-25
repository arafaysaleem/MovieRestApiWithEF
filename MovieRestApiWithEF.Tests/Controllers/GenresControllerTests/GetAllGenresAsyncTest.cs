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

namespace MovieRestApiWithEF.Tests.Unit.Controllers.GenresControllerTests
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
        public async Task GetAllGenresAsync_WithoutDetails_HasGenres_Returns200Status()
        {
            /// Arrange

            // Prepare GenreRepository mock
            var mockGenreRepo = new Mock<IGenreRepository>();
            mockGenreRepo.Setup(x => x.FindAllAsync(false)).ReturnsAsync(GenresMockData.GetGenres());

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(mockGenreRepo.Object);

            // Prepare System under test (sut)
            var sut = new GenresController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var result = await sut.GetAllGenresAsync();

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllGenresAsync_WithoutDetails_HasGenres_ReturnsGenres()
        {
            /// Arrange

            // Prepare GenreRepository mock
            var mockGenreRepo = new Mock<IGenreRepository>();
            mockGenreRepo.Setup(x => x.FindAllAsync(false)).ReturnsAsync(GenresMockData.GetGenres());

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(mockGenreRepo.Object);

            // Prepare System under test (sut)
            var sut = new GenresController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var okResult = await sut.GetAllGenresAsync() as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<List<GenreResponse>>();
            okResult!.Value.Should().BeEquivalentTo(GenresMockData.GetGenresResponse());
        }

        [Fact]
        public async Task GetAllGenresAsync_HasNoGenres_Returns200Status()
        {
            /// Arrange

            // Prepare GenreRepository mock
            var mockGenreRepo = new Mock<IGenreRepository>();
            mockGenreRepo.Setup(x => x.FindAllAsync(false)).ReturnsAsync(GenresMockData.GetEmptyGenres());

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(mockGenreRepo.Object);

            // Prepare System under test (sut)
            var sut = new GenresController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var result = await sut.GetAllGenresAsync();

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllGenresAsync_HasNoGenres_ReturnsEmptyGenres()
        {
            /// Arrange

            // Prepare GenreRepository mock
            var mockGenreRepo = new Mock<IGenreRepository>();
            mockGenreRepo.Setup(x => x.FindAllAsync(false)).ReturnsAsync(GenresMockData.GetEmptyGenres());

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(mockGenreRepo.Object);

            // Prepare System under test (sut)
            var sut = new GenresController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var okResult = await sut.GetAllGenresAsync() as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<List<GenreResponse>>();
            okResult!.Value.As<List<GenreResponse>>().Should().BeEmpty();
        }
    }
}
