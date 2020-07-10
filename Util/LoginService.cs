using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace TradeRobo.Service
{
    public class LoginService
    {
        RestClient client;
        public LoginService()
        {
            client = new RestClient();
        }


        public void Login(Credentials loginDetails, IJwtToken token)
        {

            token.isAuthenticated = false;

            String DeviceToken = "";

            Dictionary<string, object> payload = new Dictionary<string, object>();
            payload.Add("client_id", Settings.ClientId );
            payload.Add("grant_type", "password");
            payload.Add("expires_in", 86400);
            payload.Add("scope", "internal");
            payload.Add("username", loginDetails.userName);
            payload.Add("password", loginDetails.userName);

            if (!string.IsNullOrWhiteSpace(loginDetails.deviceToken))
                DeviceToken = Helper.Decrypt(loginDetails.deviceToken);

            if (string.IsNullOrWhiteSpace(DeviceToken))
            {
                DeviceToken = Helper.GenerateDeviceToken();
            }

            if (!string.IsNullOrWhiteSpace(loginDetails.mfaToken)) { 
                payload.Add("mfa_code", loginDetails.mfaToken);
            }
            

            payload.Add("device_token", DeviceToken);

            var url = "https://api.robinhood.com/oauth2/token/";


            var response = client.Post(url, payload);

            var result = JsonConvert.DeserializeObject<Dictionary<string,object>>(response);

            if (result.TryGetValue("access_token", out object value))
            {
                token.isAuthenticated = true;
                token.accessToken = Helper.Encrypt(value.ToString());
            }
            token.deviceToken = Helper.Encrypt(DeviceToken);
            token.userName = loginDetails.userName;

        }
    

    }
}
