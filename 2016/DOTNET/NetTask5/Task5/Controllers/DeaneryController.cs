using System;
using System.Threading;
using System.Threading.Tasks;
using Task5.Models;

namespace Task5.Controllers
{
    internal sealed class DeaneryController
    {
        const int MaxStudentCount = 5;
        const int MaxMarkSleep = 3 * 1000;

        private Random rnd;
        private EventWaitHandle waitHandle = null;
        private ExamViewModel examViewModel;
        private ExamView examView;
        private int studentsCount;
        Object passExamLock = new object();

        internal DeaneryController()
        {
            rnd = new Random();
            waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
            examViewModel = new ExamViewModel();
            examView = new ExamView(examViewModel);
            examView.StartExam += ActionStart;
        }
        
        internal ExamView Render()
        {
            return examView;
        }

        internal void ActionStart()
        {
            examViewModel.Clear();
            examViewModel.StartButtonActive = false;
            studentsCount = rnd.Next(5, MaxStudentCount + 1);
            waitHandle.Reset();
            for (int i = 0; i < studentsCount; i++)
            {
                new Student(waitHandle, PassExam);
            }
            waitHandle.Set();
        }

        internal uint RandomStudentMark()
        {
            return (uint)(1 + rnd.Next(5));
        }

        private void PassExam(Student student)
        {
            lock (passExamLock)
            {
                examViewModel.AddStudent(student);
                Thread.Sleep(rnd.Next(MaxMarkSleep));
                student.Mark = RandomStudentMark();
                studentsCount--;
                if (studentsCount == 0)
                {
                    examView.ExamFinished();
                    examViewModel.StartButtonActive = true;
                }
            }
        }
    }
}
