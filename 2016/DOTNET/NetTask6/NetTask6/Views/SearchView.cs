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
        internal delegate void UserInputEventHandler(string name, string director, string actor);
        internal delegate void SearchEventHandler();
        internal delegate void ClearEventHandler();

        internal event UserInputEventHandler UserInput;
        internal event SearchEventHandler Search;
        internal event ClearEventHandler Clear;

        internal SearchView(SearchViewModel data)
        {
            InitializeComponent();
            data.Changed += UpdateFromModel;
        }

        private void UpdateFromModel(SearchViewModel data)
        {
            if (this.searchViewFilmNameEdit.Text != data.Name)
            {
                this.searchViewFilmNameEdit.Text = data.Name;
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
            Clear();
        }

        private void searchFormBtnSearch_Click(object sender, EventArgs e)
        {
            Search();
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
            UserInput(searchViewFilmNameEdit.Text, searchViewDirector.Text, "");
        }
    }
}
