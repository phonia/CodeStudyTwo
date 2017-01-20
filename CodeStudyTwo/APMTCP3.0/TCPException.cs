using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APMTCP3._0
{
    public class CustomTCPException:Exception
    {
        public CustomTCPException(String message) : base(message) { }
    }
}
