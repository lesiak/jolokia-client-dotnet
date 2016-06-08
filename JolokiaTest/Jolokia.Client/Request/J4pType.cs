namespace Jolokia.Client.Request
{
    public class J4pType
    {
        public static J4pType READ = new J4pType("read");

        public static J4pType VERSION = new J4pType("version");

        public static J4pType LIST = new J4pType("list");

        private readonly string value;

        private J4pType(string pValue)
        {
            value = pValue;
        }

        public string getValue()
        {
            return value;
        }
    }
}