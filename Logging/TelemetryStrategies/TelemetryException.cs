using System;
using System.Collections.Generic;
using Logging.Verbosity;
using Microsoft.ApplicationInsights;

namespace Logging.TelemetryStrategies
{
    public class TelemetryException : ITelemetryStrategy
    {
        private readonly Exception _exception;
        private readonly TelemetryClient _telemetryClient;

        public TelemetryException() : this(new Exception()) { }

        public TelemetryException(Exception exception) : this(exception, new TelemetryClient()) { }

        public TelemetryException(Exception exception, TelemetryClient telemetryClient)
        {
            _exception = exception;
            _telemetryClient = telemetryClient;
        }

        public void TrackEvent(string source, Dictionary<string, string> properties)
        {
            _telemetryClient.TrackException(_exception, properties);
        }

        public bool Responsible(IEventType eventType)
        {
            return eventType.ToString().Equals(new EventTypes().Error()) || eventType.ToString().Equals(new EventTypes().Critical());
        }
    }
}