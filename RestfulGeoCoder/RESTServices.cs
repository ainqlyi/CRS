using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using RestfulGeoCoder.GISObjects.Request;
using RestfulGeoCoder.GISObjects.Response;

namespace RestfulGeoCoder
{
    /// <summary>
    /// Contains methods for making REST calls to ArcGIS
    /// </summary>
    public class RestServices
    {
        /// <summary>
        /// Get token from ArcGIS Web Service
        /// </summary>
        /// <param name="url">The url to use for getting the token</param>
        /// <returns>Token string</returns>
        public static string GetToken(string url)
        {
            string token = null, errorMsg;

            WebRequest request = WebRequest.Create(url);

            try
            {
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                if (responseStream != null)
                {
                    StreamReader readStream = new StreamReader(responseStream);
                    token = readStream.ReadToEnd();
                }
            }
            catch (WebException we)
            {
                if (we.Message.Contains("403"))
                {
                    errorMsg = "Server returned forbidden (403) code.";
                }
            }
            return token;
        }

        /// <summary>
        /// Makes a REST call using the serialized RootObject and returns the json results into the RootObject
        /// </summary>
        /// <param name="rootRequestObject">The object to serialize and send in the REST call</param>
        /// <param name="token">The token to use in the REST call to ArcGIS</param>
        /// <returns>The object to deserialize after making the REST call</returns>
        public static RootResponseObject GeocodeABatchOfAddresses(RootRequestObject rootRequestObject, string token)
        {
            string jsonAddressRequest = "addresses=" + JsonConvert.SerializeObject(rootRequestObject);

            string endPoint = String.Format(
                ConfigurationManager.AppSettings["arcGISBaseAddress"].ToString(CultureInfo.InvariantCulture) +
                "/arcgis/rest/services/Geocode/USA/GeocodeServer/geocodeAddresses",
                token);
            var client = new RestfulGeocoder.HttpUtils.HttpUtils(endPoint, HttpVerb.Post, jsonAddressRequest);
            client.ContentType = "application/x-www-form-urlencoded";
            var json = client.MakeRequest(String.Format("?sourceCountry=USA&token={0}&f=json", token));

            RootResponseObject rootResponseObject = JsonConvert.DeserializeObject<RootResponseObject>(json);

            if (rootResponseObject.locations == null)
            {
                GeoCodeAddresses.Log.Debug(String.Format("rootResponseObject.locations is null")); 
            }
            else
            {
                GeoCodeAddresses.Log.Debug(String.Format("GeocodeABatchOfAddresses: rootResponseObject.locations.Count = {0}",
                                         rootResponseObject.locations.Count)); 
            }
            
            return rootResponseObject;
        }
    }
}