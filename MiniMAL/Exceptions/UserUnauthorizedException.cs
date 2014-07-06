namespace MiniMAL.Exceptions
{
    public class UserUnauthorizedException : MiniMALException
    {
        public UserUnauthorizedException()
            : base("User unauthorized. Please verify your credentials and retry.") {}
    }
}