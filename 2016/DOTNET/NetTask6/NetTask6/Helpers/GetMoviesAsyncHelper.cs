﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetTask6.Models;
using NetTask6.Repositories;

namespace NetTask6.Helpers
{
    class GetMoviesAsyncHelper: AsyncHelper<List<Movie> >
    {
        IRepository<Movie> movieRepository;
        IRepository<Director> directorRepository;
        IRepository<Actor> actorRepository;

        internal GetMoviesAsyncHelper(
            IRepository<Movie> _movieRepository,
            IRepository<Director> _directorRepository,
            IRepository<Actor> _actorRepository)
        {
            movieRepository = _movieRepository;
            directorRepository = _directorRepository;
            actorRepository = _actorRepository;
        }

        internal void GetMovies(string movieName, string director, string actor)
        {
            Started();
            Task.Run(async () =>
            {
                try
                {
                    var movies = String.IsNullOrEmpty(movieName) ? movieRepository.GetAll()
                        : movieRepository.TextSearch(movieName);

                    if (!String.IsNullOrEmpty(director))
                    {
                        var directors = await directorRepository.ToArrayAsync(directorRepository.TextSearch(director));
                        movies = movies.Where(movie => directors.Contains(movie.Director));
                    }

                    if (!String.IsNullOrEmpty(actor))
                    {
                        var actors = await actorRepository.ToArrayAsync(actorRepository.TextSearch(actor));
                        movies = movies.Where(movie => actors.Intersect(movie.Actors).Count() != 0);
                    }

                    var result = await movieRepository.ToListAsync(movies);
                    Completed(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });
        }
    }
}