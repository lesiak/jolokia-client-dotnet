using Newtonsoft.Json.Linq;

namespace Jolokia.Client.Request
{
    public interface IJ4pResponseExtractor
    {
        /// <summary>
        /// Extract a response object for the given request and the returned JSON structure
        /// </summary>
        /// <typeparam name="RESP">response type</typeparam>      
        /// <param name="request">the original request</param>
        /// <param name="jsonResp"></param>
        /// <returns> the created response</returns>
        RESP Extract<RESP>(J4pRequest<RESP> request, JObject jsonResp) where RESP : class, IJ4pResponse;
        //throws J4pRemoteException;
    }
}