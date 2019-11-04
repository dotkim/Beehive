using System;
using System.IO;
using System.Text.Json;

namespace Core
{
    internal class ConfigurationLoader
    {
        static private string defaultConfigPath = Directory.GetCurrentDirectory() + "\\Core.json";

        private Configuration config = new Configuration();
        private string cfgPath;

        public ConfigurationLoader() : this(defaultConfigPath) { }
        public ConfigurationLoader(string cfgPath)
        {
            this.cfgPath = cfgPath;
        }

        public Configuration LoadConfig()
        {
            try
            {
                if (!File.Exists(cfgPath))
                {
                    return config;
                }

                string cfgContent = File.ReadAllText(cfgPath);

                config = Deserialize(cfgContent);

                return config;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Configuration Deserialize(string cfgContent)
        {
            return JsonSerializer.Deserialize<Configuration>(cfgContent);
        }
    }
}