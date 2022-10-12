using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.API.Exceptions;
using MovieRestApiWithEF.Core.Models;
using MovieRestApiWithEF.Core.Responses;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.UsersControllerTests
{
    public class GetOneUserAsyncTest : UsersControllerTestBase
    {
        [Fact]
        public async Task GetOneUserAsync_UserFound_Returns200Status()
        {
            /// Arrange
            var mockUser = UsersMockData.SingleUser();
            var genreId = mockUser.Id;
            _mockUserRepository.Setup(x => x.FindByIdAsync(genreId)).ReturnsAsync(mockUser);

            /// Act
            var result = await _systemUnderTest.GetOneUserAsync(genreId);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetOneUserAsync_UserFound_ReturnsUserWithGivenId()
        {
            /// Arrange
            var mockUser = UsersMockData.SingleUser();
            var genreId = mockUser.Id;
            _mockUserRepository.Setup(x => x.FindByIdAsync(genreId)).ReturnsAsync(mockUser);

            /// Act
            var okResult = await _systemUnderTest.GetOneUserAsync(genreId) as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<UserResponse>();
            (okResult.Value as UserResponse)!.Id.Should().Be(genreId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(5000)]
        public async Task GetOneUserAsync_UserNotFound_ThrowsNotFoundException(int genreId)
        {
            /// Arrange
            _mockUserRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync((User?)null);

            /// Act
            var act = () => _systemUnderTest.GetOneUserAsync(genreId);

            /// Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("*User*");
        }
    }
}
