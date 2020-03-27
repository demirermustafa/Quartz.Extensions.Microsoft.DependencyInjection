using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace WebApiSampleNetCore2._2.Jobs
{
    [DisallowConcurrentExecution]
    public class SingletonJob : IJob
    {
        private readonly ILogger<SingletonJob> _logger;
        public SingletonJob(ILogger<SingletonJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Singleton Job Executed: " + DateTime.UtcNow);
            return Task.CompletedTask;
        }
    }
}