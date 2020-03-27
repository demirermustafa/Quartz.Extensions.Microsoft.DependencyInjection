using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApiSampleNetCore2._2
{
    public class ScopedSampleService : IScopedSampleService
    {
        private readonly ILogger<ScopedSampleService> _logger;

        public ScopedSampleService(ILogger<ScopedSampleService> logger)
        {
            _logger = logger;
        }

        public async Task DoWork()
        {
            await Task.Delay(1000);
        }
    }
}
