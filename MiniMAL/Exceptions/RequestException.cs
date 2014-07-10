namespace MiniMAL.Exceptions
{
    public class RequestException : MiniMALException
    {
        public RequestException(string response)
            : base(response) { }
    }
}