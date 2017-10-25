using System;

namespace WebApi.RedisCache.Core.Time
{
    public class ThisMonth : IModelQuery<DateTime, CacheTime>
    {
        private readonly int _day;
        private readonly int _hour;
        private readonly int _minute;
        private readonly int _second;

        public ThisMonth(int day, int hour, int minute, int second)
        {
            this._day = day;
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
                                                      _day,
                                                      _hour,
                                                      _minute,
                                                      _second),
                };

            if (cacheTime.AbsoluteExpiration <= model)
                cacheTime.AbsoluteExpiration = cacheTime.AbsoluteExpiration.AddMonths(1);

            cacheTime.ClientTimeSpan = cacheTime.AbsoluteExpiration.Subtract(model);

            return cacheTime;
        }
    }
}