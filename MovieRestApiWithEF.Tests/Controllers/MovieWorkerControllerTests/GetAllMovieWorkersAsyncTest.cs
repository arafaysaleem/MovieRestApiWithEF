using Entities.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MovieWorkersControllerTests
{
    public class GetAllMovieWorkersAsyncTest : MovieWorkersControllerTestBase
    {
        [Fact]
        public async Task GetAllMovieWorkersAsync_WithoutDetails_HasMovieWorkers_Returns200Status()
        {
            /// Arrange
            _mockMovieWorkerRepository.Setup(x => x.FindAllAsync(null, false, false))
                                      .ReturnsAsync(MovieWorkersMockData.AllMovieWorkers());

            /// Act
            var result = await _systemUnderTest.GetAllMovieWorkersAsync();

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllMovieWorkersAsync_WithoutDetails_HasMovieWorkers_ReturnsMovieWorkers()
        {
            /// Arrange
            _mockMovieWorkerRepository.Setup(x => x.FindAllAsync(null, false, false))
                                      .ReturnsAsync(MovieWorkersMockData.AllMovieWorkers());

            /// Act
            var okResult = await _systemUnderTest.GetAllMovieWorkersAsync() as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<List<MovieWorkerResponse>>();
            okResult!.Value.Should().BeEquivalentTo(MovieWorkersMockData.AllMovieWorkersResponse());
        }

        [Fact]
        public async Task GetAllMovieWorkersAsync_WithoutDetails_HasNoMovieWorkers_Returns200Status()
        {
            /// Arrange
            _mockMovieWorkerRepository.Setup(x => x.FindAllAsync(null, false, false))
                                      .ReturnsAsync(MovieWorkersMockData.EmptyMovieWorkers());

            /// Act
            var result = await _systemUnderTest.GetAllMovieWorkersAsync();

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllMovieWorkersAsync_WithoutDetails_HasNoMovieWorkers_ReturnsEmptyMovieWorkers()
        {
            /// Arrange
            _mockMovieWorkerRepository.Setup(x => x.FindAllAsync(null, false, false))
                                      .ReturnsAsync(MovieWorkersMockData.EmptyMovieWorkers());

            /// Act
            var okResult = await _systemUnderTest.GetAllMovieWorkersAsync() as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<List<MovieWorkerResponse>>();
            okResult!.Value.As<List<MovieWorkerResponse>>().Should().BeEmpty();
        }
    }
}
