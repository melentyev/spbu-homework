using System.Collections.Generic;

namespace Task5.Models
{
    internal sealed class ExamViewModel
    {
        internal delegate void StateChangedEventHandler(ExamViewModel data);
        internal event StateChangedEventHandler StateChanged;

        private bool startButtonActive = true;
        internal bool StartButtonActive
        {
            get { return startButtonActive; }
            set
            {
                startButtonActive = value;
                StateChanged(this);
            }
        }

        private bool buttonPauseActive = false;
        internal bool ButtonPauseActive
        {
            get { return buttonPauseActive; }
            set
            {
                buttonPauseActive = value;
                StateChanged(this);
            }
        }

        private bool buttonResumeActive = false;
        internal bool ButtonResumeActive
        {
            get { return buttonResumeActive; }
            set
            {
                buttonResumeActive = value;
                StateChanged(this);
            }
        }

        private bool buttonCancelActive = false;
        internal bool ButtonCancelActive
        {
            get { return buttonCancelActive; }
            set
            {
                buttonCancelActive = value;
                StateChanged(this);
            }
        }

        private string labelPassedText = "";
        internal string LabelPassedText
        {
            get { return labelPassedText; }
            set
            {
                labelPassedText = value;
                StateChanged(this);
            }
        }

        private List<Student> students;
        internal IEnumerable<Student> Students { get { return students; } }

        internal ExamViewModel()
        {
            students = new List<Student>();
        }

        internal void AddStudent(Student student)
        {
            students.Add(student);
            student.StateChanged += (_) => StateChanged(this);
            StateChanged(this);
        }
        internal void Clear()
        {
            students.Clear();
            startButtonActive = true;
            StateChanged(this);
        }
    }
}
