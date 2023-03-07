using AutoMapper;
using MovieRestApiWithEF.API;

namespace MovieRestApiWithEF.Tests.Unit.Helpers
{
    public class TestHelpers
    {
        public static IMapper GetTestAutoMapper()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            return new Mapper(configuration);
        }
    }
}
