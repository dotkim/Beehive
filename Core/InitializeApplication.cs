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

        public InitializeApplication()
        {
            string path = @"C:\Github\Sauron\Program\Core.json";
            ConfigLoader = new ConfigurationLoader(path);
            Config = ConfigLoader.LoadConfig();
        }
    }
}
