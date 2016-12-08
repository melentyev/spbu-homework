﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

using NetTask6.Models;
using NetTask6.Repositories;
using NetTask6.Views;
using NetTask6.Helpers;
using NetTask6.Services;

namespace NetTask6.Controllers
{
    internal class CatalogController
    {
        CatalogView catalogView;
        SearchView searchView;

        MoviesGridViewModel moviesGridViewModel;
        SearchViewModel searchViewModel;
        EditMovieViewModel editMovieViewModel;

        IRepository<Movie> movieRepository;
        IRepository<Director> directorRepository;
        IRepository<Actor> actorRepository;

        GetMoviesAsyncHelper getMovies;

        internal CatalogController(DatabaseContext dbCtx)
        {
            ManualResetEvent ev = new ManualResetEvent(false);
            if (dbCtx == null)
            {
                movieRepository = new MovieMemoryRepository();
                directorRepository = new DirectorMemoryRepository();
                actorRepository = new ActorMemoryRepository();

                DefaultDataHelper.FillRepositories(movieRepository, directorRepository, actorRepository)
                    .ContinueWith((t) => { ev.Set(); });
            }
            else
            {
                movieRepository = new MovieEFRepository(dbCtx.Movies, dbCtx);
                directorRepository = new DirectorEFRepository(dbCtx.Directors, dbCtx);
                actorRepository = new ActorEFRepository(dbCtx.Actors, dbCtx);

                dbCtx.Actors.ToList().ForEach(x => dbCtx.Actors.Remove(x));
                dbCtx.Directors.ToList().ForEach(x => dbCtx.Directors.Remove(x));
                dbCtx.Movies.ToList().ForEach(x => dbCtx.Movies.Remove(x));

                DefaultDataHelper.FillRepositories(movieRepository, directorRepository, actorRepository)
                    .ContinueWith((t) => { ev.Set(); });
            }
            ev.WaitOne();
            moviesGridViewModel = new MoviesGridViewModel();
            searchViewModel = new SearchViewModel();
            editMovieViewModel = new EditMovieViewModel();

            getMovies = new GetMoviesAsyncHelper(movieRepository, directorRepository, actorRepository);
        }

        internal Form RenderMainView()
        {
            searchView = new SearchView(searchViewModel);
            searchView.Visible = false;

            catalogView = new CatalogView(moviesGridViewModel, 
                new DirectorsAutocompleteSource(directorRepository),
                new ActorsAutocompleteSource(actorRepository));

            catalogView.EditFilm += (List<Movie> selected) =>
            {
                if (selected.Count > 0)
                {
                    var movie = selected[0];
                    editMovieViewModel.MovieId = movie.MovieId;
                    editMovieViewModel.Name = movie.Name;
                    editMovieViewModel.Year = movie.Year;
                    editMovieViewModel.Image = movie.Image;
                    editMovieViewModel.Actors = new List<Actor>(movie.Actors);
                    editMovieViewModel.Director = movie.Director;
                    catalogView.ShowMovieEditForm(editMovieViewModel);
                }
            };

            catalogView.SaveMovie += async () =>
            {
                catalogView.SetEnabledState(false);

                var id = editMovieViewModel.MovieId;
                Movie model = (await movieRepository.ToArrayAsync(movieRepository.GetAll().Where(x => x.MovieId == id)))[0];

                string directorName;
                List<string> actorNames = new List<string>();
                catalogView.SaveEditMovieViewModelState(editMovieViewModel, out directorName, actorNames);

                Director[] existing = await directorRepository.ToArrayAsync(
                    directorRepository.GetAll().Where(x => x.Name == directorName));

                model.Name = editMovieViewModel.Name;
                model.Year = editMovieViewModel.Year;
                model.Director = existing.Length == 0 ? new Director() { Name = directorName } : existing[0];
                model.Actors = editMovieViewModel.Actors;

                if (model.Director.DirectorId < 1)
                {
                    await directorRepository.Save(model.Director);
                }

                await movieRepository.Save(model);

                catalogView.SetEnabledState(true);
            };

            catalogView.FindFilm += (() => 
            {
                if (searchView.IsDisposed)
                {
                    searchView = new SearchView(searchViewModel);
                }
                searchView.Show();
            });

            catalogView.GoBack += (() =>
            {
                catalogView.ShowMovieGrid();
            });

            catalogView.AboutOpen += (() =>
            {
                var aboutView = new AboutView();
                aboutView.ShowDialog();
            });

            catalogView.DeleteActor += ((position) =>
            {
                editMovieViewModel.ActorsRemoveAt(position);
            });

            searchView.UserInput += ((name, director, actor) =>
            {
                searchViewModel.Name = name;
                searchViewModel.Director = director;
                searchViewModel.Actor = actor;
            });

            searchView.Search += (() =>
            {
                string name = searchViewModel.Name;
                string director = searchViewModel.Director;
                string actor = searchViewModel.Actor;
                GetMovies(name, director, actor);
            });

            searchView.Clear += (() =>
            {
                searchViewModel.Name = "";
                searchViewModel.Director = "";
                searchViewModel.Actor = "";
            });

            getMovies.OnStarted += (() => 
            {
                catalogView.SetGridStatus(false);
                catalogView.SetGridTitle("Загрузка...");
            });

            GetMovies(null, null, null);
            return catalogView;
        }

        private void GetMovies(string movieName, string director, string actor)
        {
            GetMoviesAsyncHelper.OnCompletedEventHandler onCompletedHandler = null;
            var isEmptySearch = String.IsNullOrEmpty(movieName)
                && String.IsNullOrEmpty(director)
                && String.IsNullOrEmpty(actor);

            onCompletedHandler = ((movies) =>
            {
                getMovies.OnCompleted -= onCompletedHandler;
                catalogView.Invoke(new Action(() =>
                {
                    moviesGridViewModel.Movies = movies;
                    catalogView.SetGridTitle(isEmptySearch ? "Все фильмы" : "Результаты поиска по критерию: " + movieName);
                    catalogView.SetGridStatus(true);
                }));
            });
            getMovies.OnCompleted += onCompletedHandler;
            getMovies.GetMovies(movieName, director, actor);
        }
    }
}