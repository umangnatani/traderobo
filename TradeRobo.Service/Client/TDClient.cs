using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace TradeRobo.Service
{
    public class TDClient
    {

        RestClient client;
        TDToken _token;
        public TDClient(TDToken token)
        {
            client = new RestClient();
            _token = token;
        }


        public Dictionary<string, GlobalQuote> GetQuote(string symbol)
        {
            var list = new Dictionary<string, GlobalQuote>();

            try
            {
                client.SetHeaders(_token.access_token);

                var payload = new Dictionary<string, string>();

                payload.Add("apikey", Settings.TDClientId);
                payload.Add("symbol", symbol);

                var json = client.Get(TDEndPoint.Quotes, payload);

                list = JsonConvert.DeserializeObject<Dictionary<string, GlobalQuote>> (json);
            }
            catch (UnauthorizedAccessException e)
            {
                Authenticate();
                list = GetQuote(symbol);
            }

            return list;
        }


        public void GetAccount()
        {


            //var payload = new Dictionary<string, string>();

            //payload.Add("apikey", Settings.TDClientId);
            //payload.Add("symbol", symbol);

            try
            {

                client.SetHeaders(_token.access_token);

                var response = client.Get(TDEndPoint.Accounts);

                var json = (JObject)JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(response)[0]["securitiesAccount"];

                _token.userName = json.GetValue("accountId").ToString();
            }
            catch (UnauthorizedAccessException e)
            {
                Authenticate();
                GetAccount();
            }


            //token.userName = JsonConvert.DeserializeObject<Dictionary<string, object>>(json)["accountId"].ToString();

        }



        public void PlaceOrder(TDOrder order)
        {

            GetAccount();
            var APIEndPoint = TDEndPoint.Orders.Replace("{accountId}", _token.userName);


            //order.Quote = get_quotes(order.Symbol);

            Dictionary<string, object> payload = new Dictionary<string, object>();



            payload.Add("session", "NORMAL");
            payload.Add("orderStrategyType", order.OrderStrategyType);
            payload.Add("duration", order.TimeInForce);
            payload.Add("orderType", order.Type);

            payload.Add("price", order.Price);


            var orderLeg = new Dictionary<string, object>();

            var orderLegCollection = new List<Dictionary<string, object>>();

            orderLeg.Add("instruction", order.Side);

            orderLeg.Add("quantity", order.Quantity);

            var instrument = new Dictionary<string, object>();

            instrument.Add("symbol", order.Symbol);
            instrument.Add("assetType", "EQUITY");

            orderLeg.Add("instrument", instrument);

            orderLegCollection.Add(orderLeg);

            payload.Add("orderLegCollection", orderLegCollection);



            try
            {
                var returnValue = client.Post(APIEndPoint, payload);

                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(returnValue);


            }
            catch (Exception e)
            {
                order.Result = $"Order failed for {order.Symbol} with error: {e.Message}";
            }


        }




        public void Authenticate()
        {

            var payload = new List<KeyValuePair<string, string>>();
            //payload.Add("client_id", Settings.ClientId + "@AMER.OAUTHAP");
            payload.Add(new KeyValuePair<string, string>("client_id", Settings.TDClientId));
            payload.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
            payload.Add(new KeyValuePair<string, string>("refresh_token", _token.refresh_token));
            payload.Add(new KeyValuePair<string, string>("access_type", ""));
            payload.Add(new KeyValuePair<string, string>("code", ""));
            payload.Add(new KeyValuePair<string, string>("redirect_uri", ""));
            //payload.Add("access_type", "offline");
            //payload.Add("redirect_uri", Settings.TDRedirectURI);
            //payload.Add("code", code);


            var response = client.PostAsForm(TDEndPoint.Login, payload);

            var result = JsonConvert.DeserializeObject<TDToken>(response);

            if (!string.IsNullOrEmpty(result.access_token))
            {
                _token.isNewToken = true;
                _token.access_token = Helper.Encrypt(result.access_token);
                _token.expires_in = result.expires_in;

                //TDSettings.access_token = _token.access_token;
                //TDSettings.expires_in = DateTime.Now.AddMinutes(30);
                //token.refresh_token = Helper.Encrypt(token.refresh_token);
            }

            //token.userName = loginDetails.userName;

        }
    }
}
