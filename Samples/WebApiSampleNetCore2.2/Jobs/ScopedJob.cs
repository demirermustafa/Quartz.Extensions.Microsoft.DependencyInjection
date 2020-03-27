using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace WebApiSampleNetCore2._2.Jobs
{
    [DisallowConcurrentExecution]
    public class ScopedJob : IJob
    {
        private readonly ILogger<ScopedJob> _logger;
        private readonly IServiceProvider _provider;
        public ScopedJob(ILogger<ScopedJob> logger, IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Delay(1000);
            using (var scope = _provider.CreateScope())
            {
                var scopedSampleService = scope.ServiceProvider.GetService<IScopedSampleService>();

                await scopedSampleService.DoWork();
            }
            _logger.LogWarning("Scoped Job Executed: " + DateTime.UtcNow);
        }

    }
}