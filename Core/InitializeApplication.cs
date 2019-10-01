namespace Core
{
    public class InitializeApplication
    {
        private ConfigurationLoader configLoader;
        private static Configuration config;

        public static Configuration GetConfiguration()
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
