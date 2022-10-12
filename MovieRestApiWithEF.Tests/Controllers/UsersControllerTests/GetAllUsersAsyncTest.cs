using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.Core.Responses;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.UsersControllerTests
{
    public class GetAllUsersAsyncTest : UsersControllerTestBase
    {
        [Fact]
        public async Task GetAllUsersAsync_WithoutDetails_HasUsers_Returns200Status()
        {
            /// Arrange
            _mockUserRepository.Setup(x => x.FindAllAsync()).ReturnsAsync(UsersMockData.AllUsers());

            /// Act
            var result = await _systemUnderTest.GetAllUsersAsync();

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllUsersAsync_WithoutDetails_HasUsers_ReturnsUsers()
        {
            /// Arrange
            _mockUserRepository.Setup(x => x.FindAllAsync()).ReturnsAsync(UsersMockData.AllUsers());

            /// Act
            var okResult = await _systemUnderTest.GetAllUsersAsync() as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<List<UserResponse>>();
            okResult!.Value.Should().BeEquivalentTo(UsersMockData.AllUsersResponse());
        }

        [Fact]
        public async Task GetAllUsersAsync_HasNoUsers_Returns200Status()
        {
            /// Arrange
            _mockUserRepository.Setup(x => x.FindAllAsync()).ReturnsAsync(UsersMockData.EmptyUsers());

            /// Act
            var result = await _systemUnderTest.GetAllUsersAsync();

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllUsersAsync_HasNoUsers_ReturnsEmptyUsers()
        {
            /// Arrange
            _mockUserRepository.Setup(x => x.FindAllAsync()).ReturnsAsync(UsersMockData.EmptyUsers());

            /// Act
            var okResult = await _systemUnderTest.GetAllUsersAsync() as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<List<UserResponse>>();
            okResult!.Value.As<List<UserResponse>>().Should().BeEmpty();
        }
    }
}
