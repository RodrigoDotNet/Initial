namespace Initial.Api.Util
{
    public class AppSettings
    {
        public string JWT_Secret { get; set; }
            = "If you want to keep a secret, you must also hide it from yourself.";

        public CacheSettings Cache { get; set; }
            = CacheSettings.Default;

        public static AppSettings Default
            => new AppSettings { };

        public class CacheSettings
        {
            public int Duration { get; set; } = 45;

            public static CacheSettings Default
                => new CacheSettings { };
        }
    }
}
