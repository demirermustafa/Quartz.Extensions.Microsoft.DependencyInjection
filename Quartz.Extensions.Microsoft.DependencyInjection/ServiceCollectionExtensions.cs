using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz.Impl;
using Quartz.Spi;

namespace Quartz.Extensions.Microsoft.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddJob<TJob>(
            this IServiceCollection services,
            Action<ScheduledJobOptions> setupAction)
            where TJob : IJob
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
           
            if (services.Any(s => s.ServiceType == typeof(TJob)))
                return services;

            TryAddScheduledHostedService(services);

            ScheduledJobOptions scheduledJobOptions = new ScheduledJobOptions();
            setupAction(scheduledJobOptions);
            
            services.Add(new ServiceDescriptor(typeof(TJob), typeof(TJob), ServiceLifetime.Singleton));
            services.AddSingleton(new ScheduledJob(typeof(TJob), scheduledJobOptions.CronExpression, scheduledJobOptions.Description));

            return services;
        }

        private static void TryAddScheduledHostedService(this IServiceCollection services)
        {
            services.TryAddSingleton<IJobFactory, QuartzJobFactory>();
            services.TryAddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<ScheduledHostedService>();
        }
    }
}
