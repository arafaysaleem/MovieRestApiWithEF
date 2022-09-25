using AutoMapper;
using Contracts;
using Moq;
using MovieRestApiWithEF.Controllers;
using MovieRestApiWithEF.Tests.Unit.Helpers;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.GenresControllerTests
{
    public class GenreControllerTestBase
    {
        protected readonly Mock<IRepositoryManager> _mockRepositoryManager;
        protected readonly Mock<IGenreRepository> _mockGenreRepository;
        protected readonly ILoggerManager _stubbedLogger;
        protected readonly IMapper _mapper;
        protected readonly GenresController _systemUnderTest;

        public GenreControllerTestBase()
        {
            // Prepare Logger and Mapper mocks
            _stubbedLogger = new Mock<ILoggerManager>().Object;
            _mapper = TestHelpers.GetTestAutoMapper();

            // Initialize Repository mock
            _mockGenreRepository = new Mock<IGenreRepository>();

            // Prepare IoC container mock
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockRepositoryManager.Setup(x => x.GenreRepository).Returns(_mockGenreRepository.Object);

            // Prepare System under test (sut)
            _systemUnderTest = new GenresController(_mockRepositoryManager.Object, _stubbedLogger, _mapper);
        }
    }
}
