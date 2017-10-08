using System.Collections.Generic;
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
    }
}