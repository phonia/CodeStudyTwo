using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace UnityIoc
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory+"\\Unity.Config" };
            Configuration configuration =
                ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var unitySection = (UnityConfigurationSection)configuration.GetSection("unity");
            container.LoadConfiguration(unitySection);

            var zoo = container.Resolve<IZoo>("Usual");
            zoo.ThinkAloud();
        }
    }
}
