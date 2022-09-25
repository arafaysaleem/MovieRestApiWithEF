using AutoMapper;
using Contracts;
using Moq;
using MovieRestApiWithEF.Controllers;
using MovieRestApiWithEF.Tests.Unit.Helpers;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MovieWorkersControllerTests
{
    public class MovieWorkerControllerTestBase
    {
        protected readonly Mock<IRepositoryManager> _mockRepositoryManager;
        protected readonly Mock<IMovieWorkerRepository> _mockMovieWorkerRepository;
        protected readonly ILoggerManager _stubbedLogger;
        protected readonly IMapper _mapper;
        protected readonly MovieWorkersController _systemUnderTest;

        public MovieWorkerControllerTestBase()
        {
            // Prepare Logger and Mapper mocks
            _stubbedLogger = new Mock<ILoggerManager>().Object;
            _mapper = TestHelpers.GetTestAutoMapper();

            // Initialize Repository mock
            _mockMovieWorkerRepository = new Mock<IMovieWorkerRepository>();

            // Prepare IoC container mock
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockRepositoryManager.Setup(x => x.MovieWorkerRepository).Returns(_mockMovieWorkerRepository.Object);

            // Prepare System under test (sut)
            _systemUnderTest = new MovieWorkersController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);
        }
    }
}
