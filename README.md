## Quartz.Extensions.Microsoft.DependencyInjection

Dependency injection extensions for Quartz. It allows to inject a job easily in a standart way. Jobs are registered in **Startup.ConfigureServices** by using **AddJob** extension method as in the below configuration sample.

## Installation
>  Install-Package Quartz.Extensions.Microsoft.DependencyInjection

## Sample Configuration
To register a quartz job, use the AddJob method as follows.  And then, cron expression should be set.
Multiple jobs are easily injected in fluent syntax as in the coding conventions that used at NetCore.

```csharp
 public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJob<SingletonJob>(c => { c.CronExpression = "0/5 * * * * ?"; });
		    ..
            ..
            ...
        }
}
```
## Sample Job
Create a job as defined in Quartz.

```csharp
   [DisallowConcurrentExecution]
    public class SampleJob : IJob
    {
        private readonly ILogger<SampleJob> _logger;
        public SingletonJob(ILogger<SampleJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("SampleJob Executed: " + DateTime.UtcNow);
            return Task.CompletedTask;
        }
    }
```
