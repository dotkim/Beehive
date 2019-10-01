using System;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Core
{
    internal class ConfigurationLoader
    {
        static private string defaultConfigPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\Core.json";

        private Configuration config;
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
                    Console.WriteLine("File not found.");
                    Environment.Exit(2);
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