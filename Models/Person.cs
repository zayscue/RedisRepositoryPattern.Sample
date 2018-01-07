using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisRepositoryPattern.Sample.Models
{
    public class Person : ICacheable
    {
        public static readonly string Key = "person";

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName?.Trim() ?? string.Empty} {LastName?.Trim() ?? string.Empty}";
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public Address HomeAddress { get; set; }
        public ICollection<Hobby> Hobbies { get; set; }

        [JsonIgnore]
        public string ObjectKey => Key;

        public void FromKeyValuePairs((string key, string value)[] pairs)
        {
            foreach(var pair in pairs)
            {
                switch(pair.key)
                {
                    case "id":
                        Id = int.Parse(pair.value);
                        break;
                    case "firstname":
                        FirstName = pair.value;
                        break;
                    case "lastname":
                        LastName = pair.value;
                        break;
                    case "email":
                        Email = pair.value;
                        break;
                    case "phone":
                        Phone = pair.value;
                        break;
                    case "age":
                        Age = int.Parse(pair.value);
                        break;
                    default:
                        break;
                }
            }
        }

        public (string key, string value)[] ToKeyValuePairs()
        {
            var pairs = new List<(string key, string value)>{
                ("id", Id.ToString()),
                ("firstname", FirstName),
                ("lastname", LastName),
                ("email", Email),
                ("phone", Phone),
                ("age", Age.ToString())
            };
            pairs.RemoveAll(x => x.value == null);
            return pairs.ToArray();
        }
    }
}
