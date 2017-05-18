using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityIoc
{
    public interface IAnimal
    {
        void Say();
    }

    public class Cow : IAnimal
    {
        public String Name { get; set; }
        #region IAnimal 成员

        public void Say()
        {
            Console.WriteLine("牟......"+Name);
        }

        #endregion
    }

    public class Cat : IAnimal
    {
        public String Name { get; set; }
        #region IAnimal 成员

        public void Say()
        {
            Console.WriteLine("喵..."+Name);
        }

        #endregion
    }

    public class Dog : IAnimal
    {
        public String Name { get; set; }
        #region IAnimal 成员

        public void Say()
        {
            Console.WriteLine("汪...."+Name);
        }

        #endregion
    }
}
