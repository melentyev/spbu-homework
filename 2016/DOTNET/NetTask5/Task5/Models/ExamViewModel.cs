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
            get
            {
                return startButtonActive;
            }
            set
            {
                startButtonActive = value;
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
