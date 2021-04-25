﻿#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2021 Johnathon M
// 
//         Permission is hereby granted, free of charge, to any person obtaining a copy
//         of this software and associated documentation files (the "Software"), to deal
//         in the Software without restriction, including without limitation the rights
//         to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//         copies of the Software, and to permit persons to whom the Software is
//         furnished to do so, subject to the following conditions:
// 
//         The above copyright notice and this permission notice shall be included in
//         all copies or substantial portions of the Software.
// 
//         THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//         IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//         FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//         AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//         LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//         OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//         THE SOFTWARE.
// 
// */

#endregion

using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using TileIconifier.Core.Utilities;

namespace TileIconifier.Core
{
    [Serializable]
    public class Config
    {
        public static readonly Config Instance;

        static Config()
        {
            Instance = LoadConfig(ConfigFilePath);
        }

        [NonSerialized] public string LoadedConfigFilePath;

        private Config()
        {
        }

        private Config(string filePath)
        {
            LoadedConfigFilePath = filePath;
        }

        private static string ConfigFileName => "TileIconifierConfig.xml";
        private static string ConfigFilePath => Path.Combine(IoUtils.ProgramDataPath, ConfigFileName);

        public string LocaleToUse { get; set; }
        public bool GetPinnedItems { get; set; }
        public string LastSkin { get; set; }

        public int[] CustomColors { get; set; }

        //calculated on every load - not committed to config file
        public static bool StartMenuUpgradeEnabled { get; set; }
        public bool DisableUpgradedStartMessage { get; set; }

        public void SaveConfig()
        {
            SaveConfig(LoadedConfigFilePath);
        }

        public void SaveConfig(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            var dirPath = Path.GetDirectoryName(filePath);
            if (dirPath != null && !Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            IoUtils.ForceDelete(filePath);

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
                    var xmlDeserializer = new XmlSerializer(typeof(Config));
                    var config = (Config)xmlDeserializer.Deserialize(xmlFile);
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