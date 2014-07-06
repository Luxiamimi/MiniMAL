namespace MiniMAL.Exceptions
{
    public class UserNotConnectedException : MiniMALException
    {
        public UserNotConnectedException()
            : base("User is not connected to MiniMAL client. Please log in and retry.") {}
    }
}