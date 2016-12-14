using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            for (Int32 n = 0; n < 100000000; n++)
            {
                new PipeClient("localhost", "Request #" + n);
            }
            Console.ReadLine();
        }
    }
}
