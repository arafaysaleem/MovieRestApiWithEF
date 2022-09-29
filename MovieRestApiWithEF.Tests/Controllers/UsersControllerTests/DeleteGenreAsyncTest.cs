using Entities.Models;
using Entities.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.Exceptions;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.UsersControllerTests
{
    public class DeleteUserAsyncTest : UsersControllerTestBase
    {
        [Fact]
        public async Task DeleteUserAsync_UserFound_Returns204Status()
        {
            /// Arrange
            var genreId = UsersMockData.SingleUser().Id;
            _mockUserRepository.Setup(x => x.ExistsWithIdAsync(genreId)).ReturnsAsync(true);

            /// Act
            var result = await _systemUnderTest.DeleteUserAsync(genreId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
            ((NoContentResult)result).StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteUserAsync_UserNotFound_ThrowsNotFoundException()
        {
            /// Arrange
            var genreId = UsersMockData.SingleUser().Id;
            _mockUserRepository.Setup(x => x.ExistsWithIdAsync(genreId)).ReturnsAsync(false);

            /// Act
            var act = () => _systemUnderTest.DeleteUserAsync(genreId);

            /// Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("*User*");
        }
    }
}
