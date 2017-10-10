using System.Linq;
using Logging.Verbosity;

namespace Logging.TelemetryStrategies
{
    public interface ITelemetryFactory
    {
        ITelemetryStrategy Strategy(IEventType eventType);
    }

    public class TelemetryFactory : ITelemetryFactory
    {
        private readonly ITelemetryStrategy[] _strategies;

        public TelemetryFactory() : this(new TelemetryException(), new TelemetryEvent()) { }

        public TelemetryFactory(params ITelemetryStrategy[] strategies)
        {
            _strategies = strategies;
        }

        public ITelemetryStrategy Strategy(IEventType eventType)
        {
            foreach (ITelemetryStrategy strategy in _strategies)
            {
                if (!strategy.Responsible(eventType)) continue;
                return strategy;
            }
            return _strategies.Last();
        }
    }
}