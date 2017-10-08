using System;
using AopUnityFizzBuzzSpike.Questions;
using FizzBuzz.Shared.Fakes;
using FluentAssertions;
using Logging.Destinations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnityInterception.Tests
{
    [TestClass]
    public class UnityProxyTests
    {
        [TestMethod, TestCategory("Functional")]
        public void GivenRealPerformanceMonitorWithWindowsEventLogShouldInterceptAndNotThrow()
        {
            UnityProxy<IQuestion> unityProxy = new UnityProxy<IQuestion>(new PerformanceMonitor(new WindowsEventLog()));

            Action action = () =>
            {
                IQuestion question = unityProxy.Intercept(new AopUnityFizzBuzzSpike.Questions.FizzBuzz());
                question.String(1);
            };

            action.ShouldNotThrow();
        }

        [TestMethod, TestCategory("Functional")]
        public void GivenRealPerformanceMonitorWithApplicationInsightsLogShouldInterceptAndNotThrow()
        {
            UnityProxy<IQuestion> unityProxy = new UnityProxy<IQuestion>(new PerformanceMonitor(new ApplicationInsightsLog()));

            Action action = () =>
            {
                IQuestion question = unityProxy.Intercept(new AopUnityFizzBuzzSpike.Questions.FizzBuzz());
                question.String(1);
            };

            action.ShouldNotThrow();
        }

        [TestMethod, TestCategory("Functional")]
        public void GivenRealPerformanceMonitorShouldNotThrow()
        {
            UnityProxy<IQuestion> unityProxy = new UnityProxy<IQuestion>();

            Action action = () =>
            {
                IQuestion question = unityProxy.Intercept(new AopUnityFizzBuzzSpike.Questions.FizzBuzz());
                question.String(1);
            };

            action.ShouldNotThrow();
        }

        [TestMethod, TestCategory("CodeIntegration")]
        public void GivenRealPerformanceMonitorShouldInterceptAndLogEntry()
        {
            LogFake logFake = new LogFake();
            UnityProxy<IQuestion> unityProxy = new UnityProxy<IQuestion>(new PerformanceMonitor(logFake));

            IQuestion question = unityProxy.Intercept(new AopUnityFizzBuzzSpike.Questions.FizzBuzz());
            question.String(1);

            logFake.LogEntries.Count.Should().Be(1);
        }

        [TestMethod, TestCategory("Unit")]
        public void ShouldIntercept()
        {
            //arrange
            FakePerformanceMonitor fakePerformanceMonitor = new FakePerformanceMonitor();
            UnityProxy<IQuestion> unityProxy = new UnityProxy<IQuestion>(fakePerformanceMonitor);

            //act
            IQuestion question = unityProxy.Intercept(new AopUnityFizzBuzzSpike.Questions.FizzBuzz());
            question.String(1);

            //assert
            fakePerformanceMonitor.AssertInvoked();
        }
    }
}