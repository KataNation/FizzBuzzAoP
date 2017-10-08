using System.Collections.Generic;
using System.Linq;
using FizzBuzz.Shared.Fakes;
using FluentAssertions;
using Logging.Authors;
using Logging.Destinations;
using Logging.Verbosity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logging.Tests
{
    [TestClass]
    public class EventLogTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenAiTelemetryAndDefaultEntryTypeShouldWriteEntry()
        {
            const string source = "source";
            const string message = "message";
            EventType eventType = new EventType();

            LogAuthorFake logAuthorFake = new LogAuthorFake();
            ApplicationInsightsLog applicationInsightsLog = new ApplicationInsightsLog(logAuthorFake);

            applicationInsightsLog.WriteEntry(source, message, eventType);

            logAuthorFake.LogEntries().Count.Should().Be(1);
            logAuthorFake.LogEntries().First().Source().Should().Be(source);
            logAuthorFake.LogEntries().First().Message().Should().Be(message);
            logAuthorFake.LogEntries().First().EventType().Should().Be(eventType);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenWindowsEventLogAndDefaultEntryTypeShouldWriteEntry()
        {
            const string source = "source";
            const string message = "message";
            EventType eventType = new EventType();

            LogAuthorFake logAuthorFake = new LogAuthorFake();
            WindowsEventLog windowsEventLog = new WindowsEventLog(logAuthorFake);

            windowsEventLog.WriteEntry(source, message, eventType);

            logAuthorFake.LogEntries().Count.Should().Be(1);
            logAuthorFake.LogEntries().First().Source().Should().Be(source);
            logAuthorFake.LogEntries().First().Message().Should().Be(message);
            logAuthorFake.LogEntries().First().EventType().Should().Be(eventType);
        }
    }

    public class LogAuthorFake : ILogAuthor
    {
        private readonly List<LogEntry> _logEntries = new List<LogEntry>();

        public List<LogEntry> LogEntries()
        {
            return _logEntries;
        }

        public void WriteEntry(string source, string message, IEventType eventType)
        {
            _logEntries.Add(new LogEntry(source, message, eventType));
        }
    }
}