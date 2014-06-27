namespace MiniMAL.Exceptions
{
    public class UserUnauthorizedException : MiniMALException
    {
        public UserUnauthorizedException()
            : base("User is unauthorized. Please verify your username and password.")
        {
        }
    }
}