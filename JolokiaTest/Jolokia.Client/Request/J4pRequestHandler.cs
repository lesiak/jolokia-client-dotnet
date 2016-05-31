using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace Jolokia.Client.Request
{
    public class J4pRequestHandler
    {
        private readonly Uri j4pServerUrl;
        private J4pTargetConfig defaultTargetConfig;


        // Escape a part for usage as part of URI path: / -> \/, \ -> \\
        private static readonly string ESCAPE = "!";
        private static readonly Regex ESCAPE_PATTERN = new Regex(ESCAPE);
        private static readonly Regex SLASH_PATTERN = new Regex("/");
        private String escape(string pPart)
        {
            /* String ret = //ESCAPE_PATTERN.matcher(pPart).replaceAll(ESCAPE + ESCAPE);
                 //  ESCAPE_PATTERN.
                 ESCAPE_PATTERN.Replace(pPart, ESCAPE + ESCAPE);
                 //return SLASH_PATTERN.Ma(ret).replaceAll(ESCAPE + "/");
                 return SLASH_PATTERN.Replace()
             return pPart;*/
            return Uri.EscapeUriString(pPart);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pJ4pServerUrl">URL to remote agent</param>
        /// <param name="pTargetConfig">optional default target configuration for proxy requests</param>
        public J4pRequestHandler(string pJ4pServerUrl, J4pTargetConfig pTargetConfig)
        {
            try
            {
                j4pServerUrl = new Uri(pJ4pServerUrl);
                defaultTargetConfig = pTargetConfig;
            }
            catch (UriFormatException e)
            {
                throw new ArgumentException("Invalid URL " + pJ4pServerUrl, e);
            }
        }

        /// <summary>
        /// Get the HttpRequest for executing the given single request
        /// </summary>
        /// <param name="pRequest">pRequest request to convert</param>
        /// <param name="pPreferredMethod">HTTP method preferred</param>
        /// <param name="pProcessingOptions">optional map of processiong options</param>
        /// <returns>the request used with HttpClient to obtain the result.</returns>
        public HttpRequestMessage getHttpRequest(J4pRequest pRequest, HttpMethod pPreferredMethod,
                                             Dictionary<J4pQueryParameter, string> pProcessingOptions)
        //throws UnsupportedEncodingException, URISyntaxException 
        {
            /* string method = pPreferredMethod;
             if (method == null) {
                 method = pRequest.getPreferredHttpMethod();
             }
             if (method == null) {
                 method = doUseProxy(pRequest) ? HttpPost.METHOD_NAME : HttpGet.METHOD_NAME;
     }
     */
            HttpMethod method = HttpMethod.Get;

            String queryParams = prepareQueryParameters(pProcessingOptions);

            // GET request
            if (method == HttpMethod.Get)
            {
                //if (doUseProxy(pRequest)) {
                //    throw new IllegalArgumentException("Proxy mode can only be used with POST requests");
                // }
                List<string> parts = pRequest.getRequestParts();
                // If parts == null the request decides, that POST *must* be used
                if (parts != null)
                {
                    string baseUrl = prepareBaseUrl(j4pServerUrl);
                    StringBuilder requestPath = new StringBuilder(baseUrl);
                    requestPath.Append(pRequest.getRequestType().getValue());

                    foreach (string p in parts)
                    {
                        requestPath.Append("/");
                        requestPath.Append(escape(p));
                    }
                    //Console.WriteLine(requestPath.ToString());
                    //return new HttpGet(createRequestURI(requestPath.toString(),queryParams));
                    return new HttpRequestMessage(method, requestPath.ToString());
                }
            }

            throw new InvalidOperationException("AAAAAAAAAAA");
            // We are using a post method as fallback
            /*JSONObject requestContent = getJsonRequestContent(pRequest);
            HttpPost postReq = new HttpPost(createRequestURI(j4pServerUrl.getPath(), queryParams));
            postReq.setEntity(new StringEntity(requestContent.toJSONString(),"utf-8"));
            return postReq;
            */

        }

        private string prepareBaseUrl(Uri pUri)
        {
            string baseUri = pUri.ToString();
            /*  if (baseUri == null)
              {
                  return "/";
              }*/
            if (!baseUri.EndsWith("/"))
            {
                return baseUri + "/";
            }
            else {
                return baseUri;
            }
        }


        // prepare query parameters
        private string prepareQueryParameters(Dictionary<J4pQueryParameter, string> pProcessingOptions)
        {
            if (pProcessingOptions != null && pProcessingOptions.Count > 0)
            {
                StringBuilder queryParams = new StringBuilder();
                foreach (KeyValuePair<J4pQueryParameter, String> entry in pProcessingOptions)
                {
                    queryParams.Append(entry.Key.getParam()).Append("=").Append(entry.Value).Append("&");
                }
                return queryParams.ToString(0, queryParams.Length - 1);
            }
            else {
                return null;
            }
        }
     
    }
}