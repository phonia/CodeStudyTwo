using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LazyLoading
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var lazy = new LazyLoadingDataContext())
            {
                lazy.Departments.Add(new Department() { Name = "ss" });
                lazy.SaveChanges();
            }
        }
    }
}
