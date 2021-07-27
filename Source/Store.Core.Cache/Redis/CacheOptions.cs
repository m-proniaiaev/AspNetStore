namespace Store.Core.Cache.Redis
{
    public class CacheOptions
    {
        public string Server { get; set; }

        public int ExpirationMinutes { get; set; }
    }
}