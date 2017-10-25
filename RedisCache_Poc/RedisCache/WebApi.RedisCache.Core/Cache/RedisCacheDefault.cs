using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace WebApi.RedisCache.Core.Cache
{
    public class RedisCacheDefault : IApiOutputCache
    {


        // static readonly ZiraffSection section = (ZiraffSection)ConfigurationManager.GetSection("Server");
        //private static readonly BasicRedisClientManager Manager = new BasicRedisClientManager("p17rT9wb4z0cEKi8Eekw/iHzEecIkiyrcmVcW4+xzKA=@appsworld.redis.cache.windows.net");
        //private static readonly BasicRedisClientManager Manager = new BasicRedisClientManager(GetSection());
        private static readonly string d = "sa";
        private static readonly BasicRedisClientManager Manager = new BasicRedisClientManager("localhost:6379");

        private static readonly IRedisClient MyRedisClient = Manager.GetClient();

        //public static string GetSection()
        //{
        //    for (int i = 0; i < section.Ziraff.Count; i++)
        //    {
        //        return section.Ziraff[i].RedisUrl;
        //    }
        //    return null;
        //}
        public void RemoveStartsWith(string key)
        {
            lock (MyRedisClient)
            {
                var getAll = MyRedisClient.GetAllKeys();

                var keysToBeDeleted = new List<string>();
                keysToBeDeleted.AddRange(getAll.Where(keys => keys.Contains(key)));

                MyRedisClient.RemoveAll(keysToBeDeleted);
            }
        }

        public T Get<T>(string key) where T : class
        {

            var o = MyRedisClient.Get<T>(key);
            return o;
        }

        [Obsolete("Use Get<T> instead")]
        public object Get(string key)
        {

            return "";
        }

        public void Remove(string key)
        {

            lock (MyRedisClient)
            {
                MyRedisClient.Remove(key);
            }
        }

        public bool Contains(string key)
        {

            return MyRedisClient.ContainsKey(key);
        }

        public void Add(string key, object o, DateTimeOffset expiration, string dependsOnKey = null)
        {

            lock (MyRedisClient)
            {
                MyRedisClient.Add(key, o, expiration.DateTime);
            }
        }

        public IEnumerable<string> AllKeys
        {
            get
            {
                return MyRedisClient.GetAllKeys();
            }
        }
    }
}