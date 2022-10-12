using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.API.Exceptions;
using MovieRestApiWithEF.Core.Models;
using MovieRestApiWithEF.Core.Responses;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MovieWorkersControllerTests
{
    public class GetOneMovieWorkerAsyncTest : MovieWorkersControllerTestBase
    {
        [Fact]
        public async Task GetOneMovieWorkerAsync_MovieWorkerFound_Returns200Status()
        {
            /// Arrange
            var mockMovieWorker = MovieWorkersMockData.SingleMovieWorker();
            var genreId = mockMovieWorker.Id;
            _mockMovieWorkerRepository.Setup(x => x.FindByIdAsync(genreId)).ReturnsAsync(mockMovieWorker);

            /// Act
            var result = await _systemUnderTest.GetOneMovieWorkerAsync(genreId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneMovieWorkerAsync_MovieWorkerFound_ReturnsMovieWorkerWithGivenId()
        {
            /// Arrange
            var mockMovieWorker = MovieWorkersMockData.SingleMovieWorker();
            var genreId = mockMovieWorker.Id;
            _mockMovieWorkerRepository.Setup(x => x.FindByIdAsync(genreId)).ReturnsAsync(mockMovieWorker);

            /// Act
            var okResult = await _systemUnderTest.GetOneMovieWorkerAsync(genreId) as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<MovieWorkerResponse>();
            (okResult.Value as MovieWorkerResponse)!.Id.Should().Be(genreId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(5000)]
        public async Task GetOneMovieWorkerAsync_MovieWorkerNotFound_ThrowsNotFoundException(int genreId)
        {
            /// Arrange
            _mockMovieWorkerRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync((MovieWorker?)null);

            /// Act
            var act = () => _systemUnderTest.GetOneMovieWorkerAsync(genreId);

            /// Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Movie worker*");
        }
    }
}
