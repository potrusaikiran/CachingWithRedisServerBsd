﻿using System;

namespace WebApi.RedisCache.Core.Time
{
    public class SpecificTime : IModelQuery<DateTime, CacheTime>
    {
        private readonly int _year;
        private readonly int _month;
        private readonly int _day;
        private readonly int _hour;
        private readonly int _minute;
        private readonly int _second;

        public SpecificTime(int year, int month, int day, int hour, int minute, int second)
        {
            this._year = year;
            this._month = month;
            this._day = day;
            this._hour = hour;
            this._minute = minute;
            this._second = second;
        }

        public CacheTime Execute(DateTime model)
        {
            var cacheTime = new CacheTime
                {
                    AbsoluteExpiration = new DateTime(_year,
                                                      _month,
                                                      _day,
                                                      _hour,
                                                      _minute,
                                                      _second),
                };

            cacheTime.ClientTimeSpan = cacheTime.AbsoluteExpiration.Subtract(model);

            return cacheTime;
        }
    }
}