using Application.Contracts;
using Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.BehaviorPipes
{
    public class CachedQueryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : ICacheQuery, IRequest<TResponse>
    {
        private readonly IDistributedCache _cache; // Make sure Injected in Program.cs in web layer
        private readonly IHttpContextAccessor httpContextAccessor; // Make sure Injected in Program.cs in web layer
        public CachedQueryBehavior(IDistributedCache cache, IHttpContextAccessor httpContextAccessor)
        {
            _cache = cache;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response;
            var key = GenerateKey();
            var cachedResponse = await _cache.GetAsync(key, cancellationToken);
            if (cachedResponse != null)
            {
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.UTF8.GetString(cachedResponse));
            }
            else
            {
                response = await next();
                var seialized = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
                await CreateNewCache(request ,key, cancellationToken , seialized);
            }
            return response;
        }

        private Task CreateNewCache(TRequest request,string key, CancellationToken cancellationToken, byte[] seialized)
        {
            return _cache.SetAsync(key, seialized,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeToLive(request)
                }, cancellationToken);
        }
        private static TimeSpan TimeToLive(TRequest request)
        {
            return new TimeSpan(request.HourseSaveData, 0, 0, 0);
        }
        private string GenerateKey()
        {
            return IdGenerator.GenerateCacheKeyFromRequest(httpContextAccessor.HttpContext.Request);
        }
    }
}
