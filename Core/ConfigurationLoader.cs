using System.IO;
using System.Reflection;

namespace Core
{
    internal class ConfigurationLoader
    {
        static private string defaultConfigPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\Core.cfg";

        private Configuration config;
        private string cfgPath;
    }
}