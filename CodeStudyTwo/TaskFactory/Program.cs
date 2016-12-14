using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskFactory();//测试一
        }

        static Int32 Sum(CancellationToken token,Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; n--)
            {
                token.ThrowIfCancellationRequested();
                checked { sum += n; }
            }
            return sum;
        }

        static void TaskFactory()
        {
            Task parent = new Task(() => {
                var cts = new CancellationTokenSource();
                var tf = new TaskFactory<Int32>(cts.Token,
                    TaskCreationOptions.AttachedToParent,
                    TaskContinuationOptions.ExecuteSynchronously,               //异步执行参数？
                    TaskScheduler.Default);

                var childTasks = new[] { 
                    tf.StartNew(()=>Sum(cts.Token,1000)),
                    tf.StartNew(()=>Sum(cts.Token,2000)),
                    tf.StartNew(()=>Sum(cts.Token,Int32.MaxValue))
                };

                for (Int32 task = 0; task < childTasks.Length; task++)
                {
                    childTasks[task].ContinueWith(t=>cts.Cancel(),TaskContinuationOptions.OnlyOnFaulted);
                }

                tf.ContinueWhenAll(
                    childTasks,
                    completedTasks=>completedTasks.Where(t=>
                        !t.IsFaulted&&!t.IsCanceled).Max(t=>t.Result),
                        CancellationToken.None)
                        .ContinueWith(t=>
                            Console.WriteLine("The maximum is :"+t.Result),
                            TaskContinuationOptions.ExecuteSynchronously
                    );
            });

            parent.ContinueWith(p =>
                {
                    StringBuilder sb = new StringBuilder("The following exception(s) occurres:" + Environment.NewLine);

                    foreach (var e in p.Exception.Flatten().InnerExceptions)
                        sb.AppendLine(" " + e.GetType().ToString());
                    Console.WriteLine(sb.ToString());
                }, TaskContinuationOptions.OnlyOnFaulted);

            parent.Start();
            Thread.Sleep(100000);
        }
    }
}
