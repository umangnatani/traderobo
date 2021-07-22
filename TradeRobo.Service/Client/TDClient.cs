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
                payload.Add("symbol", symbol);

                var json = client.Get(EndPoint.Quotes, payload);

                list = JsonConvert.DeserializeObject<Dictionary<string, GlobalQuote>>(json);
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

            try
            {

                client.SetHeaders(_token.access_token);

                //var response = client.Get(EndPoint.Accounts);

                //var json = (JObject)JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(response)[0]["securitiesAccount"];

                //_token.userName = json.GetValue("accountId").ToString();
            }
            catch (UnauthorizedAccessException e)
            {
                Authenticate();
                GetAccount();
            }


            //token.userName = JsonConvert.DeserializeObject<Dictionary<string, object>>(json)["accountId"].ToString();

        }



        public void PlaceOrder(Order order)
        {
            order.OrderGroup.InitTDOrder();

            GetAccount();
            var APIEndPoint = EndPoint.Orders.Replace("{accountId}", order.TDAccountId);


            //order.Quote = get_quotes(order.Symbol);

            Dictionary<string, object> payload = new Dictionary<string, object>();



            payload.Add("session", "NORMAL");
            payload.Add("orderStrategyType", order.OrderGroup.TDOrderStrategyType);
            payload.Add("duration", order.OrderGroup.TimeInForce);
            payload.Add("orderType", order.OrderGroup.Type);

            if (order.OrderGroup.Type.ToLower() == "limit")
                payload.Add("price", order.Price.RoundDecimal());


            var orderLeg = new Dictionary<string, object>();

            var orderLegCollection = new List<Dictionary<string, object>>();

            orderLeg.Add("instruction", order.OrderGroup.Side);

            orderLeg.Add("quantity", order.Quantity);

            var instrument = new Dictionary<string, object>();

            instrument.Add("symbol", order.Symbol);
            instrument.Add("assetType", "EQUITY");

            orderLeg.Add("instrument", instrument);

            orderLegCollection.Add(orderLeg);

            payload.Add("orderLegCollection", orderLegCollection);

            if (order.Quantity > 0)
            {


                try
                {
                    var returnValue = client.Post(APIEndPoint, payload);

                    var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(returnValue);

                    order.Success = true;


                }
                catch (UnauthorizedAccessException e)
                {
                    Authenticate();
                    PlaceOrder(order);
                }
                catch (Exception e)
                {
                    order.ExecMessage = $"Order failed for {order.Symbol} with error: {e.Message}";
                }
            }
            else
                order.ExecMessage = $"Order failed for {order.Symbol} with 0 quantity";



        }




        public void Authenticate(String Code = "")
        {

            var payload = new List<KeyValuePair<string, string>>();
            //payload.Add("client_id", Settings.ClientId + "@AMER.OAUTHAP");

            if (!string.IsNullOrWhiteSpace(Code))
            {
                payload.Add(new KeyValuePair<string, string>("client_id", Settings.TDClientId + "@AMER.OAUTHAP"));
                payload.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                payload.Add(new KeyValuePair<string, string>("refresh_token", ""));
                payload.Add(new KeyValuePair<string, string>("access_type", "offline"));
                payload.Add(new KeyValuePair<string, string>("code", Code));
                payload.Add(new KeyValuePair<string, string>("redirect_uri", Settings.TDRedirectURI));
            }

            else
            {

                payload.Add(new KeyValuePair<string, string>("client_id", Settings.TDClientId));
                payload.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
                payload.Add(new KeyValuePair<string, string>("refresh_token", _token.refresh_token));
                payload.Add(new KeyValuePair<string, string>("access_type", ""));
                payload.Add(new KeyValuePair<string, string>("code", ""));
                payload.Add(new KeyValuePair<string, string>("redirect_uri", ""));
            }
            //payload.Add("access_type", "offline");
            //payload.Add("redirect_uri", Settings.TDRedirectURI);
            //payload.Add("code", code);


            var response = client.PostAsForm(EndPoint.Login, payload);

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

            if (!string.IsNullOrEmpty(result.refresh_token))
            {
                _token.refresh_token = result.refresh_token;
            }

            //token.userName = loginDetails.userName;

        }
    }
}
