using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

// The http protocol being used to transport REST requests across the net

namespace TcoRexrothPressConnector.SmartfunctionKit.RestApi
{
    /// <summary>
    /// The class that all the REST resources inherit from, thereby implementing the four REST operations.
    /// </summary>
    public abstract class RestRequest
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        protected RestRequest()
        {
            Timeout = 3000;
            ReadWriteTimeout = 6000;
        }

        /// <summary>
        /// Time out (in milliseconds) to wait for an answer from the httpResponse
        /// Default = 3000; The .Net default is 100000 (= 1.6 minutes)
        /// Please observe that a DNS request can take up to 15 seconds to time out
        ///
        /// </summary>
        protected int Timeout { get; set; }

        /// <summary>
        /// Time out (in milliseconds) to wait for the read/write streams bound to the http request.
        /// Default = 6000; The .Net default is 300000 (= 5 minutes).
        /// </summary>
        protected int ReadWriteTimeout { get; set; }

        /// <summary>
        /// Has to be set, prior to calling one of the REST methods.
        /// </summary>
        protected string ResourcePath { get; set; }

        public string Get()
        {
            return this.Request(m_getMethod, null);
        }

        public string Get(string args)
        {
            // Add arguments - if any
            if (args.Length > 0)
                ResourcePath = ResourcePath + "?" + args;

            return this.Request(m_getMethod, null);
        }

        public string Delete()
        {
            return this.Request(m_deleteMethod, null);
        }

        internal string Delete(string args)
        {
            // Add arguments - if any
            if (args.Length > 0)
                ResourcePath = ResourcePath + "?" + args;

            return this.Request(m_deleteMethod, null);
        }

        public string Put(string args, string body = "")
        {
            // Add arguments - if any
            if (args.Length > 0)
                ResourcePath = ResourcePath + "?" + args;

            // Add the empty body to the put command
            // In the current implementation, only POST contains a "real" body, and PUT contains an empty body
            byte[] buffer = Encoding.ASCII.GetBytes(body);
            return this.Request(m_putMethod, buffer);
        }

        public string Post(string args, string body)
        {
            if (body == null)
                return MyErrorHandler("Body in Post() is null");

            // Add arguments - if any
            if (args.Length > 0)
                ResourcePath = ResourcePath + "?" + args;

            byte[] buffer = Encoding.ASCII.GetBytes(body);
            return this.Request(m_postMethod, buffer);
        }

        private CredentialCache GetCredential()
        {
            if (ResourcePath == null)
                m_logger.Log(MyErrorHandler("No resource path has been specified"));

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            CredentialCache credentialCache = new CredentialCache();
            credentialCache.Add(
                new System.Uri(ResourcePath),
                "Basic",
                new NetworkCredential("admin", "admin")
            );
            return credentialCache;
        }

        private string Request(string httpMethod, byte[] entityBody)
        {
            // Check the resource path has been set - should be an exception
            if (ResourcePath == null)
                return MyErrorHandler("No resource path has been specified");

            // Create and configure our web request
            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(this.ResourcePath);
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (
                senderX,
                certificate,
                chain,
                sslPolicyErrors
            ) =>
            {
                return true;
            };
            //httpRequest.Credentials = GetCredential();
            //httpRequest.PreAuthenticate = true;
            if (httpRequest == null)
                return MyErrorHandler(
                    "HttpWebRequest.Create(" + this.ResourcePath + "); returned null"
                );

            m_logger.Log(
                String.Format(
                    CultureInfo.InvariantCulture,
                    "Entering Rest.Request({0}, {1})",
                    httpMethod,
                    ResourcePath
                )
            );

            // Set some reasonable properties on our Http Request
            httpRequest.Method = httpMethod;
            httpRequest.KeepAlive = false;
            httpRequest.Timeout = Timeout;
            httpRequest.ReadWriteTimeout = ReadWriteTimeout;

            HttpWebResponse httpResponse = null;
            string statusCode = "";
            try
            {
                // If its POST or PUT, add data to the requests sender stream
                if (httpMethod == m_postMethod || httpMethod == m_putMethod)
                {
                    httpRequest.ContentType = "application/json";
                    httpRequest.ContentLength = entityBody.Length;
                    Stream s = httpRequest.GetRequestStream();
                    s.Write(entityBody, 0, entityBody.Length);
                    s.Close();
                }

                httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                if (httpResponse != null)
                    statusCode = httpResponse.StatusCode.ToString();
            }
            catch (WebException e)
            {
                if (httpRequest != null)
                    httpRequest.Abort();
                if (httpResponse != null)
                    httpResponse.Close();

                return MyErrorHandler(e, statusCode);
            }
            catch (ProtocolViolationException e)
            {
                if (httpRequest != null)
                    httpRequest.Abort();
                if (httpResponse != null)
                    httpResponse.Close();

                return MyErrorHandler(e, statusCode);
            }
            catch (InvalidOperationException e)
            {
                if (httpRequest != null)
                    httpRequest.Abort();
                if (httpResponse != null)
                    httpResponse.Close();

                return MyErrorHandler(e, statusCode);
            }
            catch (NotSupportedException e)
            {
                if (httpRequest != null)
                    httpRequest.Abort();
                if (httpResponse != null)
                    httpResponse.Close();

                return MyErrorHandler(e, statusCode);
            }

            if (httpResponse == null)
                return MyErrorHandler("httpRequest.GetResponse() returned null");

            string response;
            using (
                StreamReader streamReader = new StreamReader(
                    httpResponse.GetResponseStream(),
                    Encoding.ASCII
                )
            )
            {
                response = streamReader.ReadToEnd();
            }

            httpResponse.Close();

            if (m_logger.TraceInfo)
            {
                m_logger.Log(
                    String.Format(
                        CultureInfo.InvariantCulture,
                        "Leaving Rest.Request({0}, {1}), [StatusCode={2}]",
                        httpMethod,
                        ResourcePath,
                        statusCode
                    )
                );
                m_logger.Log(
                    String.Format(CultureInfo.InvariantCulture, "Response = {0}", response)
                );
                m_logger.Log("");
            }

            return response;
        }

        private string MyErrorHandler(SystemException e, string statusCode)
        {
            Type t = e.GetType();

            string status = "";
            if (t == typeof(WebException))
                status = ((WebException)e).Status.ToString();

            m_logger.LogIf(TraceLevel.Error, "-- RestError ---------------------");
            m_logger.LogIf(TraceLevel.Error, "     Resource  = " + ResourcePath);
            m_logger.LogIf(TraceLevel.Error, "    StatusCode = " + statusCode);
            m_logger.LogIf(TraceLevel.Error, "          Type = " + t);
            m_logger.LogIf(TraceLevel.Error, "       Message = " + e.Message);
            m_logger.LogIf(TraceLevel.Error, "        Status = " + status);
            m_logger.LogIf(TraceLevel.Error, "----------------------------------");

            return String.Format(
                CultureInfo.InvariantCulture,
                "<*error*>\nResource={0}\nStatusCode={1}\nType={2}\nMessage={3}\nStatus={4}",
                ResourcePath,
                statusCode,
                t,
                e.Message,
                status
            );
        }

        private string MyErrorHandler(string errorMsg)
        {
            m_logger.LogIf(TraceLevel.Error, "-- RestError ---------------------");
            m_logger.LogIf(TraceLevel.Error, "     Resource  = " + ResourcePath);
            m_logger.LogIf(TraceLevel.Error, "     ErrorMsg  = " + errorMsg);
            m_logger.LogIf(TraceLevel.Error, "----------------------------------");

            return String.Format(
                CultureInfo.InvariantCulture,
                "<*error*>\nResource={0}\nErrorMsg={1}",
                ResourcePath,
                errorMsg
            );
        }

        private const string m_getMethod = "GET";
        private const string m_putMethod = "PUT";
        private const string m_postMethod = "POST";
        private const string m_deleteMethod = "DELETE";

        private static Logger m_logger = Logger.Create(typeof(RestRequest));
    }
}
