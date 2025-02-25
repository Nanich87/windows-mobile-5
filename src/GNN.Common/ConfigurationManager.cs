namespace GNN.Common
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Reflection;
    using System.Xml;

    public static class ConfigurationManager
    {
        private static NameValueCollection appSettings = new NameValueCollection();

        private static string configFile;

        static ConfigurationManager()
        {
            ConfigurationManager.configFile = string.Format("{0}.config", System.Reflection.Assembly.GetCallingAssembly().GetName().CodeBase);

            if (!File.Exists(ConfigurationManager.configFile))
            {
                throw new FileNotFoundException(String.Format("Configuration file ({0}) could not be found.", ConfigurationManager.configFile));
            }

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(ConfigurationManager.configFile);

            foreach (XmlNode appSettingNode in xmlDocument.SelectNodes("/configuration/appSettings/add"))
            {
                ConfigurationManager.AppSettings.Add(appSettingNode.Attributes["key"].Value, appSettingNode.Attributes["value"].Value);
            }
        }

        public static NameValueCollection AppSettings
        {
            get
            {
                return appSettings;
            }
        }

        public static void Save()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(ConfigurationManager.configFile);

            var appSettingsNode = xmlDocument.SelectSingleNode("/configuration/appSettings");
            if (appSettingsNode != null)
            {
                appSettingsNode.RemoveAll();

                foreach (var key in AppSettings.AllKeys)
                {
                    var appSettingNode = xmlDocument.CreateElement("add");

                    var keyAttribute = xmlDocument.CreateAttribute("key");
                    keyAttribute.Value = key;

                    var valueAttribute = xmlDocument.CreateAttribute("value");
                    valueAttribute.Value = AppSettings[key];

                    appSettingNode.Attributes.Append(keyAttribute);
                    appSettingNode.Attributes.Append(valueAttribute);

                    appSettingsNode.AppendChild(appSettingNode);
                }
            }

            xmlDocument.Save(ConfigurationManager.configFile);
        }
    }
}