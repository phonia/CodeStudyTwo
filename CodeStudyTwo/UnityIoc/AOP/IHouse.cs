using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace UnityIoc.AOP
{
    public enum HouseVisiter
    {
        Owner,Chieves,Policy,Robber
    }

    public interface IHouse
    {
        void OpenTheDoor(HouseVisiter houseVisiter);
        void BurnTheHouse(HouseVisiter houseVisiter);
    }

    public class House : IHouse
    {
        #region IHouse 成员

        public void OpenTheDoor(HouseVisiter houseVisiter)
        {
            Console.WriteLine(houseVisiter.ToString()+" open the Door!");
        }

        public void BurnTheHouse(HouseVisiter houseVisiter)
        {
            Console.WriteLine(houseVisiter.ToString() + " Burn the House!");
        }

        #endregion
    }

    public class HouseKeeper : IInterceptionBehavior
    {
        #region IInterceptionBehavior 成员

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if ((HouseVisiter)input.Inputs[0] != HouseVisiter.Owner && input.MethodBase.Name.Equals("OpenTheDoor"))
            {
                Console.WriteLine("Illegal invasion");
            }

            if ((HouseVisiter)input.Inputs[0] == HouseVisiter.Owner && input.MethodBase.Name.Equals("BurnTheHouse"))
            {
                Console.WriteLine("The Houser-owner is crazy!");
            }
            return getNext()(input, getNext);
        }

        public bool WillExecute
        {
            get { return true; }
        }

        #endregion
    }
}
