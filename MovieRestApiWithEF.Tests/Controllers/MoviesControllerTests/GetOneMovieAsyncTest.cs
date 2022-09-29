using Entities.Models;
using Entities.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.Exceptions;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MoviesControllerTests
{
    public class GetOneGenreAsyncTest : MoviesControllerTestBase
    {
        [Fact]
        public async Task GetOneMovieAsync_MovieFound_Returns200Status()
        {
            /// Arrange
            var mockMovie = MoviesMockData.SingleMovie();
            var movieId = mockMovie.Id;
            _mockMovieRepository.Setup(x => x.FindByIdAsync(movieId)).ReturnsAsync(mockMovie);

            /// Act
            var result = await _systemUnderTest.GetOneMovieAsync(movieId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneMovieAsync_MovieFound_ReturnsMovieWithGivenId()
        {
            /// Arrange
            var mockMovie = MoviesMockData.SingleMovie();
            var movieId = mockMovie.Id;
            _mockMovieRepository.Setup(x => x.FindByIdAsync(movieId)).ReturnsAsync(mockMovie);

            /// Act
            var okResult = await _systemUnderTest.GetOneMovieAsync(movieId) as OkObjectResult;

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
            _mockMovieRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync((Movie?)null);

            /// Act
            var act = () => _systemUnderTest.GetOneMovieAsync(movieId);

            /// Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Movie*");
        }
    }
}
