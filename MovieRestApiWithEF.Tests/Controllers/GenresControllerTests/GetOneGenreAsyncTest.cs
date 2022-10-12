using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.API.Exceptions;
using MovieRestApiWithEF.Core.Models;
using MovieRestApiWithEF.Core.Responses;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.GenresControllerTests
{
    public class GetOneGenreAsyncTest : GenresControllerTestBase
    {
        [Fact]
        public async Task GetOneGenreAsync_GenreFound_Returns200Status()
        {
            /// Arrange
            var mockGenre = GenresMockData.SingleGenre();
            var genreId = mockGenre.Id;
            _mockGenreRepository.Setup(x => x.FindByIdAsync(genreId)).ReturnsAsync(mockGenre);

            /// Act
            var result = await _systemUnderTest.GetOneGenreAsync(genreId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneGenreAsync_GenreFound_ReturnsGenreWithGivenId()
        {
            /// Arrange
            var mockGenre = GenresMockData.SingleGenre();
            var genreId = mockGenre.Id;
            _mockGenreRepository.Setup(x => x.FindByIdAsync(genreId)).ReturnsAsync(mockGenre);

            /// Act
            var okResult = await _systemUnderTest.GetOneGenreAsync(genreId) as OkObjectResult;

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
            _mockGenreRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync((Genre?)null);

            /// Act
            var act = () => _systemUnderTest.GetOneGenreAsync(genreId);

            /// Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Genre*");
        }
    }
}
