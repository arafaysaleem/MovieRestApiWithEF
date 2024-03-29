﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.API.Exceptions;
using MovieRestApiWithEF.Core.Models;
using MovieRestApiWithEF.Core.Responses;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.GenresControllerTests
{
    public class GetOneGenreWithMoviesAsyncTest : GenresControllerTestBase
    {
        [Fact]
        public async Task GetOneGenreWithMoviesAsync_GenreFound_HasMovies_Returns200Status()
        {
            /// Arrange
            var mockGenre = GenresMockData.SingleGenreWithMovies();
            var genreId = mockGenre.Id;
            _mockGenreRepository.Setup(x => x.FindGenreMoviesAsync(genreId)).ReturnsAsync(mockGenre);

            /// Act
            var result = await _systemUnderTest.GetOneGenreWithMoviesAsync(genreId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneGenreWithMoviesAsync_GenreFound_HasMovies_ReturnsGenreWithGivenIdWithMovies()
        {
            /// Arrange
            var mockGenre = GenresMockData.SingleGenreWithMovies();
            var genreId = mockGenre.Id;
            _mockGenreRepository.Setup(x => x.FindGenreMoviesAsync(genreId)).ReturnsAsync(mockGenre);

            /// Act
            var okResult = await _systemUnderTest.GetOneGenreWithMoviesAsync(genreId) as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<GenreWithDetailsResponse>();
            (okResult.Value as GenreWithDetailsResponse)!.Id.Should().Be(genreId);
            (okResult.Value as GenreWithDetailsResponse)!.Should().BeEquivalentTo(GenresMockData.SingleGenreWithMoviesResponse());
        }

        [Fact]
        public async Task GetOneGenreWithMoviesAsync_GenreFound_HasNoMovies_Returns200Status()
        {
            /// Arrange
            var mockGenre = GenresMockData.SingleGenre();
            var genreId = mockGenre.Id;
            _mockGenreRepository.Setup(x => x.FindGenreMoviesAsync(genreId)).ReturnsAsync(mockGenre);

            /// Act
            var result = await _systemUnderTest.GetOneGenreWithMoviesAsync(genreId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneGenreWithMoviesAsync_GenreFound_HasNoMovies_ReturnsGenreWithGivenIdWithoutMovies()
        {
            /// Arrange
            var mockGenre = GenresMockData.SingleGenre();
            var genreId = mockGenre.Id;
            _mockGenreRepository.Setup(x => x.FindGenreMoviesAsync(genreId)).ReturnsAsync(mockGenre);

            /// Act
            var okResult = await _systemUnderTest.GetOneGenreWithMoviesAsync(genreId) as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<GenreWithDetailsResponse>();
            (okResult.Value as GenreWithDetailsResponse)!.Id.Should().Be(genreId);
            (okResult.Value as GenreWithDetailsResponse)!.Should()
                                                         .BeEquivalentTo(GenresMockData.SingleGenreWithEmptyMoviesResponse());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(5000)]
        public async Task GetOneGenreWithMoviesAsync_GenreNotFound_ThrowsNotFoundException(int genreId)
        {
            /// Arrange
            _mockGenreRepository.Setup(x => x.FindGenreMoviesAsync(It.IsAny<int>())).ReturnsAsync((Genre?)null);

            /// Act
            var act = () => _systemUnderTest.GetOneGenreWithMoviesAsync(genreId);

            /// Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Genre*");
        }
    }
}
