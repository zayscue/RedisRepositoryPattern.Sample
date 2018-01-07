using RedisRepositoryPattern.Sample.Models;
using RedisRepositoryPattern.Sample.Repositories;
using StackExchange.Redis;
using System.Collections.Generic;

namespace RedisRepositoryPattern.Sample.Data
{
    public static class SeedData
    {
        public static void EnsureSeedPeopleData(this IDatabase db)
        {
            var uniqueIdKey = $"{PersonRepository.Key}:id";
            var exists = db.KeyExists(uniqueIdKey);
            if(!exists)
            {
                db.StringSet(uniqueIdKey, 1);
                var person = new Person
                {
                    Id = 1,
                    FirstName = "Zackery",
                    LastName = "Ayscue",
                    Email = "ztayscue@gmail.com",
                    Phone = "919-717-7575",
                    Age = 24,
                    HomeAddress = new Address
                    {
                        Street = "2800 Pickett Branch Rd. Apt 2803",
                        City = "Cary",
                        State = "NC",
                        Zip = "27519"
                    },
                    Hobbies = new List<Hobby>
                    {
                        new Hobby
                        {
                            Name = "Playing Video Games",
                            InterestLevel = LevelOfInterest.Medium,
                            IsActive = false,
                            IsCreative = false,
                        },
                        new Hobby
                        {
                            Name = "Web Development",
                            Description = "Building web sites and back end services",
                            InterestLevel = LevelOfInterest.High,
                            IsActive = false,
                            IsCreative = true,
                        },
                        new Hobby
                        {
                            Name = "Running",
                            InterestLevel = LevelOfInterest.High,
                            IsActive = true,
                            IsCreative = false,
                        }
                    }
                };
                var personKey = $"{PersonRepository.Key}:{person.Id}:{person.ObjectKey}";
                var personHashEntries = person.ToKeyValuePairs().ToHashEntries();
                db.HashSet(personKey, personHashEntries);
                var homeAddressKey = $"{PersonRepository.Key}:{person.Id}:{person.ObjectKey}:{person.HomeAddress.ObjectKey}";
                var homeAddressEntries = person.HomeAddress.ToKeyValuePairs().ToHashEntries(); 
                db.HashSet(homeAddressKey, homeAddressEntries);
                var count = 1;
                foreach(var hobby in person.Hobbies)
                {
                    var hobbyKey = $"{PersonRepository.Key}:{person.Id}:{person.ObjectKey}:{hobby.ObjectKey}:{count}";
                    var hobbyEntries = hobby.ToKeyValuePairs().ToHashEntries();
                    db.HashSet(hobbyKey, hobbyEntries);
                    count++;
                }
            }
        }
    }
}
