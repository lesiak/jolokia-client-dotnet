using System.Collections.Generic;

namespace Jolokia.Client.Request
{
    public interface IJ4pRequest
    {
        // Get the parts to build up a GET url (without the requestType as the first part)
        List<string> GetRequestParts();
        J4pType RequestType { get; }
    }
}