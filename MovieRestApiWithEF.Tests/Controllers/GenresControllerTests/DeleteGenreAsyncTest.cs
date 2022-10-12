using Entities.Models;
using Entities.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.API.Exceptions;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.GenresControllerTests
{
    public class DeleteGenreAsyncTest : GenresControllerTestBase
    {
        [Fact]
        public async Task DeleteGenreAsync_GenreFound_Returns204Status()
        {
            /// Arrange
            var genreId = GenresMockData.SingleGenre().Id;
            _mockGenreRepository.Setup(x => x.ExistsWithIdAsync(genreId)).ReturnsAsync(true);

            /// Act
            var result = await _systemUnderTest.DeleteGenreAsync(genreId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
            ((NoContentResult)result).StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteGenreAsync_GenreNotFound_ThrowsNotFoundException()
        {
            /// Arrange
            var genreId = GenresMockData.SingleGenre().Id;
            _mockGenreRepository.Setup(x => x.ExistsWithIdAsync(genreId)).ReturnsAsync(false);

            /// Act
            var act = () => _systemUnderTest.DeleteGenreAsync(genreId);

            /// Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Genre*");
        }
    }
}
