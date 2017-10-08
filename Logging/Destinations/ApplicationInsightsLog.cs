using Logging.Authors;
using Logging.Verbosity;

namespace Logging.Destinations
{
    public class ApplicationInsightsLog : ILog
    {
        private readonly ILogAuthor _logAuthor;

        public ApplicationInsightsLog() : this(new TelemetryAuthor()) { }

        public ApplicationInsightsLog(ILogAuthor logAuthor)
        {
            _logAuthor = logAuthor;
        }

        public void WriteEntry(string source, string message, IEventType eventType)
        {
            _logAuthor.WriteEntry(source, message, eventType);
        }
    }
}