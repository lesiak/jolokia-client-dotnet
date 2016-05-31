
namespace Jolokia.Client.Jmx
{
    public class ObjectName
    {

        private readonly string _canonicalName ;

        public ObjectName(string name)
        {
            // int aaa = 0;
            //TODO: this is too simple
            _canonicalName = name;
        }

        public string getCanonicalName()
        {            
            return _canonicalName;
        }

    }
}