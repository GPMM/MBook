using MBook.Infrastructure.Crosscutting.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MBook.Services
{
    public class HueLogicService
    {
        public static string BridgeIP;

        public static string Usercode;

        private const string APIAddressTemplate = "http://{0}/api";
        private const string BodyTemplate = "{{\"devicetype\":\"{0}\"}}";
        private const string LightsUrlTemplate = "http://{0}/api/{1}/{2}";
        private const string ControlLightUrlTemplate = "http://{0}/api/{1}/{2}/{3}/{4}";

        public static void FindBridgeIP(string sIP)
        {
            BridgeIP = sIP;
            
        }

        private static IPAddress GetLocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                return null;

            return Dns.GetHostEntry(Dns.GetHostName())
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        public static string ConnectBridge(string username)
        {
            return PostRequestToBridge(
                 string.Format(APIAddressTemplate, BridgeIP),
                 string.Format(BodyTemplate, username));
        }

        public static string GetBridge()
        {
            return GetRequestToBridge(string.Format(LightsUrlTemplate, BridgeIP, Usercode, "lights"));
        }

        public static void PutBridge(int iHueId, bool bState, long lSat, long lBri, long lHue)
        {
            var data = new ColorLightHelper(5, bState, lSat, lBri, lHue).ToJson();

            PutRequestToBridge(string.Format(ControlLightUrlTemplate, BridgeIP, Usercode, "lights", iHueId, "state"), data);
        }

        private static string PostRequestToBridge(string uri, string data, string contentType = "application/json", string method = "POST")
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = contentType;
            request.Method = method;

            try
            {
                using (Stream requestBody = request.GetRequestStream())
                {
                    requestBody.Write(dataBytes, 0, dataBytes.Length);
                }
            }

            catch
            {
                return null;
            }

            //search for easier way
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();

                var result = ResultHelper.FromJson(json);
                Usercode = result.First().Success.Username;

                return json;
            }
        }

        private static string GetRequestToBridge(string fullUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fullUrl);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        private static void PutRequestToBridge(string fullUri, string data, string contentType = "application/json", string method = "PUT")
        {
            using (var client = new System.Net.WebClient())
            {
                try
                {
                    client.UploadData(fullUri, method, Encoding.UTF8.GetBytes(data));
                }
                catch { }
            }
        }
    }
}
