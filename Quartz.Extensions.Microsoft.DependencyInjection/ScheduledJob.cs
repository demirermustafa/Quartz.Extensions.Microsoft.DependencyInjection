using System;

namespace Quartz.Extensions.Microsoft.DependencyInjection
{
    public class ScheduledJob
    {
        public ScheduledJob(Type type, string cronExpression, string description = null)
        {
            Type = type;
            CronExpression = cronExpression;
            Description = description;
        }

        public Type Type { get; }

        public string CronExpression { get; }

        public string Description { get; }
    }
}
