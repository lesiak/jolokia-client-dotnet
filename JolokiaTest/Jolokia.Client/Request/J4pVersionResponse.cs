using Newtonsoft.Json.Linq;

namespace Jolokia.Client.Request
{
    public class J4pVersionResponse : J4pResponse<J4pVersionRequest>
    {
        public J4pVersionResponse(J4pVersionRequest pRequest, JObject pResponse) : base(pRequest, pResponse)
        {
            var value = GetValueAsDictionary();
            
            AgentVersion = (string)value["agent"];
            /*protocolVersion = (String)value.get("protocol");
            details = (JSONObject)value.get("details");
            jolokiaId = (String)value.get("id");
            if (details == null)
            {
                details = new JSONObject();
            }
            info = (JSONObject)value.get("info");
            if (info == null)
            {
                info = new JSONObject();
            }*/
        }


        /// <summary>
        /// The version of the Jolokia agent
        /// </summary>
        /// <returns>version</returns>
        public string AgentVersion { get; }
    }
}