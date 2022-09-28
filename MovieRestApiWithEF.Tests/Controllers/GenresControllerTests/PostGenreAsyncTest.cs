using Entities.Models;
using Entities.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.Exceptions;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.GenresControllerTests
{
    public class PostGenreAsyncTest : GenresControllerTestBase
    {
        [Fact]
        public async Task PostGenreAsync_DoesNotAlreadyExist_Returns201Status()
        {
            /// Arrange
            var newGenre = GenresMockData.NewGenreCreateRequest();
            var newId = 1;
            _mockGenreRepository.Setup(x => x.ExistsWithNameAsync(newGenre.Name)).ReturnsAsync(false);
            _mockGenreRepository.Setup(x => x.Create(It.IsAny<Genre>())).Callback<Genre>((genre) => genre.Id = newId);

            /// Act
            var result = await _systemUnderTest.PostGenreAsync(newGenre);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedAtActionResult>();
            ((CreatedAtActionResult)result).StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task PostGenreAsync_DoesNotAlreadyExist_ReturnsGenreWithNewId()
        {
            /// Arrange
            var newGenre = GenresMockData.NewGenreCreateRequest();
            var newId = 1;
            _mockGenreRepository.Setup(x => x.ExistsWithNameAsync(newGenre.Name)).ReturnsAsync(false);
            _mockGenreRepository.Setup(x => x.Create(It.IsAny<Genre>())).Callback<Genre>((genre) => genre.Id = newId);

            /// Act
            var createdResult = await _systemUnderTest.PostGenreAsync(newGenre) as CreatedAtActionResult;

            /// Assert
            createdResult.Should().NotBeNull();
            createdResult!.Value.Should().BeOfType<GenreResponse>();
            (createdResult.Value as GenreResponse)!.Id.Should().Be(newId);
        }

        [Fact]
        public async Task PostGenreAsync_AlreadyExists_ThrowsDuplicateEntryException()
        {
            /// Arrange
            var newGenre = GenresMockData.NewGenreCreateRequest();
            _mockGenreRepository.Setup(x => x.ExistsWithNameAsync(newGenre.Name)).ReturnsAsync(true);

            /// Act
            var act = () => _systemUnderTest.PostGenreAsync(newGenre);

            /// Assert
            await act.Should().ThrowAsync<DuplicateEntryException>().WithMessage("*Genre*exists");
        }
    }
}
