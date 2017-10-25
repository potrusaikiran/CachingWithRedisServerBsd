using System;

namespace WebApi.RedisCache.Core.Time
{
    public class ShortTime : IModelQuery<DateTime, CacheTime>
    {
        private readonly int _serverTimeInSeconds;
        private readonly int _clientTimeInSeconds;

        public ShortTime(int serverTimeInSeconds, int clientTimeInSeconds)
        {
            if (serverTimeInSeconds < 0)
                serverTimeInSeconds = 0;

            this._serverTimeInSeconds = serverTimeInSeconds;

            if (clientTimeInSeconds < 0)
                clientTimeInSeconds = 0;

            this._clientTimeInSeconds = clientTimeInSeconds;
        }

        public CacheTime Execute(DateTime model)
        {
            var cacheTime = new CacheTime
                {
                    AbsoluteExpiration = model.AddSeconds(_serverTimeInSeconds),
                    ClientTimeSpan = TimeSpan.FromSeconds(_clientTimeInSeconds)
                };

            return cacheTime;
        }
    }
}