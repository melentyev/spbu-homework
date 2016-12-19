using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Task5.Models;
using Task5.Services;

namespace Task5.Controllers
{
    internal sealed class DeaneryController
    {
        const int MaxStudentCount = 8;
        const int MaxMarkSleep = 3 * 1000;

        private SleepManager sleepManager;
        private Random rnd;
        private EventWaitHandle waitHandle = null;
        private ExamViewModel examViewModel;
        private ExamView examView;
        private int studentsCount;
        private List<Student> students = new List<Student>();
        Object passExamLock = new object();

        internal DeaneryController()
        {
            sleepManager = SleepManager.Instance;
            rnd = new Random();
            waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
            examViewModel = new ExamViewModel();
            examView = new ExamView(examViewModel);
            examView.StartExam += ActionStart;
            examView.PauseExam += ActionPause;
            examView.ResumeExam += ActionResume;
            examView.CancelExam += ActionCancel;
        }
        
        internal ExamView Render()
        {
            return examView;
        }

        internal void ActionStart()
        {
            students.Clear();
            examViewModel.Clear();
            examViewModel.ActivityState = ActivityState.Running;
            studentsCount = rnd.Next(5, MaxStudentCount + 1);
            examViewModel.LabelPassedText = "Passed: 0/" + studentsCount.ToString();
            waitHandle.Reset();
            for (int i = 0; i < studentsCount; i++)
            {
                students.Add(new Student(waitHandle, PassExam));
            }
            waitHandle.Set();
        }

        internal void ActionPause()
        {
            sleepManager.PauseAll();
            examViewModel.ActivityState = ActivityState.Paused;
        }
        internal void ActionResume()
        {
            sleepManager.ResumeAll();
            examViewModel.ActivityState = ActivityState.Running;
        }

        internal void ActionCancel()
        {
            students.ForEach(s => s.IsCanceled = true);
            students.Clear();
            sleepManager.CancelAll();
            sleepManager.ResumeAll();
            ResetButtonsState();
        }

        private void ResetButtonsState()
        {
            examViewModel.ActivityState = ActivityState.Default;
        }

        private uint RandomStudentMark()
        {
            return (uint)(1 + rnd.Next(5));
        }

        // in Student thread
        private void PassExam(Student student)
        {
            lock (passExamLock)
            {
                if (student.IsCanceled) { return; }
                examViewModel.AddStudent(student);
                sleepManager.Sleep(rnd.Next(MaxMarkSleep));

                if (student.IsCanceled) { return; }
                student.Mark = RandomStudentMark();
                studentsCount--;
                examViewModel.LabelPassedText = "Passed: " + (students.Count - studentsCount).ToString() +
                    "/" + students.Count.ToString();
                if (studentsCount == 0)
                {
                    examView.ExamFinished();
                    ResetButtonsState();
                }
            }
        }
    }
}
