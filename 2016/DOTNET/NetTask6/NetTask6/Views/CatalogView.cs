using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

using NetTask6.Models;
using NetTask6.Services;
using NetTask6.Helpers;

namespace NetTask6.Views
{
    internal partial class CatalogView : Form
    {
        internal delegate void EditFilmEventHandler(List<Movie> selected);
        internal delegate void DeleteFilmEventHandler(List<Movie> selected);
        internal delegate void FindFilmEventHandler();
        internal delegate void GoBackEventHandler();
        internal delegate void AboutOpenEventHandler();
        internal delegate void DeleteActorEventHandler(int position);
        internal delegate void SaveMovieEventHandler();

        internal event EditFilmEventHandler EditFilm;
        internal event DeleteFilmEventHandler DeleteFilm;
        internal event FindFilmEventHandler FindFilm;
        internal event GoBackEventHandler GoBack;
        internal event AboutOpenEventHandler AboutOpen;
        internal event DeleteActorEventHandler DeleteActor;
        internal event SaveMovieEventHandler SaveMovie;

        const int moviesInRow = 2;

        internal CatalogView(
            MoviesGridViewModel data, 
            IAutocompleteSource directorsAutocompleteSource,
            IAutocompleteSource actorsAutocompleteSource)
        {
            InitializeComponent();
            for (int i = 0; i < moviesInRow; i++)
            {
                var column = new TextAndImageColumn();
                column.Width = 300;
                column.Resizable = DataGridViewTriState.False;
                column.ReadOnly = true;
                dataGridView1.Columns.Add(column);
            }

            editFormDirector.SetSource(directorsAutocompleteSource);
            editFormAddActor.SetSource(actorsAutocompleteSource);

            editMovieFormHelpProvider.SetShowHelp(editFormNameText, true);
            editMovieFormHelpProvider.SetHelpString(editFormNameText, "Название фильма");

            data.Changed += UpdateGridView;
            UpdateGridView(data);
            ShowMovieGrid();
        }

        internal void SetGridTitle(string title) { gridTitleLabel.Text = title; }
        internal void SetGridStatus(bool enabled) { dataGridView1.Enabled = enabled; }

        internal void ShowMovieGrid()
        {
            editMoviePanel.Hide();
            dataGridView1.Left = 0;
            dataGridView1.Width = this.ClientSize.Width;
            dataGridView1.Show();
            gridTitleLabel.Show();
        }

        internal void ShowMovieEditForm(EditMovieViewModel data)
        {
            dataGridView1.Hide();
            gridTitleLabel.Hide();
            editMoviePanel.Left = 0;
            editMoviePanel.Width = this.ClientSize.Width;
            editMoviePanel.Show();

            data.Changed += UpdateEditForm;

            UpdateEditForm(data);
        }

        private void UpdateEditForm(EditMovieViewModel data)
        {
            editFormNameText.Text = data.Name;
            editFormYear.Text = data.Year.ToString();
            editFormDirector.Text = data.Director.Name;

            uploadMovieBox.Image = ImagesHelper.FromFile(data.Image);
            editFormActorsListBox.Items.Clear();
            editFormActorsListBox.Items.AddRange(data.Actors.Select(x => x.Name).ToArray());
        }

        private void UpdateGridView(MoviesGridViewModel data) {
            
            int moviesRowHeight = 100;
            int moviesInCurrentRow = 0;
            DataGridViewRow currentRow = null;
            var cells = new List<TextAndImageCell>();

            dataGridView1.Rows.Clear();

            data.Movies.ForEach(movie =>
            {
                if (moviesInCurrentRow == 0)
                {
                    currentRow = new DataGridViewRow();
                    currentRow.Height = moviesRowHeight;
                    
                }

                var cell = new TextAndImageCell();
                cell.Value = movie.Name;
                cell.Tag = movie;
                cells.Add(cell);
                currentRow.Cells.Add(cell);
                
                moviesInCurrentRow = (moviesInCurrentRow + 1) % moviesInRow;
                if (moviesInCurrentRow == 0)
                {
                    dataGridView1.Rows.Add(currentRow);
                    currentRow = null;
                }
            });

            if (currentRow != null)
            {
                dataGridView1.Rows.Add(currentRow);
            }

            for (int i = 0; i < cells.Count; i++)
            {
                var image = ImagesHelper.FromFile(data.Movies[i].Image);
                cells[i].Image = image;
            }
        }

        internal void SaveEditMovieViewModelState(EditMovieViewModel data, out string directorName, List<string> actorNames)
        {
            data.Name = editFormNameText.Text;
            data.Year = UInt32.Parse(editFormYear.Text);
            directorName = editFormDirector.Text;
            for (int i = 0; i < editFormActorsListBox.Items.Count; i++)
            {
                actorNames.Add(editFormActorsListBox.Items[i].ToString());
            }
        }

        internal void SetEnabledState(bool enabled)
        {
            foreach (Control c in Controls) { c.Enabled = enabled; }
        }

        private void deleteFilmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            DeleteFilm(GetSelectedMovies());
        }

        private void editFilmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFilm(GetSelectedMovies());
        }

        private void findFilmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindFilm();
        }

        private List<Movie> GetSelectedMovies()
        {
            var movies = new List<Movie>();
            foreach (var cell in dataGridView1.SelectedCells)
            {
                var movieCell = cell as TextAndImageCell;
                movies.Add(movieCell.Tag as Movie);
            }
            return movies;
        }

        private void editMovieBackBtn_Click(object sender, EventArgs e)
        {
            if (GoBack != null) { GoBack(); }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AboutOpen != null) { AboutOpen(); }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CatalogView_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Movies", MessageBoxButtons.OKCancel);
            if (result.HasFlag(DialogResult.Cancel))
            {
                e.Cancel = true;
            }
        }

        private void uploadMovieBox_Click(object sender, EventArgs e)
        {
            var selectFile = new OpenFileDialog();
            var result = selectFile.ShowDialog();

            if (!result.HasFlag(DialogResult.Cancel)) {
                var file = selectFile.FileName;
                uploadMovieBox.Image = Image.FromFile(file);
            }
        }

        private void editFormAddActorBtn_Click(object sender, EventArgs e)
        {
            //editFormAddActor.Text
        }

        private void editFormDeleteActor_Click(object sender, EventArgs e)
        {
            if (DeleteActor != null) { DeleteActor(editFormActorsListBox.SelectedIndex); }
        }

        private void editMovieSaveBtn_Click(object sender, EventArgs e)
        {
            if (SaveMovie != null) { SaveMovie(); }
        }
    }
}
