﻿using System.Web.Http;

namespace WebApi.RedisCache.V2
{
    public static class HttpConfigurationExtensions
    {
        public static CacheOutputConfiguration CacheOutputConfiguration(this HttpConfiguration config)
        {
            return new CacheOutputConfiguration(config);
        }
    }
}