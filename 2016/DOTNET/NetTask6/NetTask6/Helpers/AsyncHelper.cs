using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTask6.Helpers
{
    class AsyncHelper<T>
    {
        internal delegate void OnStartedEventHandler();
        internal delegate void OnCompletedEventHandler(T result);
        internal event OnStartedEventHandler OnStarted;
        internal event OnCompletedEventHandler OnCompleted;
        protected void Started() { if (OnStarted != null) { OnStarted(); } }
        protected void Completed(T result) { if (OnCompleted != null) { OnCompleted(result); } }
    }
}
