using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using UnityIoc.AOP;

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

            //默认的lifetime
            Console.WriteLine("The first Test!");
            var zoo = container.Resolve<IZoo>("Usual");
            zoo.ThinkAloud();
            Console.WriteLine("");

            
                
            /**
             * 生命周期管理器
             * 1、RegisterType默认的TransientLifetimeManager typpe="transient"
             * 2、RegisterInstance默认的ContainerControlledLifetimeManager type="singleton"
             * **/
            Console.WriteLine("the ContainerControlledLifetimeManager Test!");
            var children = container.Resolve<IHumanbeings>("Children");
            children.Aboard();
            Console.WriteLine("");
            var man = container.Resolve<IHumanbeings>("Man");
            man.Aboard();
            Console.WriteLine("");
            var woman = container.Resolve<IHumanbeings>("Women");
            woman.Aboard();
            Console.WriteLine("");

            /**
             * AOP基础测试
             * **/
            Console.WriteLine("AOP基础");
            var house = container.Resolve<IHouse>("houseKeeper");
            house.OpenTheDoor(HouseVisiter.Owner);
            house.OpenTheDoor(HouseVisiter.Chieves);
            house.BurnTheHouse(HouseVisiter.Policy);
            house.BurnTheHouse(HouseVisiter.Owner);
            Console.WriteLine("");

            /**
             * PolicyInjection
             * ***/
            Console.WriteLine("PolicyInjection");
            var log = container.Resolve<ILogger>();
            log.Log();
            
            Console.WriteLine("");
        }
    }
}
