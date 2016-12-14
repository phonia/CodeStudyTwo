using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //TaskFirst();测试一
            //TaskTwo();//测试二
            //TaskThree();//测试三
            //TaskFith();//测试五
            //TaskSix();//测试六
            TaskSeven();//测试七
        }

        static void TaskFirst()
        {
            Task<Int32> t = new Task<int>(n => Sum((Int32)n), 100000);

            t.Start();

            t.Wait();

            Console.WriteLine("The Sum is "+t.Result);
        }

        static Int32 Sum(Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; n--)
            {
                checked { sum += n; }
            }
            return sum;
        }

        static Int32 Sum2(CancellationToken token, Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; n--)
            {
                token.ThrowIfCancellationRequested();
                checked { sum += n; }
            }
            return sum;
        }

        static void TaskTwo()                                       //表达式中的参数作为闭包，后一个作为task的参数
        {
            CancellationTokenSource source = new CancellationTokenSource();
            Task<Int32> t = new Task<int>(()=>Sum2(source.Token,1000),source.Token);
            t.Start();
            source.Cancel();
            try
            { }
            catch (AggregateException x)
            {
                x.Handle(e => e is OperationCanceledException);
                Console.WriteLine("Sum was canceled");
            }
        }

        static Int32 Sum3(Int32 n)
        {
            Int32 sum=0;
            for (; n > 0; n--)
            {
                checked { sum += n; }
            }
            return sum;
        }

        static void TaskThree()
        {
            Task<Int32> t = new Task<int>(() => Sum3(1000));
            t.Start();
            t.Wait();
            Console.WriteLine("The Result is {0}", t.Result);
        }

        static void TaskFour()                                      //表达式的正确写法
        {
            Task<int> t = new Task<int>((n)=>Sum((Int32)n),1000);
        }

        static void TaskFith()                                      //一个任务完成时启动另一个任务
        {
            Task<int> t = new Task<int>(n => Sum((Int32)n), 10000);

            t.Start();

            //ContinueWinth 返回一个Task

            Task cwt = t.ContinueWith(task =>
                {
                    Console.WriteLine("The Result is:{0}", task.Result);
                    Console.ReadLine();
                }
                );
            //不使用下一句，无法再控制台中显示上述结果，原因未知
            //cwt.Wait();
        }

        static Int32 Sum4(Int32 n)                                  //测试在辅助线程中是否可以在控制台中显示
        {
            Int32 sum = 0;
            for (; n > 0; n--)
            {
                checked { sum += n; }
            }

            Console.WriteLine(sum);
            return sum;
        }

        static void TaskSix()                                       //测试辅助线程是否可以在控制台显示信息=>无法显示                    
        {
            Task<int> t = new Task<int>(n => Sum4((Int32)n), 1000);
            t.Start();

            //加上下面这句 两条显示语句都能起作用=>这里的线程应该不是同步的才对，试一试多个线程的情况
            //Console.WriteLine(t.Result);
        }

        static Int32 Sum7(Int32 n)
        {
            for (; n > 0; n--)
            {
                Thread.Sleep(10);
            }
            return n;
        }

        static void TaskSeven()                                     //测试线程同异步
        {
            Task[] task = new Task[] { 
                new Task<int>(n => Sum7((Int32)n), 1000),
                new Task<int>(n => Sum7((Int32)n), 1000),
                new Task<int>(n => Sum7((Int32)n), 1000),
                new Task<int>(n => Sum7((Int32)n), 1000),
                new Task<int>(n => Sum7((Int32)n), 1000),
                new Task<int>(n => Sum7((Int32)n), 1000),
                new Task<int>(n => Sum7((Int32)n), 1000),
                new Task<int>(n => Sum7((Int32)n), 1000),
                new Task<int>(n => Sum7((Int32)n), 1000),
                new Task<int>(n => Sum7((Int32)n), 1000),
                new Task<int>(n => Sum7((Int32)n), 1000),
                new Task<int>(n => Sum7((Int32)n), 1000),
                new Task<int>(n => Sum7((Int32)n), 1000)
            };
            if (task.Length > 0)
            {
                for (int i = 0; i < task.Length; i++)
                {
                    task[i].Start();
                }

                Console.WriteLine("pre");
                for (int i = 0; i < task.Length/2; i++)
                {
                    Console.WriteLine("The Task {0} Result is:{1}", i, ((Task<int>)task[i]).Result);
                }

                Console.WriteLine("Middle");

                for (int i = task.Length / 2; i < task.Length; i++)
                {
                    Console.WriteLine("The Task {0} Result is:{1}", i, ((Task<int>)task[i]).Result);
                }

                Console.WriteLine("End");
            }

            //结论=>Task会在另一条线程执行，但是直接调用Task的结果会导致线程的同步执行
            //即主线程调用辅助线程的结果必须等待辅助线程返回结果
            //在这之前主线程处于等待状态无法执行下一步
            //总结=>Task属于另外线程，但是上述的方式属于同步调用
        }
    }
}
