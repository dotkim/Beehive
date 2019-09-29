using System;
using System.Collections.Generic;
using System.Text;

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
