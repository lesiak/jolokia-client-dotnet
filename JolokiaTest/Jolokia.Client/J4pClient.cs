using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Jolokia.Client.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Jolokia.Client
{
    public class J4pClient
    {
        // Http client used for connecting the j4p Agent
        private readonly HttpClient httpClient;

        // Creating and parsing HTTP-Requests and Responses
        private readonly J4pRequestHandler requestHandler;

        // Extractor used for creating J4pResponses
        private readonly IJ4pResponseExtractor responseExtractor;

        /// <summary>
        /// Construct a new client for a given server url
        /// </summary>
        /// <param name="pJ4pServerUrl">the agent URL for how to contact the server.</param>
        public J4pClient(string pJ4pServerUrl) : this(pJ4pServerUrl, null)
        {
        }

        /// <summary>
        /// Constructor for a given agent URl and a given HttpClient
        /// </summary>
        /// <param name="pJ4pServerUrl">the agent URL for how to contact the server.</param>
        /// <param name="pHttpClient">HTTP client to use for the connecting to the agent</param>
        public J4pClient(string pJ4pServerUrl, HttpClient pHttpClient) : this(pJ4pServerUrl, pHttpClient, null)
        {
        }


        /// <summary>
        /// Constructor using a given Agent URL, HttpClient and a proxy target config. If the HttpClient is null,
        /// a default client is used.If no target config is given, a plain request is performed
        /// </summary>
        /// <param name="pJ4pServerUrl">the agent URL for how to contact the server.</param>
        /// <param name="pHttpClient">HTTP client to use for the connecting to the agent</param>
        /// <param name="pTargetConfig">optional target</param>
        public J4pClient(string pJ4pServerUrl, HttpClient pHttpClient, J4pTargetConfig pTargetConfig)
            : this(pJ4pServerUrl, pHttpClient, pTargetConfig, ValidatingResponseExtractor.DEFAULT)
        {
        }


        /// <summary>
        /// Constructor using a given Agent URL, HttpClient and a proxy target config.If the HttpClient is null,
        /// a default client is used.If no target config is given, a plain request is performed
        /// </summary>
        /// <param name="pJ4pServerUrl">the agent URL for how to contact the server.</param>
        /// <param name="pHttpClient"> HTTP client to use for the connecting to the agent</param>
        /// <param name="pTargetConfig">optional target</param>
        /// <param name="pExtractor">response extractor to use</param>
        public J4pClient(string pJ4pServerUrl, HttpClient pHttpClient, J4pTargetConfig pTargetConfig,
            IJ4pResponseExtractor pExtractor)
        {
            requestHandler = new J4pRequestHandler(pJ4pServerUrl, pTargetConfig);
            responseExtractor = pExtractor;

            // Using the default as defined in the client builder
            if (pHttpClient != null)
            {
                httpClient = pHttpClient;
            }
            else
            {
                // J4pClientBuilder builder = new J4pClientBuilder();
                //httpClient = builder.createHttpClient();
                httpClient = new HttpClient();
            }

        }


        public Task<RESP> Execute<RESP>(J4pRequest<RESP> pRequest) where RESP : class, IJ4pResponse
            //throws J4pException
        {            
            return Execute(pRequest, null, null);
        }


        /// <summary>
        /// Execute a single J4pRequest which returns a single response.
        /// </summary>
        /// <typeparam name="RESP">response type</typeparam>        
        /// <param name="pRequest">pRequest request to execute</param>
        /// <param name="pMethod">pMethod method to use which should be either "GET" or "POST"</param>
        /// <param name="pProcessingOptions">pProcessingOptions optional map of processing options</param>
        /// <returns>response object</returns>
        public Task<RESP> Execute<RESP>(
            J4pRequest<RESP> pRequest,
            HttpMethod pMethod,
            Dictionary<J4pQueryParameter, string> pProcessingOptions)
            where RESP : class, IJ4pResponse
        {
            return Execute(pRequest, pMethod, pProcessingOptions, responseExtractor);
        }

        /// <summary>
        /// Execute a single J4pRequest which returns a single response.
        /// </summary>
        /// <typeparam name="RESP">response type</typeparam>
        /// <param name="pRequest">request to execute</param>
        /// <param name="pMethod">method to use which should be either "GET" or "POST"</param>
        /// <param name="pProcessingOptions">optional map of processing options</param>
        /// <param name="pExtractor">extractor for actually creating the response</param>
        /// <returns>response object</returns>
        public async Task<RESP> Execute<RESP>(J4pRequest<RESP> pRequest,
            HttpMethod pMethod,
            Dictionary<J4pQueryParameter, string> pProcessingOptions,
            IJ4pResponseExtractor pExtractor)
            where RESP : class, IJ4pResponse
            //throws J4pException
        {

            try
            {
                HttpRequestMessage requestMessage = requestHandler.getHttpRequest(pRequest, pMethod, pProcessingOptions);                
                HttpResponseMessage response = await httpClient.SendAsync(requestMessage);
              
                JObject jsonResponse = await ExtractJsonResponse(pRequest, response);
                //if (!(jsonResponse instanceof JSONObject)) {
                //  throw new J4pException("Invalid JSON answer for a single request (expected a map but got a " + jsonResponse.getClass() + ")");
                //}
                return pExtractor.Extract<RESP>(pRequest, jsonResponse);

            }
            catch (IOException e)
            {
                //throw mapException(e);
                //} catch (URISyntaxException e) {
                //throw mapException(e);
            }
            return null;
            //return default(RESP);
        }


        private async Task<JObject> ExtractJsonResponse(IJ4pRequest pRequest, HttpResponseMessage pResponse)
        //throws J4pException
        {
            string responseContent = await pResponse.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);           
            JObject jobject = JObject.Parse(responseContent);
            //string s = string.Join(";", htmlAttributes.Select(x => x.Key + "=" + x.Value));
            //Console.WriteLine(s);
            Console.WriteLine(jobject);            
            return jobject;

            //try {
            //return requestHandler.extractJsonResponse(pResponse);
            /*} catch (IoOException e) {
            throw new J4pException("IO-Error while reading the response: " + e, e);
        } catch (ParseException e) {
            // It's a parse exception. Now, check whether the HTTResponse is
            // an error and prepare the proper J4pException
            StatusLine statusLine = pResponse.getStatusLine();
            if (HttpStatus.SC_OK != statusLine.getStatusCode())
            {
                throw new J4pRemoteException(pRequest, statusLine.getReasonPhrase(), null, statusLine.getStatusCode(), null, null);
            }
            throw new J4pException("Could not parse answer: " + e, e);
        }*/
        }

    }


    public class J4pTargetConfig
    {
    }


}