using AutoMapper;
using Moq;
using MovieRestApiWithEF.API.Controllers;
using MovieRestApiWithEF.Infrastructure;
using MovieRestApiWithEF.Tests.Unit.Helpers;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.UsersControllerTests
{
    public class UsersControllerTestBase
    {
        protected readonly Mock<IRepositoryManager> _mockRepositoryManager;
        protected readonly Mock<IUserRepository> _mockUserRepository;
        protected readonly ILoggerManager _stubbedLogger;
        protected readonly IMapper _mapper;
        protected readonly UsersController _systemUnderTest;

        public UsersControllerTestBase()
        {
            // Prepare Logger and Mapper mocks
            _stubbedLogger = new Mock<ILoggerManager>().Object;
            _mapper = TestHelpers.GetTestAutoMapper();

            // Initialize Repository mock
            _mockUserRepository = new Mock<IUserRepository>();

            // Prepare IoC container mock
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockRepositoryManager.Setup(x => x.UserRepository).Returns(_mockUserRepository.Object);

            // Prepare System under test (sut)
            _systemUnderTest = new UsersController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);
        }
    }
}
