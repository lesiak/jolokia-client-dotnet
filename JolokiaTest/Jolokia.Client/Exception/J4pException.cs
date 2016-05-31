namespace Jolokia.Client.Exception
{
    public class J4pException : System.Exception
    {
        public J4pException()
        {
        }

        public J4pException(string message) : base(message)
        {
        }

        public J4pException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}