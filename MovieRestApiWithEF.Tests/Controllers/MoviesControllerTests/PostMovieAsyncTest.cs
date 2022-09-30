using Entities.Models;
using Entities.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.Exceptions;
using System.Linq.Expressions;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MoviesControllerTests
{
    public class PostMovieAsyncTest : MoviesControllerTestBase
    {
        [Fact]
        public async Task PostMovieAsync_DoesNotAlreadyExist_Returns201Status()
        {
            /// Arrange
            var newMovie = MoviesMockData.NewMovieCreateRequest();
            var newMovieCast = MoviesMockData.NewMovieCast();
            var newId = 1;
            _mockMovieWorkerRepository.Setup(x => x.FindAllFromIdsAsync(newMovie.CastIds)).ReturnsAsync(newMovieCast);
            _mockMovieRepository.Setup(x => x.ExistsWithTitleAsync(newMovie.Title)).ReturnsAsync(false);
            _mockMovieRepository.Setup(x => x.Create(It.IsAny<Movie>())).Callback<Movie>((movie) => movie.Id = newId);

            /// Act
            var result = await _systemUnderTest.PostMovieAsync(newMovie);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedAtActionResult>();
            ((CreatedAtActionResult)result).StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task PostMovieAsync_DoesNotAlreadyExist_ReturnsMovieWithNewIdAndCastIds()
        {
            /// Arrange
            var newMovie = MoviesMockData.NewMovieCreateRequest();
            var newMovieCast = MoviesMockData.NewMovieCast();
            var newId = 1;
            _mockMovieWorkerRepository.Setup(x => x.FindAllFromIdsAsync(newMovie.CastIds)).ReturnsAsync(newMovieCast);
            _mockMovieRepository.Setup(x => x.ExistsWithTitleAsync(newMovie.Title)).ReturnsAsync(false);
            _mockMovieRepository.Setup(x => x.Create(It.IsAny<Movie>())).Callback<Movie>((movie) => movie.Id = newId);

            /// Act
            var createdResult = await _systemUnderTest.PostMovieAsync(newMovie) as CreatedAtActionResult;

            /// Assert
            createdResult.Should().NotBeNull();
            createdResult!.Value.Should().BeOfType<MovieWithDetailsResponse>();
            (createdResult.Value as MovieWithDetailsResponse)!.Id.Should().Be(newId);
            (createdResult.Value as MovieWithDetailsResponse)!.Cast.Should().AllSatisfy((x) => newMovie.CastIds.Contains(x.Id));
        }

        [Fact]
        public async Task PostMovieAsync_AlreadyExists_ThrowsDuplicateEntryException()
        {
            /// Arrange
            var newMovie = MoviesMockData.NewMovieCreateRequest();
            _mockMovieRepository.Setup(x => x.ExistsWithTitleAsync(newMovie.Title)).ReturnsAsync(true);

            /// Act
            var act = () => _systemUnderTest.PostMovieAsync(newMovie);

            /// Assert
            await act.Should().ThrowAsync<DuplicateEntryException>().WithMessage("*Movie*exists");
        }
    }
}
