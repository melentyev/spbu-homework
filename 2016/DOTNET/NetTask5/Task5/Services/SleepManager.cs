using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace Task5.Services
{
    internal class SleepManager
    {
        const long NotPaused = -1;

        private static SleepManager instance;

        internal static SleepManager Instance { get { return instance; } }
        static SleepManager()
        {
            instance = new SleepManager();
        }

        internal SleepManager()
        {
            Start();
        }

        private Stopwatch sw = new Stopwatch();
        private long pauseStart = NotPaused;
        private bool isRunning;
        private List<Tuple<long, EventWaitHandle> > wakeupEvents = 
            new List<Tuple<long, EventWaitHandle> >();
        private object sleepManagerLock = new object();

        private void Start()
        {
            sw.Start();
            new Thread(Run).Start();
        }

        private void Run()
        {
            isRunning = true;
            while (isRunning)
            {
                lock (sleepManagerLock)
                {
                    if (!IsPaused())
                    {
                        for (int i = 0; i < wakeupEvents.Count; i++)
                        {
                            var we = wakeupEvents[i];
                            if (we.Item1 <= sw.ElapsedMilliseconds)
                            {
                                wakeupEvents.RemoveAt(i);
                                we.Item2.Set();
                            }
                        }
                    }
                }
                Thread.Sleep(30);
            }
        }

        // Call from other thread
        internal void Sleep(int ms)
        {
            var handle = new EventWaitHandle(false, EventResetMode.ManualReset);
            handle.Reset();
            lock (sleepManagerLock) {
                long timeEnd = sw.ElapsedMilliseconds + ms;
                wakeupEvents.Add(Tuple.Create(timeEnd, handle));
            }
            handle.WaitOne();
        }

        internal void PauseAll()
        {
            pauseStart = sw.ElapsedMilliseconds;
        }

        internal void ResumeAll()
        {
            lock (sleepManagerLock)
            {
                long pauseTotal = sw.ElapsedMilliseconds - pauseStart;
                for (int i = 0; i < wakeupEvents.Count; i++)
                {
                    wakeupEvents[i] = Tuple.Create(wakeupEvents[i].Item1 + pauseTotal, wakeupEvents[i].Item2);
                }
                pauseStart = NotPaused;
            }
        }

        internal void CancelAll()
        {
            lock (sleepManagerLock)
            {
                for (int i = 0; i < wakeupEvents.Count; i++)
                {
                    wakeupEvents[i].Item2.Set();
                }
                wakeupEvents.Clear();
            }
        }
        internal bool IsPaused()
        {
            return pauseStart != NotPaused;
        }
    }
}
