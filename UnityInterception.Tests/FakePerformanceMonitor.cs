using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace UnityInterception.Tests
{
    public class FakePerformanceMonitor : IInterceptionBehavior
    {
        public bool Invoked { get; private set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Invoked = true;
            return getNext()(input, getNext);
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute => true;

        public void AssertInvoked()
        {
            Invoked.Should().BeTrue();
        }
    }
}