using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTask6.Models
{
    class EditMovieViewModel
    {
        internal delegate void ChangedEventHandler(EditMovieViewModel data);
        internal event ChangedEventHandler Changed;

        private int movieId;
        internal int MovieId
        {
            get { return movieId; }
            set { movieId = value; Update(); }
        }

        private string name;
        internal string Name
        {
            get { return name; }
            set { name = value; Update(); }
        }

        private uint year;
        internal uint Year
        {
            get { return year; }
            set { year = value; Update(); }
        }

        private string image;
        internal string Image
        {
            get { return image; }
            set { image = value; Update(); }
        }

        private Director director;
        internal Director Director
        {
            get { return director; }
            set { director = value; Update(); }
        }

        private List<Actor> actors;
        internal List<Actor> Actors
        {
            get { return actors; }
            set { actors = value; Update(); }
        }
        internal void ActorsRemoveAt(int i) { actors.RemoveAt(i); Update(); }

        private void Update()
        {
            if (Changed != null) { Changed(this); }
        }
    }
}
