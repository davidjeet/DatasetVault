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
        private static DVConfiguration instance;

        //properties go here!!!!

        private DVConfiguration()
        {
            ////ConfigurationFileMap fileMap = new ConfigurationFileMap(file); //Path to your config file
            ////Configuration configuration = ConfigurationManager.OpenMappedMachineConfiguration(fileMap);
            ////string value = configuration.AppSettings.Settings["key1"].Value;
        }

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
    }
}
