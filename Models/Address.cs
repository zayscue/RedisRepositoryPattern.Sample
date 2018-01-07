using Newtonsoft.Json;
using System.Collections.Generic;

namespace RedisRepositoryPattern.Sample.Models
{
    public class Address : ICacheable
    {
        public static readonly string Key = "homeaddress";

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        [JsonIgnore]
        public string ObjectKey => Key;

        public void FromKeyValuePairs((string key, string value)[] pairs)
        {
            foreach (var pair in pairs)
            {
                switch (pair.key)
                {
                    case "street":
                        Street = pair.value;
                        break;
                    case "city":
                        City = pair.value;
                        break;
                    case "state":
                        State = pair.value;
                        break;
                    case "zip":
                        Zip = pair.value;
                        break;
                    default:
                        break;
                }
            }
        }

        public (string key, string value)[] ToKeyValuePairs()
        {
            var pairs = new List<(string key, string value)>
            {
                ("street", Street),
                ("city", City),
                ("state", State),
                ("zip", Zip)
            };
            pairs.RemoveAll(x => x.value == null);
            return pairs.ToArray();
        }
    }
}
