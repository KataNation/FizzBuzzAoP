using System.Collections.Generic;
using Logging.Verbosity;
using Microsoft.ApplicationInsights;

namespace Logging.TelemetryStrategies
{
    public class TelemetryEvent : ITelemetryStrategy
    {
        private readonly TelemetryClient _telemetryClient;

        public TelemetryEvent() : this(new TelemetryClient()) { }

        public TelemetryEvent(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public void TrackEvent(string source, Dictionary<string, string> properties)
        {
            _telemetryClient.TrackEvent(source, properties);
        }

        public bool Responsible(IEventType eventType)
        {
            return eventType.ToString().Equals(new EventTypes().Information()) || eventType.ToString().Equals(new EventTypes().Warning());
        }
    }
}