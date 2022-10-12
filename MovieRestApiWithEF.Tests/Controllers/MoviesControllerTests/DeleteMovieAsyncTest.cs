using Entities.Models;
using Entities.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.API.Exceptions;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MoviesControllerTests
{
    public class DeleteMovieAsyncTest : MoviesControllerTestBase
    {
        [Fact]
        public async Task DeleteMovieAsync_MovieFound_Returns204Status()
        {
            /// Arrange
            var movieId = MoviesMockData.SingleMovie().Id;
            _mockMovieRepository.Setup(x => x.ExistsWithIdAsync(movieId)).ReturnsAsync(true);

            /// Act
            var result = await _systemUnderTest.DeleteMovieAsync(movieId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
            ((NoContentResult)result).StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteMovieAsync_MovieNotFound_ThrowsNotFoundException()
        {
            /// Arrange
            var movieId = MoviesMockData.SingleMovie().Id;
            _mockMovieRepository.Setup(x => x.ExistsWithIdAsync(movieId)).ReturnsAsync(false);

            /// Act
            var act = () => _systemUnderTest.DeleteMovieAsync(movieId);

            /// Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Movie*");
        }
    }
}
