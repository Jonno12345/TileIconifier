using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TileIconifier.Core.Utilities;

namespace TileIconifier.Core
{
    [Serializable]
    public class Config
    {
        public static string ConfigFileName => "TileIconifierConfig.xml";
        public static string ConfigFilePath => Path.Combine(IoUtils.ProgramDataPath, ConfigFileName);

        private static Config _instance;
        public static Config Instance => _instance ?? (_instance = LoadConfig(ConfigFilePath));

        public Config()
        {
        }

        public Config(string filePath)
        {
            LoadedConfigFilePath = filePath;
        }

        [NonSerialized] public string LoadedConfigFilePath;

        public string LocaleToUse { get; set; }


        public void SaveConfig()
        {
            SaveConfig(LoadedConfigFilePath);
        }

        public void SaveConfig(string filePath)
        {
            using (var xmlFile = new FileStream(filePath, FileMode.Create))
            {
                var xmlSerializer = new XmlSerializer(typeof(Config));
                xmlSerializer.Serialize(xmlFile, this);
            }
        }

        public static Config LoadConfig(string filePath)
        {
            if (!File.Exists(filePath))
                return new Config(filePath);
            try
            {
                using (var xmlFile = new FileStream(filePath, FileMode.Open))
                {
                    var xmlDeserializer = new XmlSerializer(typeof (Config));
                    var config = (Config) xmlDeserializer.Deserialize(xmlFile);
                    config.LoadedConfigFilePath = filePath;
                    return config;

                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                return new Config(filePath);
            }
        }
    }
}
