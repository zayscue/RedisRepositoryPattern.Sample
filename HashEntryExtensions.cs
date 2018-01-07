using StackExchange.Redis;
using System.Linq;

namespace RedisRepositoryPattern.Sample
{
    public static class HashEntryExtensions
    {
        public static HashEntry[] ToHashEntries(this (string key, string value)[] pairs)
        {
            return pairs.Select(x => new HashEntry(x.key, x.value)).ToArray();
        }

        public static (string key, string value)[] FromHashEntries(this HashEntry[] entries)
        {
            return entries.Select(x => (key: x.Name.ToString(), value: x.Value.ToString())).ToArray();
        }
    }
}
