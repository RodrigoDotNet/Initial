namespace Initial.Api.Util
{
    /// <summary>
    /// Abstração dos dados do appsettings.json
    /// </summary>
    public class AppSettings
    {
        public CacheSettings Cache { get; set; }
            = CacheSettings.Default;
        public SecuritySettings Security { get; set; }
            = SecuritySettings.Default;

        public static AppSettings Default
            => new AppSettings { };

        public class CacheSettings
        {
            public int Duration { get; set; } = 45;

            public static CacheSettings Default
                => new CacheSettings { };
        }

        public class SecuritySettings
        {
            public string Secret { get; set; }
                = "If you want to keep a secret, you must also hide it from yourself.";

            public int Expires { get; set; } = 7;

            public static SecuritySettings Default
                => new SecuritySettings { };
        }
    }
}
