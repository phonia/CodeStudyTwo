using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CancellToken
{
    class Program
    {
        static void Main(string[] args)
        {
            //Go();测试一
            LinkCts();//测试二
        }

        static void Go()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            //将CancellationToken和"要数到的数"{number - to -count-to)传入操作
            ThreadPool.QueueUserWorkItem(o => Count(cts.Token, 100));
            Console.WriteLine("Press <enter> to cancel the operation!");
            Console.ReadLine();
            cts.Cancel();
        }

        static void Count(CancellationToken token, Int32 countTo)
        {
            for (Int32 count = 0; count < countTo; count++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Count is cancelled");
                    break;
                }
                Console.WriteLine(count);
                Thread.Sleep(200);
            }
            Console.WriteLine("Count is done!");
        }

        static void LinkCts()
        {
            var cts1 = new CancellationTokenSource();
            cts1.Token.Register(() => Console.WriteLine("cts1 cancelled"));

            var cts2 = new CancellationTokenSource();
            cts2.Token.Register(() => Console.WriteLine("cts2 Cancelled"));

            var linkCts = CancellationTokenSource.CreateLinkedTokenSource(cts1.Token, cts2.Token);
            cts2.Cancel();

            Console.WriteLine("cts1 canceled={0} ,cts2 canceled={1},linkedCts={2}",
                cts1.IsCancellationRequested, cts2.IsCancellationRequested,
                linkCts.IsCancellationRequested);
        }
    }
}
