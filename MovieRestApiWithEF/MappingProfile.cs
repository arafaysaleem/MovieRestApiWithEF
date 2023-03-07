using AutoMapper;
using MovieRestApiWithEF.Core.Models;
using MovieRestApiWithEF.Core.Requests;
using MovieRestApiWithEF.Core.Responses;

namespace MovieRestApiWithEF.API
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


            // To map EFCore model to Genre response
            CreateMap<Genre, GenreResponse>();

            // To map EFCore model to Genre with nested properties response
            CreateMap<Genre, GenreWithDetailsResponse>();

            // To map request genre body to Genre EFCore model
            CreateMap<GenreCreateRequest, Genre>();


            // To map EFCore model to MovieWorker response
            CreateMap<MovieWorker, MovieWorkerResponse>();

            // To map EFCore model to MovieWorker with nested properties response
            CreateMap<MovieWorker, MovieWorkerWithDetailsResponse>();

            // To map request movie worker body to MovieWorker EFCore model
            CreateMap<MovieWorkerCreateRequest, MovieWorker>();


            // To map EFCore model to User profile response
            CreateMap<User, UserResponse>();
        }
    }
}
