using System;
using AopUnityFizzBuzzSpike.Controllers;
using AopUnityFizzBuzzSpike.Questions;
using Castle.DynamicProxy;
using CastleInterception;
using FizzBuzz.Shared.Fakes;
using FluentAssertions;
using Logging;
using Logging.Verbosity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzzSpike.CastleWindsor.Tests.Controllers
{
    [TestClass]
    public class FizzBuzzCastleControllerTests
    {
        private IFizzBuzzController _fizzBuzzController;
        private LogFake _logFake;

        [TestInitialize]
        public void Setup()
        {
            _logFake = new LogFake();

            _fizzBuzzController = new FizzBuzzCastleController(new CastleProxy<IQuestion>(_logFake));
        }

        [TestMethod, TestCategory("Functional")]
        public void GivenDefaultConstructionShouldNotThrow()
        {
            FizzBuzzCastleController fizzBuzzCastleController = new FizzBuzzCastleController();
            string actual = string.Empty;
            Action action = () =>
            {
                actual = fizzBuzzCastleController.Get(1);
            };

            action.ShouldNotThrow();
            actual.Should().Be("1");
        }

        [TestMethod, TestCategory("CodeIntegration")]
        public void GivenFakeLogShouldLogPerformance()
        {
            FizzBuzzCastleController fizzBuzzCastleController = new FizzBuzzCastleController(_logFake);

            fizzBuzzCastleController.Get(15);

            _logFake.LogEntries.Count.Should().Be(2);
        }

        [TestMethod, TestCategory("Unit")]
        public void GivenExceptionShouldLogExceptionDetails()
        {

            IQuestion interfaceProxyWithTarget = (IQuestion) new ProxyGenerator()
                .CreateInterfaceProxyWithTarget(typeof(IQuestion), new FakeFizzBuzzQuestion(), new PerformanceMonitor(_logFake));

            FizzBuzzCastleController fizzBuzzCastleController = new FizzBuzzCastleController(interfaceProxyWithTarget);

            Action action = () =>
            {
                fizzBuzzCastleController.Get(1);
            };

            action.ShouldThrow<Exception>();

            _logFake.LogEntries.Count.Should().Be(3);
            _logFake.LogEntries[1].EventType().ToString().Should().Be(new EventTypes().Error());
        }

        [TestMethod]
        public void GivenEightShouldReturnNumberAsString()
        {
            _fizzBuzzController.Get(8).Should().Be("8");
            _logFake.LogEntries.Count.Should().Be(8);
        }

        [TestMethod]
        public void GivenThirtyThreeShouldReturnFizz()
        {

            _fizzBuzzController.Get(33).Should().Be("fizz");
            _logFake.LogEntries.Count.Should().Be(6);
        }

        [TestMethod]
        public void GivenFiftyShouldReturnBuzz()
        {

            _fizzBuzzController.Get(50).Should().Be("buzz");
            _logFake.LogEntries.Count.Should().Be(4);
        }

        [TestMethod]
        public void GivenSixtyShouldReturnFizzBuzz()
        {

            _fizzBuzzController.Get(60).Should().Be("fizzbuzz");
            _logFake.LogEntries.Count.Should().Be(2);
        }
    }

    public class FakeFizzBuzzQuestion : IQuestion
    {
        public string String(int input)
        {
            throw new Exception("FizzBuzz blew up.");
        }
    }
}