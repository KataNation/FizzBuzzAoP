using Logging;
using Logging.Verbosity;

namespace FizzBuzz.Shared.Fakes
{
    public class LogEntry
    {
        private readonly string _source;
        private readonly string _message;
        private readonly IEventType _eventType;

        public LogEntry(string source, string message, IEventType eventType)
        {
            _source = source;
            _message = message;
            _eventType = eventType;
        }

        public string Source()
        {
            return _source;
        }

        public string Message()
        {
            return _message;
        }

        public IEventType EventType()
        {
            return _eventType;
        }
    }
}