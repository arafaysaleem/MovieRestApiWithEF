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
    public class GetOneGenreAsyncTest
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly ILoggerManager _stubbedLogger;
        private readonly IMapper _mapper;

        public GetOneGenreAsyncTest()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _stubbedLogger = new Mock<ILoggerManager>().Object;
            _mapper = TestHelpers.GetTestAutoMapper();
        }

        [Fact]
        public async Task GetOneGenreAsync_GenreFound_Returns200Status()
        {
            /// Arrange
            var mockGenre = GenresMockData.GetGenre();
            var genreId = mockGenre.Id;

            // Prepare GenreRepository mock
            var mockGenreRepo = new Mock<IGenreRepository>();
            mockGenreRepo.Setup(x => x.FindByIdAsync(genreId)).ReturnsAsync(mockGenre);

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(mockGenreRepo.Object);

            // Prepare System under test (sut)
            var sut = new GenresController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var result = await sut.GetOneGenreAsync(genreId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneGenreAsync_GenreFound_ReturnsGenreWithGivenId()
        {
            /// Arrange
            var mockGenre = GenresMockData.GetGenre();
            var genreId = mockGenre.Id;

            // Prepare GenreRepository mock
            var mockGenreRepo = new Mock<IGenreRepository>();
            mockGenreRepo.Setup(x => x.FindByIdAsync(genreId)).ReturnsAsync(mockGenre);

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(mockGenreRepo.Object);

            // Prepare System under test (sut)
            var sut = new GenresController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var okResult = await sut.GetOneGenreAsync(genreId) as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<GenreResponse>();
            (okResult.Value as GenreResponse)!.Id.Should().Be(genreId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(5000)]
        public async Task GetOneGenreAsync_GenreNotFound_ThrowsNotFoundException(int genreId)
        {
            /// Arrange

            // Prepare GenreRepository mock
            var mockGenreRepo = new Mock<IGenreRepository>();
            mockGenreRepo.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync((Genre?)null);

            // Prepare IoC container mock
            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(mockGenreRepo.Object);

            // Prepare System under test (sut)
            var sut = new GenresController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);

            /// Act
            var act = () => sut.GetOneGenreAsync(genreId);

            /// Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Genre*");
        }
    }
}
