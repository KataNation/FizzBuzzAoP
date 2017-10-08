using System;
using System.Diagnostics;
using Logging.Verbosity;

namespace Logging.Authors
{
    public class LogAuthorAuthor : ILogAuthor
    {
        private readonly EventLog _eventLog;

        public LogAuthorAuthor() : this(new EventLog()) { }

        public LogAuthorAuthor(EventLog eventLog)
        {
            _eventLog = eventLog;
        }

        public void WriteEntry(string source, string message, IEventType eventType)
        {
            _eventLog.Source = source;
            _eventLog.WriteEntry(message, (EventLogEntryType) Enum.Parse(typeof(EventLogEntryType), eventType.ToString()));
        }
    }
}