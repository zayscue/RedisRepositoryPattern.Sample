using RedisRepositoryPattern.Sample.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace RedisRepositoryPattern.Sample.Repositories
{
    public class PersonRepository
    {
        private readonly IDatabase _db;
        public static readonly string Key = "people";

        public PersonRepository(IDatabase db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(Person person)
        {
            throw new NotImplementedException();
        }

        public Person Get(int id)
        {
            var personKey = $"{Key}:{id}:{Person.Key}";
            var personEntries = _db.HashGetAll(personKey);
            if (personEntries == null || personEntries.Length <= 0) return null;
            var person = new Person();
            person.FromKeyValuePairs(personEntries.FromHashEntries());
            var homeAddresskey = $"{Key}:{id}:{Person.Key}:{Address.Key}";
            var homeAddressEntries = _db.HashGetAll(homeAddresskey);
            if(homeAddressEntries != null && homeAddressEntries.Length > 0)
            {
                person.HomeAddress = new Address();
                person.HomeAddress.FromKeyValuePairs(homeAddressEntries.FromHashEntries());
            }
            var hobbyKeyPrefix = $"{Key}:{id}:{Person.Key}:{Hobby.Key}";
            var count = 1;
            var hobbyEntries = _db.HashGetAll($"{hobbyKeyPrefix}:{count}");
            if (hobbyEntries == null || hobbyEntries.Length <= 0) return person;
            person.Hobbies = new List<Hobby>();
            while(hobbyEntries != null && hobbyEntries.Length > 0)
            {
                var hobby = new Hobby();
                hobby.FromKeyValuePairs(hobbyEntries.FromHashEntries());
                person.Hobbies.Add(hobby);
                count++;
                hobbyEntries = _db.HashGetAll($"{hobbyKeyPrefix}:{count}");
            }
            return person;
        }
    }
}
