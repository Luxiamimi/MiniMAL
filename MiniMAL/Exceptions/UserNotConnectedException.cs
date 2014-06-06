using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MiniMAL.Exceptions
{
    public class UserNotConnectedException : Exception
    {
        public UserNotConnectedException() : base("User is not connected to MiniMAL client. Use command \"login\".")
        {
        }
    }
}
