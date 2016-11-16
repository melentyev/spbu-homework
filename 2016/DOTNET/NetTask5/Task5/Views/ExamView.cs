using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Task5.Models;

namespace Task5
{
    internal partial class ExamView: Form
    {
        internal delegate void StartExamEventHandler();
        // internal delegate void CancelExamEventHandler();
        internal event StartExamEventHandler StartExam;
        // internal event CancelExamEventHandler CancelExam;

        private const string StrExamFinished = "Exam finished!";

        internal ExamView(ExamViewModel viewModel)
        {
            InitializeComponent();
            viewModel.StateChanged += OnModelStateChanged;
            studentMarksListView.View = View.Details;
            studentMarksListView.Columns.Add("Имя студента");
            studentMarksListView.Columns.Add("Оценка");
            studentMarksListView.Columns[0].Width = 300;
        }

        internal void ExamFinished()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => MessageBox.Show(StrExamFinished)));
            }
            else
            {
                MessageBox.Show(StrExamFinished);
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            StartExam();
        }

        private void OnModelStateChanged(ExamViewModel data)
        {
            
            if (InvokeRequired) {
                Invoke(new Action<ExamViewModel>(HandleModelStateChanged), data);
            }
            else
            {
                HandleModelStateChanged(data);
            }
        }
        private void HandleModelStateChanged(ExamViewModel data)
        {
            var items = data.Students.Select(s => new ListViewItem(new string[] { s.Name, s.MarkString })).ToArray();
            startBtn.Enabled = data.StartButtonActive;
            studentMarksListView.Items.Clear();
            studentMarksListView.Items.AddRange(items);
        }
    }
}
