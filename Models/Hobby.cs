using Newtonsoft.Json;
using System.Collections.Generic;

namespace RedisRepositoryPattern.Sample.Models
{
    public class Hobby : ICacheable
    {
        public static readonly string Key = "hobbies";

        public string Name { get; set; }
        public string Description { get; set; }
        public LevelOfInterest InterestLevel { get; set; }
        public bool IsActive { get; set; }
        public bool IsCreative { get; set; }

        [JsonIgnore]
        public string ObjectKey => Key;

        public void FromKeyValuePairs((string key, string value)[] pairs)
        {
            foreach (var pair in pairs)
            {
                switch (pair.key)
                {
                    case "name":
                        Name = pair.value;
                        break;
                    case "description":
                        Description = pair.value;
                        break;
                    case "interestlevel":
                        InterestLevel = (LevelOfInterest)int.Parse(pair.value);
                        break;
                    case "isactive":
                        IsActive = bool.Parse(pair.value);
                        break;
                    case "iscreative":
                        IsCreative = bool.Parse(pair.value);
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
                ("name", Name),
                ("description", Description),
                ("interestlevel", ((int) InterestLevel).ToString()),
                ("isactive", IsActive.ToString()),
                ("iscreative", IsCreative.ToString())
            };
            pairs.RemoveAll(x => x.value == null);
            return pairs.ToArray();
        }
    }
}
