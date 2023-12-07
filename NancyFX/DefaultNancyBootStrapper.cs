using Nancy;
using Nancy.Configuration;

namespace NancyFX {
    // CustomBootstrapper class inherits from DefaultNancyBootstrapper
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        // Override the Configure method to set up the environment for Nancy
        public override void Configure(INancyEnvironment environment)
        {
            // Call the base class's Configure method
            base.Configure(environment);

            // Configure the environment for Nancy
            // Tracing is disabled for performance, but error traces are enabled for debugging
            environment.Tracing(enabled: false, displayErrorTraces: true);
        }
    }
}
