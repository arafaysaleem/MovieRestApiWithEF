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
            // To map EFCore model to Movie response
            CreateMap<Movie, MovieDto>();

            // To map EFCore model to Movie with nested properties response
            CreateMap<Movie, MovieWithDetailsDto>();

            // To map request movie body to Movie EFCore model
            CreateMap<MovieCreateDto, Movie>();


            // To map EFCore model to Movie response
            CreateMap<Genre, GenreDto>();

            // To map EFCore model to Movie with nested properties response
            CreateMap<Genre, GenreWithDetailsDto>();

            // To map request movie body to Movie EFCore model
            CreateMap<GenreCreateDto, Genre>();


            // To map EFCore model to Movie response
            CreateMap<MovieWorker, MovieWorkerDto>();

            // To map EFCore model to Movie with nested properties response
            CreateMap<MovieWorker, MovieWorkerWithDetailsDto>();

            // To map request movie body to Movie EFCore model
            CreateMap<MovieWorkerCreateDto, MovieWorker>();

        }
    }
}
