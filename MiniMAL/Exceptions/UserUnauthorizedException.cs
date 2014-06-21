using System.Net;

namespace MiniMAL.Exceptions
{
    public class UserUnauthorizedException : WebException
    {
        public UserUnauthorizedException()
            : base("User is unauthorized. Please verify your username and password.")
        {
        }
    }
}