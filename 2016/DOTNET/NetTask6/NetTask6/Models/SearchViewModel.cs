namespace NetTask6.Models
{
    class SearchViewModel
    {
        internal delegate void ChangedEventHandler(SearchViewModel data);
        internal event ChangedEventHandler Changed;

        private string name;
        internal string Name {
            get { return name; }
            set { name = value; Update(); }
        }

        private string country;
        internal string Country
        {
            get { return country; }
            set { country = value; Update(); }
        }

        private int year;
        internal int Year
        {
            get { return year; }
            set { year = value; Update(); }
        }

        private string director;
        internal string Director
        {
            get { return director; }
            set { director = value; Update(); }
        }

        private string actor;
        internal string Actor
        {
            get { return actor; }
            set { actor = value; Update(); }
        }

        private void Update()
        {
            if (Changed != null) { Changed(this); }
        }
    }
}
