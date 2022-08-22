using AutoMapper;
using Entities.Models;
using Entities.Requests;
using Entities.Responses;

namespace MovieRestApiWithEF
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // To map EFCore model to Movie response
            CreateMap<Movie, MovieResponse>();

            // To map EFCore model to Movie with nested properties response
            CreateMap<Movie, MovieWithDetailsResponse>();

            // To map request movie body to Movie EFCore model
            CreateMap<MovieCreateRequest, Movie>();


            // To map EFCore model to Movie response
            CreateMap<Genre, GenreResponse>();

            // To map EFCore model to Movie with nested properties response
            CreateMap<Genre, GenreWithDetailsResponse>();

            // To map request movie body to Movie EFCore model
            CreateMap<GenreCreateRequest, Genre>();


            // To map EFCore model to Movie response
            CreateMap<MovieWorker, MovieWorkerResponse>();

            // To map EFCore model to Movie with nested properties response
            CreateMap<MovieWorker, MovieWorkerWithDetailsResponse>();

            // To map request movie body to Movie EFCore model
            CreateMap<MovieWorkerCreateRequest, MovieWorker>();

        }
    }
}
