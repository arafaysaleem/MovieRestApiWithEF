﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.Core.Responses;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MoviesControllerTests
{
    public class GetAllGenresAsyncTest : MoviesControllerTestBase
    {
        [Fact]
        public async Task GetAllMoviesAsync_HasMovies_Returns200Status()
        {
            /// Arrange
            _mockMovieRepository.Setup(x => x.FindAllAsync()).ReturnsAsync(MoviesMockData.AllMovies());

            /// Act
            var result = await _systemUnderTest.GetAllMoviesAsync();

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllMoviesAsync_HasMovies_ReturnsMoviesWithDetails()
        {
            /// Arrange
            _mockMovieRepository.Setup(x => x.FindAllAsync()).ReturnsAsync(MoviesMockData.AllMovies());

            /// Act
            var okResult = await _systemUnderTest.GetAllMoviesAsync() as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<List<MovieWithDetailsResponse>>();
            okResult!.Value.Should().BeEquivalentTo(MoviesMockData.AllMoviesResponse());
        }

        [Fact]
        public async Task GetAllMoviesAsync_HasNoMovies_Returns200Status()
        {
            /// Arrange
            _mockMovieRepository.Setup(x => x.FindAllAsync()).ReturnsAsync(MoviesMockData.EmptyMovies());

            /// Act
            var result = await _systemUnderTest.GetAllMoviesAsync();

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllMoviesAsync_HasNoMovies_ReturnsEmptyMovies()
        {
            /// Arrange
            _mockMovieRepository.Setup(x => x.FindAllAsync()).ReturnsAsync(MoviesMockData.EmptyMovies());

            /// Act
            var okResult = await _systemUnderTest.GetAllMoviesAsync() as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<List<MovieWithDetailsResponse>>();
            okResult!.Value.As<List<MovieWithDetailsResponse>>().Should().BeEmpty();
        }
    }
}
