using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
