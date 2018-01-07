namespace RedisRepositoryPattern.Sample
{
    public interface ICacheable
    {
        string ObjectKey { get; }
        (string key, string value)[] ToKeyValuePairs();
        void FromKeyValuePairs((string key, string value)[] pairs);
    }
}
