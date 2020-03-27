namespace Quartz.Extensions.Microsoft.DependencyInjection
{
    public class ScheduledJobOptions
    {
        public string CronExpression { get; set; }

        public string Description { get; set; }
    }
}