using Castle.DynamicProxy;
using FluentAssertions;

namespace CastleInterception.Tests
{
    public class FakePerformanceMonitor : IInterceptor
    {
        public bool Invoked { get; private set; }

        public void Intercept(IInvocation invocation)
        {
            Invoked = true;
        }

        public void AssertInvoked()
        {
            Invoked.Should().BeTrue();
        }
    }
}