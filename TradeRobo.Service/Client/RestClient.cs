using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OAuth;


namespace TradeRobo.Service
{
    public class RestClient
    {
        private HttpClient _client;

        const string ConsumerKey = "t3903BjavPCl1ohu4kMlL6YFSreBJjBWF7Suxmh3q0Q3";
        const string ConsumerSecret = "C19mianx7G4dgqUZPaJG0xyspeHNJPhUiVt0y2xTHvs8";
        const string OAuthToken = "1aho2OWNLNif3KZy0O2aKFUJCRYjDqK84jVlNE1TUhM8";
        const string OAuthTokenSecret = "K3EDin3cn0MtRSdaK45EHfo3JAk08SAVtB3s2KWdS6o7";

        public RestClient()
        {

            if (Settings.UseProxy )
            {
                var proxy = new WebProxy
                {
                    Address = new Uri(Settings.Proxy),
                };

                // Now create a client handler which uses that proxy
                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = proxy,
                };

                httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                _client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
            }
            else

                _client = new HttpClient();

            
        }


        public void SetHeaders(string key, string value)
        {
            _client.DefaultRequestHeaders.Add(key, value);
        }

        public void SetHeaders(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helper.Decrypt(token));
        }

        public void SetOAuth1Headers(string url)
        {
            OAuthRequest authClient = OAuthRequest.ForProtectedResource("POST", ConsumerKey, ConsumerSecret, OAuthToken, OAuthTokenSecret);
            authClient.RequestUrl = url;
            var auth = authClient.GetAuthorizationHeader();

            _client.DefaultRequestHeaders.Add("Authorization", auth);
        }

        //public void SetHeaders()
        //{   if (!string.IsNullOrWhiteSpace(TDSettings.access_token))
        //        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helper.Decrypt(TDSettings.access_token));
        //    else
        //        throw new UnauthorizedAccessException();
        //}

        public void ClearHeaders()
        {
            _client.DefaultRequestHeaders.Clear();
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

        public string Get(string url, Dictionary<string, string> payload)
        {
            var content = new FormUrlEncodedContent(payload);

            var query = content.ReadAsStringAsync().Result;

            return Get(url + "?" + query);
        }


        public string Post(string url, Dictionary<string, object> payload)
        {
            var json = JsonConvert.SerializeObject(payload);

            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = _client.PostAsync(url, data).Result;

            return HandleException(response);

        }

        public string PostAsRaw(string url, string payload)
        {
            SetOAuth1Headers(url);

            var data = new StringContent(payload);

            var response = _client.PostAsync(url, data).Result;

            return HandleException(response);

        }


        public string PostAsForm(string url, List<KeyValuePair<string, string>> payload)
        {

            ClearHeaders();


            var req = new HttpRequestMessage(HttpMethod.Post, url);

            req.Content = new FormUrlEncodedContent(payload);

            //var res = await client.SendAsync(req);

            //var json = JsonConvert.SerializeObject(payload);

            //var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = _client.SendAsync(req).Result;

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
