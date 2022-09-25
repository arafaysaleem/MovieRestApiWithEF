using Entities.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.GenresControllerTests
{
    public class GetAllGenresAsyncTest : GenreControllerTestBase
    {
        [Fact]
        public async Task GetAllGenresAsync_WithoutDetails_HasGenres_Returns200Status()
        {
            /// Arrange
            _mockGenreRepository.Setup(x => x.FindAllAsync(false)).ReturnsAsync(GenresMockData.GetGenres());

            /// Act
            var result = await _systemUnderTest.GetAllGenresAsync();

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllGenresAsync_WithoutDetails_HasGenres_ReturnsGenres()
        {
            /// Arrange
            _mockGenreRepository.Setup(x => x.FindAllAsync(false)).ReturnsAsync(GenresMockData.GetGenres());

            /// Act
            var okResult = await _systemUnderTest.GetAllGenresAsync() as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<List<GenreResponse>>();
            okResult!.Value.Should().BeEquivalentTo(GenresMockData.GetGenresResponse());
        }

        [Fact]
        public async Task GetAllGenresAsync_HasNoGenres_Returns200Status()
        {
            /// Arrange
            _mockGenreRepository.Setup(x => x.FindAllAsync(false)).ReturnsAsync(GenresMockData.GetEmptyGenres());

            /// Act
            var result = await _systemUnderTest.GetAllGenresAsync();

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllGenresAsync_HasNoGenres_ReturnsEmptyGenres()
        {
            /// Arrange
            _mockGenreRepository.Setup(x => x.FindAllAsync(false)).ReturnsAsync(GenresMockData.GetEmptyGenres());

            /// Act
            var okResult = await _systemUnderTest.GetAllGenresAsync() as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<List<GenreResponse>>();
            okResult!.Value.As<List<GenreResponse>>().Should().BeEmpty();
        }
    }
}
