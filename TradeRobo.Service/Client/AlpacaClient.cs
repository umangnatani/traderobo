using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TradeRobo.Service
{
    public class AlpacaResponse
    {
        public string id { get; set; }
        public string status { get; set; }
    }

    public class AlpacaClient
    {
        RestClient client;
        public AlpacaClient()
        {
            client = new RestClient();
            client.SetHeaders("APCA-API-KEY-ID", Settings.AlpacaKey);
            client.SetHeaders("APCA-API-SECRET-KEY", Settings.AlpacaSecret);
        }


        public Account GetAccount()
        {
            var account = JsonConvert.DeserializeObject<Account>(client.Get(AlpacaEndPoint.Account));

            var returnValue = client.Get(AlpacaEndPoint.Positions);

            var positions = JsonConvert.DeserializeObject<List<Position>>(returnValue) ;

            account.positions = positions.OrderByDescending(x => x.market_value).ToList();

            return account;
        }


        public void PlaceOrder(Order order, string AccountUrl = "")
        {

            order.Success = false;


            Dictionary<string, object> payload = new Dictionary<string, object>();


            payload.Add("symbol", order.Symbol);
            payload.Add("side", order.OrderGroup.Side);
            
            payload.Add("time_in_force", "day");
            //payload.Add("extended_hours", order.OrderGroup.ExtendedHours);



            // For Dollar based amount orders
            if (order.Amount > 0)
            {
                payload.Add("notional", order.Amount);
                payload.Add("type", "market");

            }
            else
            {
                payload.Add("qty", order.Quantity);
                payload.Add("type", order.OrderGroup.Type);

                if (order.OrderGroup.Type == "limit")
                {
                    payload.Add("limit_price", order.Price.RoundDecimal());
                }
            }


           




            try
            {
                var returnValue = client.Post(AlpacaEndPoint.Orders, payload);

                var json = JsonConvert.DeserializeObject<AlpacaResponse>(returnValue);

                if (json.status == "accepted")
                {
                    //Console.WriteLine("Order placed for " + symbol + ", id: " + value.ToString());
                    order.ExecMessage = $"${order.Amount} order placed for {order.Symbol}.";
                    order.Success = true;
                }
                
                else
                {
                    order.ExecMessage = $"Order failed for {order.Symbol}.";
                }
            }
            catch (Exception e)
            {
                order.ExecMessage = $"Order failed for {order.Symbol} with error: {e.Message}";
            }


        }
    }
}
