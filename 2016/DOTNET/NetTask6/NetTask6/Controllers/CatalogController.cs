using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

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

        internal CatalogController(DatabaseContext dbCtx = null)
        {
            InitializeRepositories(dbCtx);

            moviesGridViewModel = new MoviesGridViewModel();
            searchViewModel = new SearchViewModel();
            editMovieViewModel = new EditMovieViewModel();

            getMovies = new GetMoviesAsyncHelper(movieRepository, directorRepository, actorRepository);
        }

        private void InitializeRepositories(DatabaseContext dbCtx)
        {
            if (dbCtx == null)
            {
                movieRepository = new MovieMemoryRepository();
                directorRepository = new DirectorMemoryRepository();
                actorRepository = new ActorMemoryRepository();
            }
            else
            {
                movieRepository = new MovieEFRepository(dbCtx.Movies, dbCtx);
                directorRepository = new DirectorEFRepository(dbCtx.Directors, dbCtx);
                actorRepository = new ActorEFRepository(dbCtx.Actors, dbCtx);
            }
        }

        internal Form RenderMainView()
        {
            InitSearchView();

            catalogView = new CatalogView(moviesGridViewModel, 
                new DirectorsAutocompleteSource(directorRepository),
                new ActorsAutocompleteSource(actorRepository));

            catalogView.RefillDatabase += (async () => 
            {
                await actorRepository.DropAll();
                await directorRepository.DropAll();
                await movieRepository.DropAll();

                await DefaultDataHelper.FillRepositories(movieRepository, directorRepository, actorRepository);

                GetMovies();
            });

            catalogView.SaveMovie += SaveMovie;
            catalogView.DeleteFilm += DeleteMovie;

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

            catalogView.AddMovieActor += (async (name) =>
            {
                var query = actorRepository.GetAll().Where(actor => actor.Name == name);
                var foundActor = (await actorRepository.ToListAsync(query)).FirstOrDefault();
                if (foundActor != null)
                {
                    editMovieViewModel.Actors.Add(foundActor);
                }
                else
                {
                    var newActor = new Actor() { Name = name };
                    await actorRepository.Save(newActor);
                    editMovieViewModel.Actors.Add(newActor);
                }
            });

            catalogView.FindFilm += (() => 
            {
                if (searchView.IsDisposed)
                {
                    InitSearchView();
                }
                searchView.Show();
                searchView.Activate();
                searchView.Left = catalogView.Left + catalogView.Width / 2 - searchView.Width / 2;
                catalogView.FindMovieMenuItem.Checked = true;
                catalogView.ExitMenuItem.Enabled = false;
            });

            catalogView.GoBack += (() =>
            {
                GetMovies();
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

            getMovies.OnStarted += (() => 
            {
                catalogView.SetGridStatus(false);
                catalogView.SetGridTitle(Properties.Resources.GridTitleLoading);
            });

            GetMovies();
            return catalogView;
        }

        private async void SaveMovie()
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
        }

        private async void DeleteMovie(List<Movie> selected)
        {
            bool confirmed = true;
            for (int i = 0; i<selected.Count; i++)
            {
                var name = selected[i].Name;
                var res = MessageBox.Show(
                    String.Format(Properties.Resources.ApproveDeleteMovie, selected[i].Name),
                    Properties.Resources.ApproveDeleteMovieCaption, MessageBoxButtons.YesNo);
                if (res.HasFlag(DialogResult.No))
                {
                    confirmed = false;
                    break;
                }
            }
            if (confirmed)
            {
                await Task.WhenAll(selected.Select(x => movieRepository.Delete(x)).ToArray());
                GetMovies();
            }
        }

        private void InitSearchView()
        {
            searchView = new SearchView(searchViewModel);
            searchView.Visible = false;

            searchView.UserInput += ((name, year, director, actor) =>
            {
                searchViewModel.Name = name;
                searchViewModel.Year = year;
                searchViewModel.Director = director;
                searchViewModel.Actor = actor;
            });

            searchView.Search += (() =>
            {
                string name = searchViewModel.Name;
                string director = searchViewModel.Director;
                string actor = searchViewModel.Actor;
                GetMovies(name, searchViewModel.Year, searchViewModel.Country, director, actor);
                catalogView.Activate();
            });

            searchView.Clear += (() =>
            {
                searchViewModel.Name = "";
                searchViewModel.Director = "";
                searchViewModel.Actor = "";
            });

            searchView.FormClosed += ((s, e) =>
            {
                catalogView.FindMovieMenuItem.Checked = false;
                catalogView.ExitMenuItem.Enabled = true;
            });
        }

        private void GetMovies(
            string movieName = null,
            int year = 0, 
            string country = null, 
            string director = null, 
            string actor = null)
        {
            GetMoviesAsyncHelper.OnCompletedEventHandler onCompletedHandler = null;
            var isEmptySearch = String.IsNullOrEmpty(movieName)
                && year == 0
                && String.IsNullOrEmpty(country)
                && String.IsNullOrEmpty(director)
                && String.IsNullOrEmpty(actor);

            onCompletedHandler = ((movies) =>
            {
                getMovies.OnCompleted -= onCompletedHandler;
                catalogView.Invoke(new Action(() =>
                {
                    moviesGridViewModel.Movies = movies;
                    catalogView.SetGridTitle(isEmptySearch ? 
                        Properties.Resources.GridTitleAllMovies : 
                        String.Format(Properties.Resources.GridTitleSearchResult, movieName));
                    catalogView.SetGridStatus(true);
                }));
            });
            getMovies.OnCompleted += onCompletedHandler;
            getMovies.GetMovies(movieName, year, country, director, actor);
        }
    }
}
