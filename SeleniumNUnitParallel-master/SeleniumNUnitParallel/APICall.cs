using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNUnitParallel
{
    public class APICall
    {

        private const string m_BaseUrl = "";
        public static string result;

        /// <summary>
        /// <param name="resource">The kind of resource to ask for</param>
        /// <param name="argument">Any argument that needs to be passed, such as a project key</param>
        /// <param name="data">More advanced data sent in POST requests</param>
        /// <param name="method">Either GET or POST</param>
        /// <param name="APIBase">Base or DevStatus</param>
        /// <returns></returns>
        public static string RunQuery(string url, string resource = null, string argument = null, string data = null, string method = "GET")
        {

            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                request.CachePolicy = noCachePolicy;
                request.ContentType = "application/json";
                request.Method = method;

                if (data != null)
                {
                    using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                    {
                        writer.Write(data);
                    }
                }

                string base64Credentials = GetEncodedCredentials();
                request.Headers.Add("Authorization", "Basic " + base64Credentials);

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                string result = string.Empty;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static string fetchAPIResult()
        {

            result = RunQuery("http://54.169.34.162:5252/video", method: "GET");
            return result;

        }

        private static string GetEncodedCredentials()
        {
            string mergedCredentials = string.Format("{0}:{1}", "m_Username", "m_Password");
            byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);
            return Convert.ToBase64String(byteCredentials);
        }
    }
}
