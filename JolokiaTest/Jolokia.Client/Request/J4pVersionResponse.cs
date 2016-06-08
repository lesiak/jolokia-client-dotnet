using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Jolokia.Client.Request
{
    public class J4pVersionResponse : J4pResponse<J4pVersionRequest>
    {

        private readonly JObject info;

        public J4pVersionResponse(J4pVersionRequest pRequest, JObject pResponse) : base(pRequest, pResponse)
        {
            var value = (IDictionary<string, JToken>) GetValueAsJObject();
            
            AgentVersion = (string)value["agent"];
            ProtocolVersion = (string)value["protocol"];
            /*
            details = (JSONObject)value.get("details");
            jolokiaId = (String)value.get("id");
            if (details == null)
            {
                details = new JSONObject();
            }*/
            info = value["info"] as JObject;
            if (info == null)
            {
                info = new JObject();
            }
        }


        /// <summary>
        /// The version of the Jolokia agent
        /// </summary>
        /// <returns>version</returns>
        public string AgentVersion { get; }

        /// <summary>
        /// Jolokia protocol version by the remote Jolokia agent
        /// </summary>
        /// <returns>protocol version (as string)</returns>
        public string ProtocolVersion { get; }

        /// <summary>
        /// Get all supported realms
        /// </summary>
        /// <returns>set of supported realms</returns>
        public ICollection<string> Realms => ((IDictionary<string, JToken>)info).Keys;
    }
}