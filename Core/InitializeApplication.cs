namespace Core
{
    public class InitializeApplication : IConfiguration
    {
        private ConfigurationLoader configLoader;
        private Configuration config;

        public Configuration GetConfiguration()
        {
            return config;
        }

        public InitializeApplication()
        {
            string path = @"C:\Github\Sauron\Program\Core.json";
            configLoader = new ConfigurationLoader(path);
            config = configLoader.LoadConfig();
        }
    }
}
