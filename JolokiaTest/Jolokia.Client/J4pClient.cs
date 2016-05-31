﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;


namespace Jolokia.Client
{
    public class J4pClient
    {
        // Http client used for connecting the j4p Agent
        private HttpClient httpClient;

        // Creating and parsing HTTP-Requests and Responses
        private J4pRequestHandler requestHandler;

        // Extractor used for creating J4pResponses
        private IJ4pResponseExtractor responseExtractor;

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
        public J4pClient(string pJ4pServerUrl, HttpClient pHttpClient, J4pTargetConfig pTargetConfig) : this(pJ4pServerUrl, pHttpClient, pTargetConfig, ValidatingResponseExtractor.DEFAULT)
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
        public J4pClient(String pJ4pServerUrl, HttpClient pHttpClient, J4pTargetConfig pTargetConfig, IJ4pResponseExtractor pExtractor)
        {
            requestHandler = new J4pRequestHandler(pJ4pServerUrl, pTargetConfig);
            responseExtractor = pExtractor;
            
            // Using the default as defined in the client builder
            if (pHttpClient != null)
            {
                httpClient = pHttpClient;
            }
            else {
               // J4pClientBuilder builder = new J4pClientBuilder();
                //httpClient = builder.createHttpClient();
                httpClient = new HttpClient();
            }
            
        }

        public Task<J4pReadResponse> Execute(J4pReadRequest pRequest)                   
            //throws J4pException
        {
            return Execute<J4pReadResponse, J4pReadRequest>(pRequest, null, null);
        }

        /*  public Task<RESP> Execute<REQ, RESP>(REQ pRequest)
              where REQ : J4pRequest
              where RESP : J4pResponse<REQ>

              //throws J4pException
          {
              // type spec is required to keep OpenJDK 1.6 happy (other JVM dont have a problem
              // with infering the type is missing here)
              return Execute<RESP, REQ>(pRequest, null, null);

          }*/



        /// <summary>
        /// Execute a single J4pRequest which returns a single response.
        /// </summary>
        /// <typeparam name="RESP">response type</typeparam>
        /// <typeparam name="REQ">request type</typeparam>
        /// <param name="pRequest">pRequest request to execute</param>
        /// <param name="pMethod">pMethod method to use which should be either "GET" or "POST"</param>
        /// <param name="pProcessingOptions">pProcessingOptions optional map of processing options</param>
        /// <returns>response object</returns>
        public Task<RESP> Execute<RESP, REQ>(REQ pRequest, HttpMethod pMethod, Dictionary<J4pQueryParameter, string> pProcessingOptions)
            where REQ : J4pRequest
            where RESP : J4pResponse<REQ>             
        {
            return Execute<RESP, REQ>(pRequest,pMethod,pProcessingOptions,responseExtractor);
        }


        /**
     * Execute a single J4pRequest which returns a single response.
     *
     * @param pRequest request to execute
     * @param pMethod method to use which should be either "GET" or "POST"
     * @param pProcessingOptions optional map of processing options
     * @param pExtractor extractor for actually creating the response
     *
     * @param <RESP> response type
     * @param <REQ> request type
     * @return response object
     * @throws J4pException if something's wrong (e.g. connection failed or read timeout)
     */
        public async Task<RESP> Execute<RESP, REQ> (REQ pRequest, HttpMethod pMethod,Dictionary<J4pQueryParameter, String> pProcessingOptions,
                                                                         IJ4pResponseExtractor pExtractor)
            where REQ : J4pRequest
            where RESP : J4pResponse<REQ>
            
            //throws J4pException
        {

        try
        {
            HttpRequestMessage requestMessage = requestHandler.getHttpRequest(pRequest, pMethod, pProcessingOptions);
            //HttpResponse response = httpClient.execute(requestMessage);
            HttpResponseMessage response = await httpClient.SendAsync(requestMessage);
                
            string resp = await response.Content.ReadAsStringAsync();
            Console.WriteLine(resp);
             
                //JSONAware jsonResponse = extractJsonResponse(pRequest, response);
                //if (!(jsonResponse instanceof JSONObject)) {
                //  throw new J4pException("Invalid JSON answer for a single request (expected a map but got a " + jsonResponse.getClass() + ")");
                //}
                //return pExtractor.extract(pRequest, (JSONObject)jsonResponse);
            }
            catch (IOException e) {
                //throw mapException(e);
            //} catch (URISyntaxException e) {
                //throw mapException(e);
            }
            return null;
        }
    }

   
    public class J4pTargetConfig
    {
    }

    public class ValidatingResponseExtractor : IJ4pResponseExtractor
    {
        public static IJ4pResponseExtractor DEFAULT;
    }
}