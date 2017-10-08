using IntEx = Microsoft.Practices.Unity.InterceptionExtension;

namespace UnityInterception
{
    public class UnityProxy<T> where T : class
    {
        private readonly IntEx.IInterceptionBehavior[] _interceptionBehavior;

        public UnityProxy(): this(new PerformanceMonitor()) { }

        public UnityProxy(params IntEx.IInterceptionBehavior[] interceptionBehavior)
        {
            _interceptionBehavior = interceptionBehavior;
        }

        public T Intercept(T method)
        {
            return IntEx.Intercept.ThroughProxy(method, new IntEx.InterfaceInterceptor(), _interceptionBehavior);
        }
    }
}