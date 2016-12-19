using System.Collections.Generic;

namespace Task5.Models
{
    internal enum ActivityState
    {
        Default,
        Running,
        Paused
    }

    internal sealed class ExamViewModel
    {
        
        internal delegate void StateChangedEventHandler(ExamViewModel data);
        internal event StateChangedEventHandler StateChanged;

        private ActivityState activityState;
        internal ActivityState ActivityState
        {
            get { return activityState; }
            set { activityState = value; StateChanged(this); }
        }

        internal bool StartButtonActive
        {
            get { return activityState == ActivityState.Default; }
        }

        internal bool ButtonPauseActive
        {
            get { return activityState == ActivityState.Running; }
        }

        internal bool ButtonResumeActive
        {
            get { return activityState == ActivityState.Paused; }
        }

        internal bool ButtonCancelActive
        {
            get { return activityState == ActivityState.Running || activityState == ActivityState.Paused; }
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
            activityState = ActivityState.Default;
            StateChanged(this);
        }
    }
}
