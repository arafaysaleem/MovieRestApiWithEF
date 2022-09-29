using Entities.Models;
using Entities.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.Exceptions;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MovieWorkersControllerTests
{
    public class DeleteMovieWorkerAsyncTest : MovieWorkersControllerTestBase
    {
        [Fact]
        public async Task DeleteMovieWorkerAsync_MovieWorkerFound_Returns204Status()
        {
            /// Arrange
            var genreId = MovieWorkersMockData.SingleMovieWorker().Id;
            _mockMovieWorkerRepository.Setup(x => x.ExistsWithIdAsync(genreId)).ReturnsAsync(true);

            /// Act
            var result = await _systemUnderTest.DeleteMovieWorkerAsync(genreId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
            ((NoContentResult)result).StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteMovieWorkerAsync_MovieWorkerNotFound_ThrowsNotFoundException()
        {
            /// Arrange
            var genreId = MovieWorkersMockData.SingleMovieWorker().Id;
            _mockMovieWorkerRepository.Setup(x => x.ExistsWithIdAsync(genreId)).ReturnsAsync(false);

            /// Act
            var act = () => _systemUnderTest.DeleteMovieWorkerAsync(genreId);

            /// Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Movie worker*");
        }
    }
}
