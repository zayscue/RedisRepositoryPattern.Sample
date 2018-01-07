using Microsoft.AspNetCore.Mvc;
using RedisRepositoryPattern.Sample.Models;
using RedisRepositoryPattern.Sample.Repositories;
using System;

namespace RedisRepositoryPattern.Sample.Controllers
{
    [Route("api/people")]
    public class PersonController : Controller
    {
        private readonly PersonRepository _people;

        public PersonController(PersonRepository people)
        {
            _people = people ?? throw new ArgumentNullException(nameof(people));
        }

        // POST api/people
        [HttpPost]
        public void Post([FromBody] Person person) => _people.Add(person);

        // GET api/person/{id}
        [HttpGet("{id}")]
        public Person Get(int id) => _people.Get(id);
    }
}
