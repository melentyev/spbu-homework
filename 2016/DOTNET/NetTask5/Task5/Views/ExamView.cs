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
        internal delegate void PauseExamEventHandler();
        internal delegate void ResumeExamEventHandler();
        internal delegate void CancelExamEventHandler();

        internal event StartExamEventHandler StartExam;
        internal event PauseExamEventHandler PauseExam;
        internal event ResumeExamEventHandler ResumeExam;
        internal event CancelExamEventHandler CancelExam;

        private const string StrExamFinished = "Exam finished!";

        internal ExamView(ExamViewModel viewModel)
        {
            InitializeComponent();
            viewModel.StateChanged += OnModelStateChanged;
            studentMarksListView.View = View.Details;
            studentMarksListView.Columns.Add("Имя студента");
            studentMarksListView.Columns.Add("Оценка");
            studentMarksListView.Columns[0].Width = 300;

            OnModelStateChanged(viewModel);
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
            startBtn.Enabled = data.StartButtonActive;
            btnPause.Enabled = data.ButtonPauseActive;
            btnResume.Enabled = data.ButtonResumeActive;
            btnCancel.Enabled = data.ButtonCancelActive;

            labelPassed.Text = data.LabelPassedText;

            var items = data.Students.Select(s => new ListViewItem(new string[] { s.Name, s.MarkString })).ToArray();

            studentMarksListView.Items.Clear();
            studentMarksListView.Items.AddRange(items);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            PauseExam();
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            ResumeExam();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelExam();
        }
    }
}
