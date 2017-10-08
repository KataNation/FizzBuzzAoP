using System;
using AopUnityFizzBuzzSpike.Controllers;
using AopUnityFizzBuzzSpike.Questions;
using FizzBuzz.Shared.Fakes;
using FluentAssertions;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AopUnityFizzBuzzSpike.Tests.Controllers
{
    [TestClass]
    public class FizzBuzzUnityControllerTest
    {
        private IFizzBuzzController _fizzBuzzController;
        private LogFake _logFake;

        [TestInitialize]
        public void Setup()
        {
            _logFake = new LogFake();

            UnityInterception.PerformanceMonitor performanceMonitor = new UnityInterception.PerformanceMonitor(_logFake);

            IQuestion defaultQuestion = Intercept.ThroughProxy<IQuestion>(new Default(), new InterfaceInterceptor(), new IInterceptionBehavior[] { performanceMonitor });
            IQuestion fizzQuestion = Intercept.ThroughProxy<IQuestion>(new Fizz(defaultQuestion), new InterfaceInterceptor(), new IInterceptionBehavior[] { performanceMonitor });
            IQuestion buzzQuestion = Intercept.ThroughProxy<IQuestion>(new Buzz(fizzQuestion), new InterfaceInterceptor(), new IInterceptionBehavior[] { performanceMonitor });
            IQuestion fizzBuzzQuestion = Intercept.ThroughProxy<IQuestion>(new Questions.FizzBuzz(buzzQuestion), new InterfaceInterceptor(), new IInterceptionBehavior[] { performanceMonitor });

            _fizzBuzzController = Intercept.ThroughProxy<IFizzBuzzController>(
                new FizzBuzzUnityController(fizzBuzzQuestion),
                new InterfaceInterceptor(),
                new IInterceptionBehavior[]
                {
                    new UnityInterception.PerformanceMonitor(_logFake)
                });
        }

        [TestMethod, TestCategory("Functional")]
        public void GivenOneAndDefaultConstructionShouldNotThrow()
        {
            FizzBuzzUnityController fizzBuzzUnityController = new FizzBuzzUnityController();

            string actual = string.Empty;
            Action action = () =>
            {
                actual = fizzBuzzUnityController.Get(1);
            };

            action.ShouldNotThrow();
            actual.Should().Be("1");
        }

        [TestMethod]
        public void GivenEightShouldReturnNumberAsString()
        {
            _fizzBuzzController.Get(8).Should().Be("8");
            _logFake.LogEntries.Count.Should().Be(5);
        }

        [TestMethod]
        public void GivenThirtyThreeShouldReturnFizz()
        {

            _fizzBuzzController.Get(33).Should().Be("fizz");
            _logFake.LogEntries.Count.Should().Be(4);
        }

        [TestMethod]
        public void GivenFiftyShouldReturnBuzz()
        {

            _fizzBuzzController.Get(50).Should().Be("buzz");
            _logFake.LogEntries.Count.Should().Be(3);
        }

        [TestMethod]
        public void GivenSixtyShouldReturnFizzBuzz()
        {

            _fizzBuzzController.Get(60).Should().Be("fizzbuzz");
            _logFake.LogEntries.Count.Should().Be(2);
        }
    }
}
