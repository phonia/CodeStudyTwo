using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            for (Int32 n = 0; n < Environment.ProcessorCount; n++)
            {
                new PipleServer();
            }

            Console.WriteLine("Press <Enter> to terminate this server application");
            Console.ReadLine();
        }
    }
}
