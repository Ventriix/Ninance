using System;
using System.IO;
using Config.Net;

namespace Ninance_v2.Core
{

    public interface INinanceSettings
    {
        [Option(Alias = "General.UsingDarkMode")]
        bool UsingDarkMode { get; set; }
    }

    public class ConfigHandler
    {

        public INinanceSettings Settings { get; }

        public ConfigHandler()
        {
            string path = Environment.CurrentDirectory + "/data/settings.ini";

            if (!File.Exists(path))
                File.Create(path).Close();

            Settings = new ConfigurationBuilder<INinanceSettings>()
                .UseIniFile(path)
                .Build();
        }
    }
}
