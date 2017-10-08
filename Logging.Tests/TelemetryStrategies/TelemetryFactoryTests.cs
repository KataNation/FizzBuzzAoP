using System;
using System.Collections.Generic;
using FluentAssertions;
using Logging.TelemetryStrategies;
using Logging.Verbosity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logging.Tests.TelemetryStrategies
{
    [TestClass]
    public class TelemetryFactoryTests
    {
        [TestMethod, TestCategory("Functional")]
        public void ShouldWriteEventWithoutThrowingException()
        {
            TelemetryFactory telemetryFactory = new TelemetryFactory();

            ITelemetryStrategy telemetryStrategy = telemetryFactory.Strategy(new EventType(new EventTypes().Information()));

            Action action = () =>
            {
                telemetryStrategy.TrackEvent("source", new Dictionary<string, string>
                {
                    { "key", "value" }
                });
            };

            action.ShouldNotThrow();
        }

        [TestMethod, TestCategory("Functional")]
        public void ShouldWriteExceptionWithoutThrowingException()
        {
            TelemetryFactory telemetryFactory = new TelemetryFactory();

            ITelemetryStrategy telemetryStrategy = telemetryFactory.Strategy(new EventType(new EventTypes().Error()));

            Action action = () =>
            {
                telemetryStrategy.TrackEvent("source", new Dictionary<string, string>
                {
                    { "key", "value" }
                });
            };

            action.ShouldNotThrow();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenInformationEventShouldReturnTelemetryEventStrategy()
        {
            TelemetryFactory telemetryFactory = new TelemetryFactory();

            ITelemetryStrategy telemetryStrategy = telemetryFactory.Strategy(new EventType(new EventTypes().Information()));

            telemetryStrategy.Should().BeOfType<TelemetryEvent>();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenWarningEventShouldReturnTelemetryEventStrategy()
        {
            TelemetryFactory telemetryFactory = new TelemetryFactory();

            ITelemetryStrategy telemetryStrategy = telemetryFactory.Strategy(new EventType(new EventTypes().Warning()));

            telemetryStrategy.Should().BeOfType<TelemetryEvent>();
        }
        
        [TestMethod, TestCategory("Unit")]
        public void GivenErrorEventShouldReturnTelemetryExceptionStrategy()
        {
            TelemetryFactory telemetryFactory = new TelemetryFactory();

            ITelemetryStrategy telemetryStrategy = telemetryFactory.Strategy(new EventType(new EventTypes().Error()));

            telemetryStrategy.Should().BeOfType<TelemetryException>();
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenCriticalEventShouldReturnTelemetryExceptionStrategy()
        {
            TelemetryFactory telemetryFactory = new TelemetryFactory();

            ITelemetryStrategy telemetryStrategy = telemetryFactory.Strategy(new EventType(new EventTypes().Critical()));

            telemetryStrategy.Should().BeOfType<TelemetryException>();
        }
    }
}