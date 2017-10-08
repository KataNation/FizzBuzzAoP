using System;
using AopUnityFizzBuzzSpike.Questions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CastleInterception.Tests
{
    [TestClass]
    public class CastleProxyTests
    {
        [TestMethod, TestCategory("Functional")]
        public void GivenDefaultConstructionShouldNotThrow()
        {
            CastleProxy<IQuestion> castleProxy = new CastleProxy<IQuestion>();

            Action action = () =>
            {
                IQuestion question = castleProxy.Interceptor(new Default());
                question.String(1);
            };

            action.ShouldNotThrow();
        }

        [TestMethod, TestCategory("Unit")]
        public void ShouldIntercept()
        {
            //arrange
            FakePerformanceMonitor fakePerformanceMonitor = new FakePerformanceMonitor();
            CastleProxy<IQuestion> castleProxy = new CastleProxy<IQuestion>(fakePerformanceMonitor);

            //act
            IQuestion question = castleProxy.Interceptor(new Default());
            question.String(1);

            //assert
            fakePerformanceMonitor.AssertInvoked();
        }
    }
}