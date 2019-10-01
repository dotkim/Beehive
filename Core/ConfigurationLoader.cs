﻿using System;
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

        public ConfigurationLoader(string cfgPath)
        {
            this.cfgPath = cfgPath;
        }

        public Configuration LoadConfig()
        {
            config = new Configuration();

            try
            {
                if (!File.Exists(cfgPath))
                {
                    Console.WriteLine("File not found.");
                    Environment.Exit(2);
                }

                string cfgContent = File.ReadAllText(cfgPath);

                JsonValue parsedCfg = JsonValue.Parse(cfgContent);
                

            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}