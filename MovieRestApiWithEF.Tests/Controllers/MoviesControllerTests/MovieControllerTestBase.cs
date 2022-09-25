using AutoMapper;
using Contracts;
using Moq;
using MovieRestApiWithEF.Controllers;
using MovieRestApiWithEF.Tests.Unit.Helpers;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MoviesControllerTests
{
    public class MovieControllerTestBase
    {
        protected readonly Mock<IRepositoryManager> _mockRepositoryManager;
        protected readonly Mock<IMovieRepository> _mockMovieRepository;
        protected readonly ILoggerManager _stubbedLogger;
        protected readonly IMapper _mapper;
        protected readonly MoviesController _systemUnderTest;

        public MovieControllerTestBase()
        {
            // Prepare Logger and Mapper mocks
            _stubbedLogger = new Mock<ILoggerManager>().Object;
            _mapper = TestHelpers.GetTestAutoMapper();

            // Initialize Repository mock
            _mockMovieRepository = new Mock<IMovieRepository>();

            // Prepare IoC container mock
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockRepositoryManager.Setup(x => x.MovieRepository).Returns(_mockMovieRepository.Object);

            // Prepare System under test (sut)
            _systemUnderTest = new MoviesController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);
        }
    }
}
