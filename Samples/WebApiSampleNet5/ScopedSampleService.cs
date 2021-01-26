using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace WebApiSampleNet5
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
            await Task.Delay(100);
        }
    }
}
