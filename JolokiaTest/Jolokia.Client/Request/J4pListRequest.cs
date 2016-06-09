using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Jolokia.Client.Request
{
    public class J4pListRequest : J4pRequest<J4pListResponse>
    {

        private static readonly Regex SLASH_ESCAPE_PATTERN = new Regex("((?:[^!/]|!.)*)(?:/|$)");

        private readonly List<string> pathElements;
        
        /// <summary>
        /// Constructor using a path to restrict the information
        /// returned by the list command
        /// </summary>
        /// <param name="pPath">
        /// path into the JSON response. The path <strong>must already be 
        /// properly escaped</strong> when it contains slashes or exclamation marks.
        ///  You can use {@link #escape(String)} in order to escape a single path element.
        /// </param>
        public J4pListRequest(string pPath) : this(null, pPath)
        {            
        }

        /// <summary>
        /// Constructor using a path to restrict the information
        /// returned by the list command
        /// </summary>
        /// <param name="pConfig">proxy target configuration or <code>null</code> if no proxy should be used</param>
        /// <param name="pPath">
        /// path into the JSON response. The path <strong>must already be 
        /// properly escaped</strong> when it contains slashes or exclamation marks.
        /// You can use {@link #escape(String)} in order to escape a single path element.
        /// </param>
        public J4pListRequest(J4pTargetConfig pConfig, string pPath) : base(J4pType.LIST, pConfig)
        {            
            pathElements = SplitPath(pPath);
        }


        public override List<string> GetRequestParts()
        {
            return pathElements;
        }

        internal override J4pListResponse CreateResponse(JObject pResponse)
        {
            return new J4pListResponse(this, pResponse);
        }

        /// <summary>
        /// Split up a path taking into account proper escaping (as described in the
        /// <a href="http://www.jolokia.org/reference">reference manual</a>).
        /// </summary>
        /// <param name="pArg">string to split with escaping taken into account</param>
        /// <returns>split element or null if the argument was null.</returns>
        public static List<string> SplitPath(string pArg)
        {
            List<string> ret = new List<string>();
            if (pArg != null)
            {
                MatchCollection matches = SLASH_ESCAPE_PATTERN.Matches(pArg);
                foreach (Match m in matches)
                {
                    if (m.Index != pArg.Length)
                    {                        
                        ret.Add(m.Groups[1].Value);
                        //ret.add(UNESCAPE_PATTERN.matcher(m.group(1)).replaceAll("$1"));
                    }
                }                
            }
            return ret;
        }


    }
}