namespace MiniMAL.Exceptions
{
    public class NotAvailableException : MiniMALException
    {
        public NotAvailableException()
            : base("Client is already sending a request. You can't send twice at the same time.")
        {
        }
    }
}