using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace TradeRobo.Service
{
    public class RestClient
    {
        private HttpClient _client;

        public RestClient()
        {
            string proxyServer = "";

            if (File.Exists(Settings.CSVPath + "proxy.txt"))
                proxyServer = File.ReadAllText(Settings.CSVPath + "proxy.txt", Encoding.UTF8);

            if (!string.IsNullOrWhiteSpace(proxyServer))
            {
                var proxy = new WebProxy
                {
                    Address = new Uri(proxyServer),
                };

                // Now create a client handler which uses that proxy
                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = proxy,
                };


                _client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
            }
            else

                _client = new HttpClient();
        }


        public void SetHeaders(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helper.Decrypt(token));
        }


        public async Task<string> GetAsync(string url)
        {
            var stringTrask = await _client.GetStringAsync(url);
            return stringTrask;
        }

        public string Get(string url)
        {
            var response = _client.GetAsync(url).Result;

            return HandleException(response);
        }


        public string Post(string url, Dictionary<string, object> payload)
        {
            var json = JsonConvert.SerializeObject(payload);

            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = _client.PostAsync(url, data).Result;

            return HandleException(response);

        }

        private string HandleException(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsStringAsync().Result;
            else if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                return response.Content.ReadAsStringAsync().Result;
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException(response.Content.ReadAsStringAsync().Result);
            else
                throw new Exception(response.Content.ReadAsStringAsync().Result);
        }


    }
}
