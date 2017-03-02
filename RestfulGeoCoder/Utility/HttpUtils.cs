using System;
using System.IO;
using System.Net;
using System.Text;

public enum HttpVerb
{
    Get,
    Post,
    Put,
    Delete
}

namespace RestfulGeocoder.HttpUtils
{
    /// <summary>
    /// Provides a client for HTTP REST calls
    /// </summary>
    public class HttpUtils
    {
        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }

        public HttpUtils()
        {
            EndPoint = "";
            Method = HttpVerb.Get;
            ContentType = "text/xml";
            PostData = "";
        }
        public HttpUtils(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.Get;
            ContentType = "text/xml";
            PostData = "";
        }
        public HttpUtils(string endpoint, HttpVerb method)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/xml";
            PostData = "";
        }

        public HttpUtils(string endpoint, HttpVerb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/xml";
            PostData = postData;
        }


        public string MakeRequest()
        {
            return MakeRequest("");
        }

        /// <summary>
        /// Make a REST request
        /// </summary>
        /// <param name="parameters">The parameters to include in the URL string</param>
        /// <returns>The response</returns>
        public string MakeRequest(string parameters)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;

            if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.Post)
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);

                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                // grab the response
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }

                return responseValue;
            }
        }

        /// <summary>
        /// Replaces reserved URL characters with their encoded value
        /// </summary>
        /// <param name="myString">The string to sanitize</param>
        /// <returns>The sanitized string</returns>
        public string SanitizeUrlString(string myString)
        {
            myString = myString.Replace("%", "%25");
            myString = myString.Replace("<", "%3C");
            myString = myString.Replace(">", "%3E");
            myString = myString.Replace("#", "%23");
            myString = myString.Replace("{", "%7B");
            myString = myString.Replace("}", "%7D");
            myString = myString.Replace("|", "%7C");
            myString = myString.Replace("\\", "%5C");
            myString = myString.Replace("^", "%5E");
            myString = myString.Replace("~", "%7E");
            myString = myString.Replace("[", "%5B");
            myString = myString.Replace("]", "%5D");
            myString = myString.Replace("`", "%60");
            myString = myString.Replace(";", "%3B");
            myString = myString.Replace("/", "%2F");
            myString = myString.Replace("?", "%3F");
            myString = myString.Replace(":", "%3A");
            myString = myString.Replace("@", "%40");
            myString = myString.Replace("=", "%3D");
            myString = myString.Replace("&", "%26");
            myString = myString.Replace("$", "%24");

            return myString;
        }

    } // class
}