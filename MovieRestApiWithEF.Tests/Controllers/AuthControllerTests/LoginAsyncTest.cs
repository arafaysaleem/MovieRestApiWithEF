using Entities.Models;
using Entities.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieRestApiWithEF.Exceptions;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.AuthControllerTests
{
    public class LoginAsyncTest : AuthControllerTestBase
    {
        [Fact]
        public async Task LoginAsync_CorrectLogin_Returns200Status()
        {
            /// Arrange
            var mockUser = AuthMockData.ExistingUser();
            var loginDetails = AuthMockData.CorrectLoginDetails();
            var givenEmail = loginDetails.Email!;
            _mockUserRepository.Setup(x => x.FindByEmailAsync(givenEmail)).ReturnsAsync(mockUser);
            _jwtService.Setup(x => x.BuildToken(mockUser)).Returns(AuthMockData.TestJwtToken);

            /// Act
            var result = await _systemUnderTest.LoginAsync(loginDetails);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task LoginAsync_CorrectLogin_ReturnsSuccessfulAuthDetails()
        {
            /// Arrange
            var mockUser = AuthMockData.ExistingUser();
            var loginDetails = AuthMockData.CorrectLoginDetails();
            var givenEmail = loginDetails.Email!;
            _mockUserRepository.Setup(x => x.FindByEmailAsync(givenEmail)).ReturnsAsync(mockUser);
            _jwtService.Setup(x => x.BuildToken(mockUser)).Returns(AuthMockData.TestJwtToken);

            /// Act
            var okResult = await _systemUnderTest.LoginAsync(loginDetails) as OkObjectResult;

            /// Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<AuthenticatedResponse>();
            okResult!.Value.Should().BeEquivalentTo(AuthMockData.LoginSuccessResponse());
        }

        [Fact]
        public async Task LoginAsync_UnknownEmail_ThrowsNotFoundException()
        {
            /// Arrange
            var mockUser = AuthMockData.ExistingUser();
            var loginDetails = AuthMockData.UnknownEmailLogin();
            var givenEmail = loginDetails.Email!;
            _mockUserRepository.Setup(x => x.FindByEmailAsync(givenEmail)).ReturnsAsync((User?)null);

            /// Act
            var act = () => _systemUnderTest.LoginAsync(loginDetails);

            /// Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Email*");
        }

        [Fact]
        public async Task LoginAsync_IncorrectPassword_ThrowsUnauthorizedException()
        {
            /// Arrange
            var mockUser = AuthMockData.ExistingUser();
            var loginDetails = AuthMockData.IncorrectPasswordLogin();
            var givenEmail = loginDetails.Email!;
            _mockUserRepository.Setup(x => x.FindByEmailAsync(givenEmail)).ReturnsAsync(mockUser);

            /// Act
            var act = () => _systemUnderTest.LoginAsync(loginDetails);

            /// Assert
            await act.Should().ThrowAsync<UnauthorizedException>().WithMessage("*password*");
        }
    }
}
