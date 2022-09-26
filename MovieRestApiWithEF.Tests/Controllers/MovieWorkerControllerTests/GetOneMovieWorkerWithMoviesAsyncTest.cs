using Entities.Models;
using Entities.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.Exceptions;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MovieWorkersControllerTests
{
    public class GetOneMovieWorkerWithMoviesAsyncTest : MovieWorkersControllerTestBase
    {
        [Fact]
        public async Task GetOneMovieWorkerWithMoviesAsync_MovieWorkerFound_HasActedMovies_Returns200Status()
        {
            /// Arrange
            var mockMovieWorker = MovieWorkersMockData.SingleMovieWorkerWithActedMovies();
            var genreId = mockMovieWorker.Id;
            _mockMovieWorkerRepository.Setup(x => x.FindMovieWorkerMoviesAsync(genreId)).ReturnsAsync(mockMovieWorker);

            /// Act
            var result = await _systemUnderTest.GetOneMovieWorkerWithMoviesAsync(genreId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneMovieWorkerWithMoviesAsync_MovieWorkerFound_HasActedMovies_ReturnsMovieWorkerWithGivenIdWithActedMovies()
        {
            /// Arrange
            var mockMovieWorker = MovieWorkersMockData.SingleMovieWorkerWithActedMovies();
            var genreId = mockMovieWorker.Id;
            _mockMovieWorkerRepository.Setup(x => x.FindMovieWorkerMoviesAsync(genreId)).ReturnsAsync(mockMovieWorker);

            /// Act
            var okResult = await _systemUnderTest.GetOneMovieWorkerWithMoviesAsync(genreId) as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<MovieWorkerWithDetailsResponse>();
            (okResult.Value as MovieWorkerWithDetailsResponse)!.Id.Should().Be(genreId);
            (okResult.Value as MovieWorkerWithDetailsResponse)!.Should().BeEquivalentTo(MovieWorkersMockData.SingleMovieWorkerWithActedMoviesResponse());
        }

        [Fact]
        public async Task GetOneMovieWorkerWithMoviesAsync_MovieWorkerFound_HasNoActedMovies_Returns200Status()
        {
            /// Arrange
            var mockMovieWorker = MovieWorkersMockData.SingleMovieWorker();
            var genreId = mockMovieWorker.Id;
            _mockMovieWorkerRepository.Setup(x => x.FindMovieWorkerMoviesAsync(genreId)).ReturnsAsync(mockMovieWorker);

            /// Act
            var result = await _systemUnderTest.GetOneMovieWorkerWithMoviesAsync(genreId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneMovieWorkerWithMoviesAsync_MovieWorkerFound_HasNoActedMovies_ReturnsMovieWorkerWithGivenIdWithEmptyActedMovies()
        {
            /// Arrange
            var mockMovieWorker = MovieWorkersMockData.SingleMovieWorker();
            var genreId = mockMovieWorker.Id;
            _mockMovieWorkerRepository.Setup(x => x.FindMovieWorkerMoviesAsync(genreId)).ReturnsAsync(mockMovieWorker);

            /// Act
            var okResult = await _systemUnderTest.GetOneMovieWorkerWithMoviesAsync(genreId) as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<MovieWorkerWithDetailsResponse>();
            var responseBody = (okResult.Value as MovieWorkerWithDetailsResponse)!;
            responseBody.Id.Should().Be(genreId);
            responseBody.ActedMovies.Should().BeEmpty();
        }

        [Fact]
        public async Task GetOneMovieWorkerWithMoviesAsync_MovieWorkerFound_HasDirectedMovies_Returns200Status()
        {
            /// Arrange
            var mockMovieWorker = MovieWorkersMockData.SingleMovieWorkerWithDirectedMovies();
            var genreId = mockMovieWorker.Id;
            _mockMovieWorkerRepository.Setup(x => x.FindMovieWorkerMoviesAsync(genreId)).ReturnsAsync(mockMovieWorker);

            /// Act
            var result = await _systemUnderTest.GetOneMovieWorkerWithMoviesAsync(genreId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneMovieWorkerWithMoviesAsync_MovieWorkerFound_HasDirectedMovies_ReturnsMovieWorkerWithGivenIdWithDirectedMovies()
        {
            /// Arrange
            var mockMovieWorker = MovieWorkersMockData.SingleMovieWorkerWithDirectedMovies();
            var genreId = mockMovieWorker.Id;
            _mockMovieWorkerRepository.Setup(x => x.FindMovieWorkerMoviesAsync(genreId)).ReturnsAsync(mockMovieWorker);

            /// Act
            var okResult = await _systemUnderTest.GetOneMovieWorkerWithMoviesAsync(genreId) as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<MovieWorkerWithDetailsResponse>();
            (okResult.Value as MovieWorkerWithDetailsResponse)!.Id.Should().Be(genreId);
            (okResult.Value as MovieWorkerWithDetailsResponse)!.Should().BeEquivalentTo(MovieWorkersMockData.SingleMovieWorkerWithDirectedMoviesResponse());
        }

        [Fact]
        public async Task GetOneMovieWorkerWithMoviesAsync_MovieWorkerFound_HasNoDirectedMovies_Returns200Status()
        {
            /// Arrange
            var mockMovieWorker = MovieWorkersMockData.SingleMovieWorker();
            var genreId = mockMovieWorker.Id;
            _mockMovieWorkerRepository.Setup(x => x.FindMovieWorkerMoviesAsync(genreId)).ReturnsAsync(mockMovieWorker);

            /// Act
            var result = await _systemUnderTest.GetOneMovieWorkerWithMoviesAsync(genreId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneMovieWorkerWithMoviesAsync_MovieWorkerFound_HasNoDirectedMovies_ReturnsMovieWorkerWithGivenIdWithEmptyDirectedMovies()
        {
            /// Arrange
            var mockMovieWorker = MovieWorkersMockData.SingleMovieWorker();
            var genreId = mockMovieWorker.Id;
            _mockMovieWorkerRepository.Setup(x => x.FindMovieWorkerMoviesAsync(genreId)).ReturnsAsync(mockMovieWorker);

            /// Act
            var okResult = await _systemUnderTest.GetOneMovieWorkerWithMoviesAsync(genreId) as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<MovieWorkerWithDetailsResponse>();
            var responseBody = (okResult.Value as MovieWorkerWithDetailsResponse)!;
            responseBody.Id.Should().Be(genreId);
            responseBody.ActedMovies.Should().BeEmpty();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(5000)]
        public async Task GetOneMovieWorkerWithMoviesAsync_MovieWorkerNotFound_ThrowsNotFoundException(int genreId)
        {
            /// Arrange
            _mockMovieWorkerRepository.Setup(x => x.FindMovieWorkerMoviesAsync(It.IsAny<int>())).ReturnsAsync((MovieWorker?)null);

            /// Act
            var act = () => _systemUnderTest.GetOneMovieWorkerWithMoviesAsync(genreId);

            /// Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Movie worker*");
        }
    }
}
