using System.Collections.Generic;
using Logging;
using Logging.Destinations;
using Logging.Verbosity;

namespace FizzBuzz.Shared.Fakes
{
    public class LogFake : ILog
    {
        public List<LogEntry> LogEntries = new List<LogEntry>();

        public void WriteEntry(string source, string message, IEventType eventType)
        {
            LogEntries.Add(new LogEntry(source, message, eventType));
        }
    }
}