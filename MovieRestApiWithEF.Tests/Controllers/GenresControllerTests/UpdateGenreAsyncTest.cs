using Entities.Models;
using Entities.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.Exceptions;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.GenresControllerTests
{
    public class UpdateGenreAsyncTest : GenresControllerTestBase
    {
        [Fact]
        public async Task UpdateGenreAsync_GenreFound_Returns204Status()
        {
            /// Arrange
            var updatedGenre = GenresMockData.NewGenreCreateRequest();
            var genreId = GenresMockData.SingleGenre().Id;
            _mockGenreRepository.Setup(x => x.ExistsWithIdAsync(genreId)).ReturnsAsync(true);

            /// Act
            var result = await _systemUnderTest.UpdateGenreAsync(genreId, updatedGenre);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
            ((NoContentResult)result).StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task UpdateGenreAsync_GenreNotFound_ThrowsNotFoundException()
        {
            /// Arrange
            var updatedGenre = GenresMockData.NewGenreCreateRequest();
            var genreId = GenresMockData.SingleGenre().Id;
            _mockGenreRepository.Setup(x => x.ExistsWithIdAsync(genreId)).ReturnsAsync(false);

            /// Act
            var act = () => _systemUnderTest.UpdateGenreAsync(genreId, updatedGenre);

            /// Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Genre*");
        }
    }
}
