using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace polymorphic
{
    class Program
    {
        static void Main(string[] args)
        {
            EntityBase product = new Product();
            Console.WriteLine(product.ToString());
            Console.ReadLine();
        }
    }


    public class EntityBase
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            foreach (var item in this.GetType().GetProperties())
            {
                sb.AppendLine(item.Name + "-" + item.GetValue(this, null)+";");
            }
            return sb.ToString();
        }
    }

    public class Product : EntityBase
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
    }
}
