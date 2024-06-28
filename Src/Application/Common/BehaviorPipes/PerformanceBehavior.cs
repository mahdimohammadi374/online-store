using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.BehaviorPipes
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly Stopwatch _timer;
        public PerformanceBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Performance ...");
            _timer.Start();
            var response = await next();
            _timer.Stop();
            var elapsedMilsec = _timer.ElapsedMilliseconds;
            if (elapsedMilsec <= 500) return response;
            var reqName = typeof(TRequest).Name;
            _logger.LogWarning($"CleanArchitecture long running request : ({elapsedMilsec} milliseconds", reqName, elapsedMilsec, request);
            return response;
        }
    }
}
