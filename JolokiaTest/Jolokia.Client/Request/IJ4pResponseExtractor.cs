using System.Collections.Generic;

namespace Jolokia.Client.Request
{
    public interface IJ4pResponseExtractor
    {
        /// <summary>
        /// Extract a response object for the given request and the returned JSON structure
        /// </summary>
        /// <typeparam name="RESP">response type</typeparam>
        /// <typeparam name="REQ">request type</typeparam>
        /// <param name="request">the original request</param>
        /// <param name="jsonResp"></param>
        /// <returns> the created response</returns>
        RESP Extract<RESP>(J4pRequest<RESP> request, Dictionary<string, object> jsonResp);

        //where REQ : J4pRequest
        //where RESP : J4pResponse<REQ>;

        //throws J4pRemoteException;
    }
}