using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTask6.Models
{
    internal class MoviesGridViewModel
    {
        internal delegate void ChangedEventHandler(MoviesGridViewModel data);
        internal event ChangedEventHandler Changed;

        internal List<Movie> movies = new List<Movie>();
        internal List<Movie> Movies {
            get { return movies; }
            set
            {
                movies = value;
                Update();
            }
        }

        internal List<Actor> actors = new List<Actor>();
        internal List<Actor> Actors { get { return actors; } }

        internal void AddMovie(Movie movie)
        {
            movies.Add(movie);
            Update();
        }

        internal void AddMovieRange(Movie[] movies)
        {
            this.movies.AddRange(movies);
            Update();
        }

        private void Update()
        {
            if (Changed != null) { Changed(this); }
        }
    }
}
