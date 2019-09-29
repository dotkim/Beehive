namespace Core
{
    class InitializeApplication : IConfiguration
    {
        private ConfigurationLoader configLoader;
        private Configuration config;

        public Configuration GetConfiguration()
        {
            return config;
        }
    }
}
