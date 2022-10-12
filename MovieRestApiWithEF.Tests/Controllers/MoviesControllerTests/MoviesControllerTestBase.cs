using AutoMapper;
using Moq;
using MovieRestApiWithEF.API.Controllers;
using MovieRestApiWithEF.Infrastructure;
using MovieRestApiWithEF.Tests.Unit.Helpers;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MoviesControllerTests
{
    public class MoviesControllerTestBase
    {
        protected readonly Mock<IRepositoryManager> _mockRepositoryManager;
        protected readonly Mock<IMovieRepository> _mockMovieRepository;
        protected readonly Mock<IMovieWorkerRepository> _mockMovieWorkerRepository;
        protected readonly ILoggerManager _stubbedLogger;
        protected readonly IMapper _mapper;
        protected readonly MoviesController _systemUnderTest;

        public MoviesControllerTestBase()
        {
            // Prepare Logger and Mapper mocks
            _stubbedLogger = new Mock<ILoggerManager>().Object;
            _mapper = TestHelpers.GetTestAutoMapper();

            // Initialize Repository mocks
            _mockMovieRepository = new Mock<IMovieRepository>();
            _mockMovieWorkerRepository = new Mock<IMovieWorkerRepository>();

            // Prepare IoC container mock
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockRepositoryManager.Setup(x => x.MovieRepository).Returns(_mockMovieRepository.Object);
            _mockRepositoryManager.Setup(x => x.MovieWorkerRepository).Returns(_mockMovieWorkerRepository.Object);

            // Prepare System under test (sut)
            _systemUnderTest = new MoviesController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);
        }
    }
}
