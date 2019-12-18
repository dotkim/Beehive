namespace Core
{
    /// <summary>
    /// Currently a placeholder class for stuff that needs to be done before the communicator class will work.
    /// This might be removed if some other method of generating config is used.
    /// </summary>
    public class InitializeApplication
    {
        private readonly ConfigurationLoader ConfigLoader;
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
