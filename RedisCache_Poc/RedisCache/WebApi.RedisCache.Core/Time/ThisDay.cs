using System;

namespace WebApi.RedisCache.Core.Time
{
    public class ThisDay : IModelQuery<DateTime, CacheTime>
    {
        private readonly int _hour;
        private readonly int _minute;
        private readonly int _second;

        public ThisDay(int hour, int minute, int second)
        {
            this._hour = hour;
            this._minute = minute;
            this._second = second;
        }

        public CacheTime Execute(DateTime model)
        {
            var cacheTime = new CacheTime
            {
                AbsoluteExpiration = new DateTime(model.Year,
                                                  model.Month,
                                                  model.Day,
                                                  _hour,
                                                  _minute,
                                                  _second),
            };

            if (cacheTime.AbsoluteExpiration <= model)
                cacheTime.AbsoluteExpiration = cacheTime.AbsoluteExpiration.AddDays(1);

            cacheTime.ClientTimeSpan = cacheTime.AbsoluteExpiration.Subtract(model);

            return cacheTime;
        }
    }
}
