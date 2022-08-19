using AutoMapper;
using Entities.Models;
using Entities.RequestDtos;
using Entities.ResponseDtos;

namespace MovieRestApiWithEF
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<Movie, MovieWithDetailsDto>();
            CreateMap<MovieCreateDto, Movie>();
            CreateMap<MovieUpdateDto, Movie>();

            CreateMap<Genre, GenreDto>();
            CreateMap<Genre, GenreWithDetailsDto>();
            CreateMap<GenreCreateDto, Genre>();

            CreateMap<MovieWorker, MovieWorkerDto>();
            CreateMap<MovieWorker, MovieWorkerWithDetailsDto>();
            CreateMap<MovieWorkerCreateDto, MovieWorker>();
        }
    }
}
