using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TcoRexrothPressConnector.SmartfunctionKit.RestApi
{
    public class Client
    {
        public class RestClient : RestRequest
        {
            public RestClient(string s)
            {
                base.ResourcePath = s;
            }
        }

        public Client(string address)
        {
            m_address = address;
            m_uri = "https://" + address + "/api";
        }

        public string m_uri;
        public string m_address;
        private readonly string _userName;
        private readonly string _password;
        private static Logger m_logger = Logger.Create(typeof(RestRequest));

        public RestClient Api(string s)
        {
            return new RestClient(m_uri + "/" + s);
        }

        public CurveItem GetLastCurveData()
        {
            var resp = this.Api("curves?page=0&size=1&sort={\"_id\":\"desc\"}").Get();
            var removeRootBracket = resp.Substring(1, resp.Length - 2);
            var curve = JsonConvert.DeserializeObject<CurveItem>(removeRootBracket);

            return curve;
        }

        public CurveItem GetCurveDataById(string id)
        {
            var resp = this.Api("curves/" + id).Get();
            var removeRootBracket = resp.Substring(1, resp.Length - 2);
            var curve = JsonConvert.DeserializeObject<CurveItem>(removeRootBracket);

            return curve;
        }

        public CurveItem GetCurveDataByCustomId(string id)
        {
            var resp = this.Api(
                    "curves?page=0&size=1&q={\"customId\": {\"$eq\":\"" + id + "\"}}" + id
                )
                .Get();
            var removeRootBracket = resp.Substring(1, resp.Length - 2);
            var curve = JsonConvert.DeserializeObject<CurveItem>(removeRootBracket);

            return curve;
        }
    }
}
