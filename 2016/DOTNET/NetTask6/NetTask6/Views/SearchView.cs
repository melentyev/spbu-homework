using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetTask6.Models;

namespace NetTask6.Views
{
    internal partial class SearchView : Form
    {
        internal delegate void UserInputEventHandler(string name, uint year, string director, string actor);
        internal delegate void SearchEventHandler();
        internal delegate void ClearEventHandler();

        internal event UserInputEventHandler UserInput;
        internal event SearchEventHandler Search;
        internal event ClearEventHandler Clear;

        internal SearchView(SearchViewModel data)
        {
            InitializeComponent();
            searchViewYearInput.SetErrorProvider(searchMovieFormErrorProvider);
            data.Changed += UpdateFromModel;
        }

        private void UpdateFromModel(SearchViewModel data)
        {
            if (this.searchViewFilmNameEdit.Text != data.Name)
            {
                this.searchViewFilmNameEdit.Text = data.Name;
            }
            if (this.searchViewCountryInput.Text != data.Country)
            {
                this.searchViewCountryInput.Text = data.Country;
            }
            if (this.searchViewDirector.Text != data.Director)
            {
                this.searchViewDirector.Text = data.Director;
            }
            if (this.searchViewActor.Text != data.Actor)
            {
                this.searchViewActor.Text = data.Actor;
            }
        }

        private void searchFormBtnClear_Click(object sender, EventArgs e)
        {
            if (Clear != null) { Clear(); }
        }

        private void searchFormBtnSearch_Click(object sender, EventArgs e)
        {
            if (Search != null) { Search(); }
        }

        private void searchViewFilmNameEdit_TextChanged(object sender, EventArgs e)
        {
            FireUserInput();
        }

        private void searchViewDirector_TextChanged(object sender, EventArgs e)
        {
            FireUserInput();
        }

        private void FireUserInput()
        {
            UserInput(searchViewFilmNameEdit.Text,
                searchViewYearInput.IsValid ? UInt32.Parse(searchViewYearInput.Text) : 0,
                searchViewDirector.Text, "");
        }

        private void searchViewActor_TextChanged(object sender, EventArgs e)
        {
            FireUserInput();
        }

        private void searchViewYearInput_TextChanged(object sender, EventArgs e)
        {
            FireUserInput();
        }

        private void searchViewCountryInput_TextChanged(object sender, EventArgs e)
        {
            FireUserInput();
        }
    }
}
