using System;
using System.Windows.Forms;

using NetTask6.Models;

namespace NetTask6.Views
{
    internal sealed partial class SearchView: Form
    {
        internal delegate void UserInputEventHandler(string name, int year, string country, string director, string actor);
        internal delegate void SearchEventHandler();
        internal delegate void ClearEventHandler();

        internal event UserInputEventHandler UserInput;
        internal event SearchEventHandler Search;
        internal event ClearEventHandler Clear;

        internal SearchView(SearchViewModel data)
        {
            InitializeComponent();
            searchViewYearInput.SetErrorProvider(searchMovieFormErrorProvider);
            searchViewFilmNameEdit.SetErrorProvider(searchMovieFormErrorProvider);
            searchViewCountryInput.SetErrorProvider(searchMovieFormErrorProvider);

            data.NameChanged     += (m => searchViewFilmNameEdit.Text = m.Name);
            data.YearChanged     += (m => searchViewYearInput.Text = m.Year == 0 ? "" : m.Year.ToString());
            data.CountryChanged  += (m => searchViewCountryInput.Text = m.Country);
            data.DirectorChanged += (m => searchViewDirector.Text = m.Director);
            data.ActorChanged    += (m => searchViewActor.Text = m.Actor);
        }
        
        private void OnSearchFormBtnClearClick(object sender, EventArgs e)
        {
            if (Clear != null) { Clear(); }
        }

        private void OnSearchFormBtnSearchClick(object sender, EventArgs e)
        {
            if (Search != null) { Search(); }
        }

        private void OnSearchViewFilmNameEditTextChanged(object sender, EventArgs e)
        {
            FireUserInput();
        }

        private void OnSearchViewDirectorTextChanged(object sender, EventArgs e)
        {
            FireUserInput();
        }

        private void OnSearchViewActorTextChanged(object sender, EventArgs e)
        {
            FireUserInput();
        }

        private void OnSearchViewYearInputTextChanged(object sender, EventArgs e)
        {
            FireUserInput();
        }

        private void OnSearchViewCountryInputTextChanged(object sender, EventArgs e)
        {
            FireUserInput();
        }

        private void FireUserInput()
        {
            UserInput(searchViewFilmNameEdit.Text,
                searchViewYearInput.IsNumber ? Int32.Parse(searchViewYearInput.Text) : 0,
                searchViewCountryInput.Text,
                searchViewDirector.Text,
                searchViewActor.Text);
        }

        private void OnSearchViewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            if ((e.KeyCode & Keys.Enter) == Keys.Enter)
            {
                Search();
            }
        }
    }
}
