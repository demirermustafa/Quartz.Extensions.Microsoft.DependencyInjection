using System;
using Microsoft.Extensions.DependencyInjection;

namespace Quartz.Extensions.Microsoft.DependencyInjection
{
    public sealed class ScheduledHostServiceBuilder
    {
        public ScheduledHostServiceBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public IServiceCollection Services { get; }
    }
}