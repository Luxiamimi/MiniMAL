using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MiniMAL.Exceptions
{
    public class UserUnauthorizedException : WebException
    {
        public UserUnauthorizedException() : base("User is unauthorized. Please verify your username and password.")
        {
        }
    }
}
