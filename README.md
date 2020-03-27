## Quartz.Extensions.Microsoft.DependencyInjection

Dependency injection extensions Quartz

## Installation
>  Install-Package Quartz.Extensions.Microsoft.DependencyInjection

## Configuration
To register a quartz job, use the AddJob method as follows. 
Multiple jobs are easily injected in fluent way as in the coding conventions that used at NetCore.

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