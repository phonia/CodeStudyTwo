using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Research1._0
{
    class Program
    {
        static void Main(string[] args)
        {
            /////////测试一  
            //Staff s1=new Staff();
            //int i = 0;
            //while (i<10)
            //{
            //    ThreadPool.QueueUserWorkItem(o => s1.Push());
            //    ThreadPool.QueueUserWorkItem(o => s1.Pop());
            //    i++;
            //}
            //Console.ReadLine();


            /////测试二
            //Staff s1 = new Staff();
            //Staff s2 = new Staff();
            //int i = 0;
            //while (i < 10)
            //{
            //    ThreadPool.QueueUserWorkItem(o => s1.Push());
            //    ThreadPool.QueueUserWorkItem(o => s2.Pop());
            //    i++;
            //}
            //Console.ReadLine();
            
            //结论：lock关键字进入的临界区 是指对象 object的临界区，也就是意味着在该对象未从临界区退出前，无法访问任何有该对象标记的临界区

        }
    }

    public class Staff
    {
        private object _lockObejct = new object();
        public List<int> Data { get; set; }

        public Staff()
        {
            Data = new List<int>();
        }

        public void Pop()
        {
            lock (_lockObejct)
            {
                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine("Pop:"+i.ToString());
                }
            }
        }

        public void Push()
        {
            lock (_lockObejct)
            {
                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine("Push:"+i.ToString());
                }
            }
        }
    }
}
