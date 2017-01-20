using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArrayResearch
{
    class Program
    {
        static void Main(string[] args)
        {
            //测试一：获取数据测实际占用长度，试验失败
            //byte[] b1 = new byte[10];
            //for (int i = 0; i < 8; i++)
            //{
            //    b1[i] = 2;
            //}
            //Console.WriteLine(b1.Length);

            //测试二：返回的数组是否指向原值
            int[] i2 = CT2();
            i2[2] = 100;
            foreach (var i in i1)
            {
                Console.WriteLine(i);
            }

        }



        static int[] i1 = new Int32[10];
        static int[] CT2()
        {
            for (int i = 0; i < 10; i++)
            {
                i1[i] = i;
            }
            return i1;
        }
    }
}
