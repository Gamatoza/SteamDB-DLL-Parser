
namespace DLLParcer.Core.SteamDB {
    class HabraSettings : IParserSettings
    {
        public HabraSettings(string prefix)
        {
            Prefix = prefix;
        }

        public string BaseUrl { get; set; } = "https://steamdb.info/app/";

        public string Prefix { get; set; } = "{CurrentId}";
    }
}
