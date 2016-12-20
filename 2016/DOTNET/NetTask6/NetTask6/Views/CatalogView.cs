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
    internal sealed partial class CatalogView : Form
    {
        internal delegate void RefillDatabaseEventHandler();
        internal delegate void EditFilmEventHandler(List<Movie> selected);
        internal delegate void DeleteFilmEventHandler(List<Movie> selected);
        internal delegate void FindFilmEventHandler();
        internal delegate void GoBackEventHandler();
        internal delegate void AboutOpenEventHandler();
        internal delegate void DeleteActorEventHandler(int position);
        internal delegate void SaveMovieEventHandler();

        internal event RefillDatabaseEventHandler RefillDatabase;
        internal event EditFilmEventHandler EditFilm;
        internal event DeleteFilmEventHandler DeleteFilm;
        internal event FindFilmEventHandler FindFilm;
        internal event GoBackEventHandler GoBack;
        internal event AboutOpenEventHandler AboutOpen;
        internal event DeleteActorEventHandler DeleteActor;
        internal event SaveMovieEventHandler SaveMovie;

        const int moviesInRow = 2;

        EditMovieViewModel editMovieViewModel;

        internal ToolStripMenuItem ExitMenuItem { get { return exitToolStripMenuItem; } }
        internal ToolStripMenuItem FindMovieMenuItem { get { return findFilmToolStripMenuItem; } }

        internal CatalogView(
            MoviesGridViewModel data, 
            IAutocompleteSource directorsAutocompleteSource,
            IAutocompleteSource actorsAutocompleteSource)
        {
            InitializeComponent();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            var imageColumn = new DataGridViewImageColumn();
            var nameColumn = new DataGridViewTextBoxColumn();
            var yearColumn = new DataGridViewTextBoxColumn();

            dataGridView1.Columns.Add(imageColumn);
            dataGridView1.Columns.Add(nameColumn);
            dataGridView1.Columns.Add(yearColumn);

            imageColumn.Resizable = DataGridViewTriState.False;
            nameColumn.Resizable = DataGridViewTriState.False;
            yearColumn.Resizable = DataGridViewTriState.False;

            imageColumn.ReadOnly = true;
            nameColumn.ReadOnly = true;
            yearColumn.ReadOnly = true;

            editFormYear.SetErrorProvider(editMovieFormErrorProvider);

            editFormDirector.SetSource(directorsAutocompleteSource);
            editFormAddActor.SetSource(actorsAutocompleteSource);

            editMovieFormHelpProvider.SetShowHelp(editFormNameText, true);
            editMovieFormHelpProvider.SetHelpString(editFormNameText, "Название фильма");

            data.Changed += UpdateGridView;
            UpdateGridView(data);
            ShowMovieGrid();

            OnCatalogViewResize(null, null);
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
            editMovieViewModel = data;

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
            
            int moviesRowHeight = 200;
            DataGridViewRow currentRow = null;
            var cells = new List<DataGridViewImageCell>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                ((row.Cells[0] as DataGridViewImageCell).Value as Image).Dispose();
            }
            dataGridView1.Rows.Clear();

            data.Movies.ForEach(movie =>
            {
                currentRow = new DataGridViewRow();
                currentRow.Height = moviesRowHeight;
                currentRow.Tag = movie;

                var imageCell = new DataGridViewImageCell();
                var nameCell = new DataGridViewTextBoxCell() { Value = movie.Name };
                var yearCell = new DataGridViewTextBoxCell() { Value = movie.Year };

                cells.Add(imageCell);

                currentRow.Cells.Add(imageCell);
                currentRow.Cells.Add(nameCell);
                currentRow.Cells.Add(yearCell);

                dataGridView1.Rows.Add(currentRow);
                    currentRow = null;
                
            });

            if (currentRow != null)
            {
                dataGridView1.Rows.Add(currentRow);
            }

            for (int i = 0; i < cells.Count; i++)
            {
                var image = ImagesHelper.FromFile(data.Movies[i].Image);
                cells[i].Value = image;
            }
        }

        internal void SaveEditMovieViewModelState(EditMovieViewModel data, out string directorName, List<string> actorNames)
        {
            data.Name = editFormNameText.Text;
            data.Year = Int32.Parse(editFormYear.Text);
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

            if (DeleteFilm != null) { DeleteFilm(GetSelectedMovies()); }
        }

        private void OnEditFilmToolStripMenuItemClick(object sender, EventArgs e)
        {
            EditFilm(GetSelectedMovies());
        }

        private void OnFindFilmToolStripMenuItemClick(object sender, EventArgs e)
        {
            FindFilm();
        }

        private List<Movie> GetSelectedMovies()
        {
            var movies = new List<Movie>();
            foreach (var row in dataGridView1.SelectedRows)
            {
                var movieRow = row as DataGridViewRow;
                movies.Add(movieRow.Tag as Movie);
            }
            return movies;
        }

        private void OnEditMovieBackBtnClick(object sender, EventArgs e)
        {
            if (GoBack != null) { GoBack(); }
        }

        private void OnAboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (AboutOpen != null) { AboutOpen(); }
        }

        private void OnExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnCatalogViewFormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Movies", MessageBoxButtons.OKCancel);
            if (result.HasFlag(DialogResult.Cancel))
            {
                e.Cancel = true;
            }
        }

        private void OnUploadMovieBoxClick(object sender, EventArgs e)
        {
            var selectFile = new OpenFileDialog();
            var result = selectFile.ShowDialog();

            if (!result.HasFlag(DialogResult.Cancel)) {
                var file = selectFile.FileName;
                uploadMovieBox.Image = Image.FromFile(file);

                editMovieViewModel.Image = file;
            }
        }

        private void OnEditFormAddActorBtnClick(object sender, EventArgs e)
        {
            //editFormAddActor.Text
        }

        private void OnEditFormDeleteActorClick(object sender, EventArgs e)
        {
            if (DeleteActor != null) { DeleteActor(editFormActorsListBox.SelectedIndex); }
        }

        private void OnEditMovieSaveBtnClick(object sender, EventArgs e)
        {
            if (SaveMovie != null) { SaveMovie(); }
        }

        private void OnRefillDatabaseToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (RefillDatabase != null) { RefillDatabase(); }
        }

        private void OnCatalogViewResize(object sender, EventArgs e)
        {
            int dgwidth = dataGridView1.Width - dataGridView1.RowHeadersWidth * 2;
            dataGridView1.Columns[0].Width = dgwidth / 3;
            dataGridView1.Columns[1].Width = dgwidth / 3;
            dataGridView1.Columns[2].Width = dgwidth / 3;
        }
    }
}
