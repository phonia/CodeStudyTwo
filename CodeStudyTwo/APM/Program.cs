using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APM
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化一个委托变量，让它引用想要变异调用的方法
            Func<UInt64, UInt64> sumDelegate = Sum;

            //使用一个线程池调用方法
            sumDelegate.BeginInvoke(100000000, SumIsDone, sumDelegate);

            Console.WriteLine("The Main Thread!");

            Console.ReadLine();
        }

        static UInt64 Sum(UInt64 n)
        {
            UInt64 sum = 0;
            for (UInt64 i = 1; i < n; i++)
            {
                checked { sum += n; }
            }
            return sum;
        }

        static void SumIsDone(IAsyncResult result)
        {
            var sumDelegate=(Func<UInt64,UInt64>)result.AsyncState;

            Console.WriteLine("CallBack!");

            try
            {
                Console.WriteLine("Sum's result:" + sumDelegate.EndInvoke(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sum's result- is too large to calculate");
            }
        }
    }
}
