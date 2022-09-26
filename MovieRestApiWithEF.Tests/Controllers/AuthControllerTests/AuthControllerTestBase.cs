using AutoMapper;
using Contracts;
using Moq;
using MovieRestApiWithEF.Controllers;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.AuthControllerTests
{
    public class AuthControllerTestBase
    {
        protected readonly Mock<IRepositoryManager> _mockRepositoryManager;
        protected readonly Mock<IUserRepository> _mockUserRepository;
        protected readonly Mock<IJwtService> _jwtService;
        protected readonly ILoggerManager _stubbedLogger;
        protected readonly AuthController _systemUnderTest;

        public AuthControllerTestBase()
        {
            // Prepare Logger and JWT service mocks
            _stubbedLogger = new Mock<ILoggerManager>().Object;
            _jwtService = new Mock<IJwtService>();

            // Initialize Repository mock
            _mockUserRepository = new Mock<IUserRepository>();

            // Prepare IoC container mock
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockRepositoryManager.Setup(x => x.UserRepository).Returns(_mockUserRepository.Object);

            // Prepare System under test (sut)
            _systemUnderTest = new AuthController(_mockRepositoryManager.Object, _stubbedLogger, _jwtService.Object);
        }
    }
}
