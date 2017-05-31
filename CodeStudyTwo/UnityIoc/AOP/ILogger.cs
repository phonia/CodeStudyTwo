using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace UnityIoc.AOP
{
    public interface ILogger
    {
        void Log();
    }

    public class ConsoleLog : ILogger
    {
        #region ILogger 成员

        public void Log()
        {
            Console.WriteLine("Write Down a piece of log!");
        }

        #endregion
    }

    public class LogHandler : ICallHandler
    {
        #region ICallHandler 成员

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            Console.WriteLine("CallHandler!");
            return getNext()(input, getNext);
        }

        public int Order { get; set; }

        #endregion
    }

    public class SelfRule : IMatchingRule
    {
        #region IMatchingRule 成员

        public bool Matches(System.Reflection.MethodBase member)
        {
            if (member.DeclaringType == typeof(ConsoleLog)) return true;
            return true;
        }

        #endregion
    }
}
