using System.Collections.Generic;
using Logging.Verbosity;

namespace Logging.TelemetryStrategies
{
    public interface ITelemetryStrategy
    {
        void TrackEvent(string source, Dictionary<string, string> properties);
        bool Responsible(IEventType eventType);
    }
}