﻿using Entities.Models;
using MovieRestApiWithEF.Core.Models;
using MovieRestApiWithEF.Core.Requests;
using MovieRestApiWithEF.Core.Responses;

namespace MovieRestApiWithEF.Tests.Unit.Controllers.MoviesControllerTests
{
    public class MoviesMockData
    {
        private static DateTime DateTime = DateTime.Now;

        public static List<Movie> AllMovies()
        {
            return new List<Movie>{
                 new Movie{
                     Id = 1, Title = "Movie 1", ReleaseDate = DateTime, DirectorId = 1, GenreId = 1,
                     Director = new MovieWorker { Id = 1, FullName = "Director 1", PictureUrl = "some-url" },
                     Genre = new Genre { Id = 1, Name = "Genre 1" },
                     Cast = new List<MovieWorker>(),
                 },
                 new Movie{
                     Id = 2, Title = "Movie 2", ReleaseDate = DateTime, DirectorId = 2, GenreId = 2,
                     Director = new MovieWorker { Id = 2, FullName = "Director 2", PictureUrl = "some-url" },
                     Genre = new Genre { Id = 2, Name = "Genre 2" },
                     Cast = new List<MovieWorker>(),
                 },
            };
        }

        public static List<MovieWithDetailsResponse> AllMoviesResponse()
        {
            return new List<MovieWithDetailsResponse>{
                 new MovieWithDetailsResponse
                 {
                     Id = 1, Title = "Movie 1", ReleaseDate = DateTime,
                     Director = new MovieWorkerResponse { Id = 1, FullName = "Director 1", PictureUrl = "some-url" },
                     Genre = new GenreResponse { Id = 1, Name = "Genre 1" },
                     Cast = new List<MovieWorkerResponse>(),
                 },
                 new MovieWithDetailsResponse
                 {
                     Id = 2, Title = "Movie 2", ReleaseDate = DateTime,
                     Director = new MovieWorkerResponse { Id = 2, FullName = "Director 2", PictureUrl = "some-url" },
                     Genre = new GenreResponse { Id = 2, Name = "Genre 2" },
                     Cast = new List<MovieWorkerResponse>(),
                 },
            };
        }

        public static List<Movie> EmptyMovies()
        {
            return new List<Movie>();
        }

        public static Movie SingleMovie()
        {
            return new Movie
            {
                Id = 1,
                Title = "Movie 1",
                ReleaseDate = DateTime,
                DirectorId = 1,
                GenreId = 1,
                Director = new MovieWorker { Id = 1, FullName = "Director 1", PictureUrl = "some-url" },
                Genre = new Genre { Id = 1, Name = "Genre 1" },
                Cast = new List<MovieWorker>(),
            };
        }

        public static MovieWithDetailsResponse SingleMovieResponse()
        {
            return new MovieWithDetailsResponse
            {
                Id = 1,
                Title = "Movie 1",
                ReleaseDate = DateTime,
                Director = new MovieWorkerResponse { Id = 1, FullName = "Director 1", PictureUrl = "some-url" },
                Genre = new GenreResponse { Id = 1, Name = "Genre 1" },
                Cast = new List<MovieWorkerResponse>(),
            };
        }

        public static MovieCreateRequest NewMovieCreateRequest()
        {
            return new MovieCreateRequest
            {
                Title = "Movie 1",
                ReleaseDate = DateTime,
                DirectorId = 1,
                GenreId = 1,
                CastIds = new List<int> { 1, 2, 3 },
            };
        }

        public static MovieWithDetailsResponse NewMovieCreateResponse()
        {
            return new MovieWithDetailsResponse
            {
                Id = 1,
                Title = "Movie 1",
                ReleaseDate = DateTime,
                Director = new MovieWorkerResponse { Id = 1, FullName = "Director 1", PictureUrl = "some-url" },
                Genre = new GenreResponse { Id = 1, Name = "Genre 1" },
                Cast = new List<MovieWorkerResponse>
                {
                    new MovieWorkerResponse { Id = 1, FullName = "Cast 1", PictureUrl = "some-url" },
                    new MovieWorkerResponse { Id = 2, FullName = "Cast 2", PictureUrl = "some-url" },
                    new MovieWorkerResponse { Id = 3, FullName = "Cast 3", PictureUrl = "some-url" },
                },
            };
        }

        public static IEnumerable<MovieWorker> NewMovieCast()
        {
            return new List<MovieWorker>
            {
                new MovieWorker { Id = 1, FullName = "Cast 1", PictureUrl = "some-url" },
                new MovieWorker { Id = 2, FullName = "Cast 2", PictureUrl = "some-url" },
                new MovieWorker { Id = 3, FullName = "Cast 3", PictureUrl = "some-url" },
            };
        }
    }
}
