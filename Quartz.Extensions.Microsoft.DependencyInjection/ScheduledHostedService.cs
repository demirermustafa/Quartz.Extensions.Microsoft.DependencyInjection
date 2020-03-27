using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz.Spi;

namespace Quartz.Extensions.Microsoft.DependencyInjection
{
    public class ScheduledHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<ScheduledJob> _jobSchedules;

        public ScheduledHostedService(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
            IEnumerable<ScheduledJob> jobSchedules)
        {
            _schedulerFactory = schedulerFactory;
            _jobSchedules = jobSchedules;
            _jobFactory = jobFactory;
        }

        private IScheduler Scheduler { get; set; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            foreach (var jobSchedule in _jobSchedules)
            {
                await ScheduleJob(cancellationToken, jobSchedule);
            }

            await Scheduler.Start(cancellationToken);
        }

        private async Task ScheduleJob(CancellationToken cancellationToken, ScheduledJob scheduledJob)
        {
            var job = CreateJob(scheduledJob);
            var trigger = CreateTrigger(scheduledJob);

            await Scheduler.ScheduleJob(job, trigger, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (Scheduler != null) await Scheduler.Shutdown(cancellationToken);
        }

        private IJobDetail CreateJob(ScheduledJob schedule)
        {
            var jobType = schedule.Type;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();
        }

        private ITrigger CreateTrigger(ScheduledJob schedule)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.Type.FullName}.trigger")
                .WithCronSchedule(schedule.CronExpression)
                .WithDescription(schedule.Description ?? schedule.Type.FullName)
                .Build();
        }
    }
}
