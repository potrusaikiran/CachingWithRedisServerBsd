using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.RedisCache.V2;

namespace RedisCache_Poc.Controllers
{
    [RoutePrefix("api/redis")]
    public class RedisController : ApiController
    {
        [HttpGet]
        [Route("getperson")]
        [CacheOutput(ClientTimeSpan = 0, ServerTimeSpan = 6000)]
        public List<Person> GetPerson()
        {
            var lstperson = new List<Person>();
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                var person = new Person();
                int randomNumber = random.Next(0, 1000);
                person.Id = randomNumber;
                person.Name = "Saikiran" +"-"+ randomNumber;
                person.NickName = "Nick" + "-" + person.Name;
                lstperson.Add(person);
            }
            return lstperson;
        }

    }
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
    }

}
