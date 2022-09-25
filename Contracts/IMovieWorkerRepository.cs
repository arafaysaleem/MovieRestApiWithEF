﻿using Entities.Models;
using System.Linq.Expressions;

namespace Contracts
{
    public interface IMovieWorkerRepository
    {
        Task<IEnumerable<MovieWorker>> FindAllAsync(
            Expression<Func<MovieWorker, bool>>? condition = null,
            bool details = false,
            bool tracking = false
        );

        Task<MovieWorker?> FindByIdAsync(int id);

        Task<MovieWorker?> FindMovieWorkerMoviesAsync(int id);

        Task<bool> ExistsWithIdAsync(int id);

        void Create(MovieWorker MovieWorker);

        void Update(MovieWorker MovieWorker);

        void Delete(int id);
    }
}
