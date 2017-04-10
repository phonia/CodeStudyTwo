using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContructionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Product a = new Product() { Name = "hy", Account = 1 };
            Goods b = new Goods() { Product = a };
            Goods d = new Goods();
            Goods c = b;
            Console.WriteLine("Test Instacne c:" + c.Product.Account);
            Console.ReadLine();
        }
    }

    public class Product
    {
        public String Name { get; set; }
        public Int32 Account { get; set; }

        public void Print()
        {
            Console.WriteLine(Account);
        }

        public Int32 Count()
        {
            return Account++;
        }
    }

    public class Goods
    {
        private Product _product;

        public Product Product
        {
            get { return _product; }
            set { 
                _product = value;
                _product.Account = _product.Count();
                _product.Print();
            }
        }
    }
}
