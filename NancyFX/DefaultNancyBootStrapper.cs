using Nancy;
using Nancy.Configuration;

namespace NancyFX {
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        public override void Configure(INancyEnvironment environment)
        {
            base.Configure(environment);
            environment.Tracing(enabled: false, displayErrorTraces: true);
        }
    }
}
