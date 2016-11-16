using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task5.Controllers;

namespace Task5.Models
{
    internal sealed class Student
    {
        internal delegate void StateChangedEventHandler(Student data);
        internal event StateChangedEventHandler StateChanged;

        const int MaxStudentSleepTime = 1;

        private string name;
        private static Random rnd = new Random();
        private UInt32? mark;

        private static string[] firstNames = new string[] { "Вася", "Петя", "Коля", "Макар", "Захар", "Гоша", "Витя" };
        private static string[] lastNames = new string[] { "Васильев", "Петров", "Николаев", "Макаров", "Захаров", "Григорьев" };
        
        internal string Name { get { return name; } }

        internal UInt32? Mark
        {
            get
            {
                return mark;
            }
            set
            {
                mark = value;
                StateChanged(this);
            }
        }

        internal String MarkString
        {
            get
            {
                return mark.HasValue ? mark.ToString() : "";
            }
        }
        
        internal Student(EventWaitHandle waitHandle, Action<Student> passExam)
        {
            name = RandomName();
            mark = null;
            new Thread(() =>
            {
                waitHandle.WaitOne();
                Thread.Sleep(RandomSleepTime());
                passExam(this);
            }).Start();
        }

        private string RandomName()
        {    
            return firstNames[rnd.Next(firstNames.Length)] + " " + lastNames[rnd.Next(lastNames.Length)];
        }

        private int RandomSleepTime()
        {
            return rnd.Next(0, MaxStudentSleepTime * 1000);
        }
    }
}
