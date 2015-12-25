using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
                return configuration.AppSettings.Settings["SourceSqlConnectionString"].Value;
            }
        }
        public string SearchServiceName
        {
            get
            {
                return configuration.AppSettings.Settings["SearchServiceName"].Value;
            }
        }
        public string SearchServiceApiKey
        {
            get
            {
                return configuration.AppSettings.Settings["SearchServiceApiKey"].Value;
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
            ////ConfigurationFileMap fileMap = new ConfigurationFileMap(file); //Path to your config file
            ////Configuration configuration = ConfigurationManager.OpenMappedMachineConfiguration(fileMap);
            ////string value = configuration.AppSettings.Settings["key1"].Value;
        }


    }
}
