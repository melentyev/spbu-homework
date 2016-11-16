using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace Task5.Services
{
    class SleepManager
    {
        private static Stopwatch sw;
        private static long pauseStart;
        private static bool isRunning;
        private static List<Tuple<long, EventWaitHandle> > wakeupEvents;
        private static object sleepManagerLock;

        public static void Start()
        {
            pauseStart = -1;
            sleepManagerLock = new object();
            sw = new Stopwatch();
            sw.Start();
            wakeupEvents = new List<Tuple<long, EventWaitHandle> >();

            new Thread(Run).Start();
        }

        private static void Run()
        {
            isRunning = true;
            while (isRunning)
            {
                lock (sleepManagerLock)
                {
                    if (pauseStart == -1)
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
        public static void Sleep(int ms)
        {
            var handle = new EventWaitHandle(false, EventResetMode.ManualReset);
            handle.Reset();
            lock (sleepManagerLock) {
                long timeEnd = sw.ElapsedMilliseconds + ms;
                wakeupEvents.Add(Tuple.Create(timeEnd, handle));
            }
            handle.WaitOne();
        }

        public static void PauseAll()
        {
            pauseStart = sw.ElapsedMilliseconds;
        }

        public static void ResumeAll()
        {
            lock (sleepManagerLock)
            {
                long pauseTotal = sw.ElapsedMilliseconds - pauseStart;
                for (int i = 0; i < wakeupEvents.Count; i++)
                {
                    wakeupEvents[i] = Tuple.Create(wakeupEvents[i].Item1 + pauseTotal, wakeupEvents[i].Item2);
                }
                pauseStart = -1;
            }
        }

        public static void CancelAll()
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
    }
}
