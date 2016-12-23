using System;
using System.Collections.Generic;

using NetTask6.Helpers;

namespace NetTask6.Models
{
    internal sealed class EditMovieViewModel
    {
        internal delegate void ChangedEventHandler(EditMovieViewModel data);
        internal event ChangedEventHandler MovieIdChanged;
        internal event ChangedEventHandler NameChanged;
        internal event ChangedEventHandler CountryChanged;
        internal event ChangedEventHandler YearChanged;
        internal event ChangedEventHandler ImageChanged;
        internal event ChangedEventHandler DirectorChanged;
        internal event ChangedEventHandler ActorsChanged;

        private int movieId;
        internal int MovieId
        {
            get { return movieId; }
            set { movieId = value; if (MovieIdChanged != null) { MovieIdChanged(this); } }
        }

        private string name;
        internal string Name
        {
            get { return name; }
            set { name = value; NameChanged(this); }
        }

        private string country;
        internal string Country
        {
            get { return country; }
            set { country = value; CountryChanged(this); }
        }

        private int year;
        internal int Year
        {
            get { return year; }
            set { year = value; YearChanged(this); }
        }

        private string image;
        internal string Image
        {
            get { return image; }
            set { image = value; ImageChanged(this); }
        }

        private Director director;
        internal Director Director
        {
            get { return director; }
            set { director = value; DirectorChanged(this); }
        }

        private List<Actor> actors;
        internal List<Actor> Actors
        {
            get { return actors; }
            set { actors = value; ActorsChanged(this); }
        }
        internal void ActorsRemoveAt(int i) { actors.RemoveAt(i); ActorsChanged(this); }

        internal bool IsValid
        {
            get
            {
                return ValidationHelper.ValidateYear(year) &&
                    ValidationHelper.ValidatePlainText(name) &&
                    ValidationHelper.ValidatePlainText(country);
            }
        }
    }
}
