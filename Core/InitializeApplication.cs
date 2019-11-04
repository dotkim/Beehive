namespace Core
{
    public class InitializeApplication
    {
        private ConfigurationLoader ConfigLoader;
        private static Configuration Config;

        public static Configuration GetConfiguration()
        {
            return Config;
        }

        public InitializeApplication(string path = null)
        {
            _ = path != null
            ? ConfigLoader = new ConfigurationLoader(path)
            : ConfigLoader = new ConfigurationLoader();

            Config = ConfigLoader.LoadConfig();
        }
    }
}
