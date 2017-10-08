using FluentAssertions;
using Logging.Verbosity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logging.Tests
{
    [TestClass]
    public class EventTypeTests
    {
        [TestMethod, TestCategory("Unit")]
        public void GivenDefaultEventTypeToStringShouldReturnInformation()
        {
            EventType eventType = new EventType();

            eventType.ToString().Should().Be("Information");
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenWarningEventTypeToStringShouldReturnWarning()
        {
            string typeDescription = new EventTypes().Warning();
            EventType eventType = new EventType(typeDescription);

            eventType.ToString().Should().Be("Warning");
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenErrorEventTypeToStringShouldReturnError()
        {
            string typeDescription = new EventTypes().Error();
            EventType eventType = new EventType(typeDescription);

            eventType.ToString().Should().Be("Error");
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenCriticalEventTypeToStringShouldReturnCritical()
        {
            string typeDescription = new EventTypes().Critical();
            EventType eventType = new EventType(typeDescription);

            eventType.ToString().Should().Be("Critical");
        }
    }
}