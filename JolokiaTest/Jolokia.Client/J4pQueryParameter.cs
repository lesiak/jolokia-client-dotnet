namespace Jolokia.Client
{
    public class J4pQueryParameter
    {
        // Query parameter
        private readonly string param;

        J4pQueryParameter(string pParam)
        {
            param = pParam;
        }

        public string getParam()
        {
            return param;
        }
    }
}