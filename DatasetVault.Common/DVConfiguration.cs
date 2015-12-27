using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatasetVault.Common
{
    public class DVConfiguration
    {
        private Configuration configuration;

        public string SourceSqlConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SourceSqlConnectionString"].ConnectionString;
                ////return configuration.AppSettings.Settings["SourceSqlConnectionString"].Value;
            }
        }

        public string SearchServiceName
        {
            get
            {
                return  ConfigurationManager.AppSettings["SearchServiceName"];
                ////return configuration.AppSettings.Settings["SearchServiceName"].Value;
            }
        }

        public string SearchServiceApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["SearchServiceApiKey"];
                ////return configuration.AppSettings.Settings["SearchServiceApiKey"].Value;
            }
        }

        public string SearchIndexName
        {
            get
            {
                return ConfigurationManager.AppSettings["SearchIndexName"];
                ////return configuration.AppSettings.Settings["SearchIndexName"].Value;
            }
        }

        private static DVConfiguration instance;

        public static DVConfiguration Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DVConfiguration();
                }
                return instance;
            }

        }

        private DVConfiguration()
        {
            ////string folderPath = Path.Combine(GetExecutingFolderPath(), "App.Config");
            ////ConfigurationFileMap fileMap = new ConfigurationFileMap(folderPath); //Path to your config file
            ////configuration = ConfigurationManager.OpenMappedMachineConfiguration(fileMap);
        }

        private string GetExecutingFolderPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

    }
}
