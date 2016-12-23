using NetTask6.Helpers;

namespace NetTask6.Models
{
    internal sealed class SearchViewModel
    {
        internal delegate void ChangedEventHandler(SearchViewModel data);
        internal event ChangedEventHandler NameChanged;
        internal event ChangedEventHandler CountryChanged;
        internal event ChangedEventHandler YearChanged;
        internal event ChangedEventHandler DirectorChanged;
        internal event ChangedEventHandler ActorChanged;

        private string name;
        internal string Name {
            get { return name; }
            set { bool upd = name != value; name = value; if (upd) { NameChanged(this); } }
        }

        private string country;
        internal string Country
        {
            get { return country; }
            set { bool upd = country != value; country = value; if (upd) { CountryChanged(this); } }
        }

        private int year;
        internal int Year
        {
            get { return year; }
            set { year = value; YearChanged(this); }
        }

        private string director;
        internal string Director
        {
            get { return director; }
            set { bool upd = director != value; director = value; if (upd) { DirectorChanged(this); } }
        }

        private string actor;
        internal string Actor
        {
            get { return actor; }
            set { bool upd = actor != value; actor = value; if (upd) { ActorChanged(this); } }
        }

        internal bool IsValid
        {
            get
            {
                return ValidationHelper.ValidateYear(year) &&
                    ValidationHelper.ValidatePlainText(name) &&
                    ValidationHelper.ValidatePlainText(country) &&
                    ValidationHelper.ValidatePlainText(director) &&
                    ValidationHelper.ValidatePlainText(actor);
            }
        }
    }
}
