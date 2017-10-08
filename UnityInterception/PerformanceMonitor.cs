using System;
using System.Collections.Generic;
using Logging;
using Logging.Destinations;
using Logging.Verbosity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace UnityInterception
{
    public class PerformanceMonitor : IInterceptionBehavior
    {
        private readonly ILog[] _log;

        public PerformanceMonitor() : this(new ApplicationInsightsLog()) { }

        public PerformanceMonitor(params ILog[] log)
        {
            _log = log;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            WriteCurrentTime(input);
            return getNext()(input, getNext);
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute => true;

        private void WriteCurrentTime(IMethodInvocation input)
        {
            TimeSpan timeOfDay = DateTime.Now.TimeOfDay;

            foreach (ILog log in _log)
            {
                log.WriteEntry($"{input.Target.GetType()}.{input.MethodBase.Name}",
                    $"{input.Target.GetType()}.{input.MethodBase.Name} was called at {timeOfDay.Hours}:{timeOfDay.Minutes}:{timeOfDay.Seconds}.{timeOfDay.Milliseconds}.",
                    new EventType());
            }
        }
    }
}