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

namespace TradeRobo.Service
{

    public class QuoteResult
    {
        public List<Quote> results { get; set; }
    }

    public class OrderService
    {
        RestClient client;
        public OrderService(IJwtToken token)
        {
            client = new RestClient();
            client.SetHeaders(token.accessToken);
        }


        public List<Order> PlaceOrder(String pie, double amount)
        {
            var StockList = new List<Stock>();

            var retValue = new List<Order>();

            using (var reader = new StreamReader(Settings.CSVPath + pie))

            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                StockList = csv.GetRecords<Stock>().ToList();
            };

            var AccountUrl = GetAccountProfile();

            //var Tickers = get_quotes(string.Join(",", StockList.Select(x => x.Ticker).ToArray()));

            foreach (var stock in StockList)
            {
                var order = new Order { Symbol = stock.Ticker, Amount = stock.Weight * amount / 100 };

                //ticker.Amount = ticker.Weight * amount / 100;


                PlaceOrder(order, AccountUrl);

                retValue.Add(order);
            }

            return retValue;
        }

        //public List<string> PlaceOrder()
        //{
        //    var retValue = new List<string>();
        //    GetAccountProfile();
        //    var result = PlaceOrder("SHOP", 20, "gfd");
        //    retValue.Add(result);
        //    return retValue;
        //}


        public string GetAccountProfile()
        {
            var url = Urls.Accounts;

            var returnValue = client.Get(url);

            var json = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, object>>>>(returnValue)["results"];

            return json[0]["url"].ToString();

        }


        public Quote get_quotes(string inputSymbols)
        {
            var url = Urls.Quotes + "?symbols=" + inputSymbols;

            var returnValue = client.Get(url);

            var json = JsonConvert.DeserializeObject<QuoteResult>(returnValue).results;

            return json[0];

        }


        public void PlaceOrder(Order order, string AccountUrl = "")
        {

            if (string.IsNullOrWhiteSpace(AccountUrl))
                AccountUrl = GetAccountProfile();

            order.Quote = get_quotes(order.Symbol);

            Dictionary<string, object> payload = new Dictionary<string, object>();



            payload.Add("account", AccountUrl);
            payload.Add("instrument", order.Quote.instrument);


            // For Dollar based amount orders
            if (order.Amount > 0)
            {
                order.Price = Helper.Round(order.Quote.last_trade_price);
                order.Quantity = Helper.Round(order.Amount / order.Price);

                order.Type = "market";

                if (order.Price * order.Quantity > order.Amount)
                    order.Amount = Math.Round(order.Price * order.Quantity + .2, 2);

                Dictionary<string, object> child = new Dictionary<string, object>();

                child.Add("amount", order.Amount);

                child.Add("currency_code", "USD");

                payload.Add("dollar_based_amount", child);

            }

            

            payload.Add("symbol", order.Symbol);



            payload.Add("price", order.Price);

            payload.Add("quantity", order.Quantity);

            payload.Add("type", order.Type);
            //payload.Add("stop_price", "");
            payload.Add("time_in_force", order.TimeInForce);
            payload.Add("trigger", "immediate");

            payload.Add("side", order.Side);
            payload.Add("extended_hours", order.ExtendedHours);


    
            try
            {
                var returnValue = client.Post(Urls.Orders, payload);

                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(returnValue);

                if (json.TryGetValue("id", out object value))
                {
                    //Console.WriteLine("Order placed for " + symbol + ", id: " + value.ToString());
                    order.Result = $"Order placed for {order.Symbol}.";

                }
                else if (json.ContainsKey("detail"))
                {
                    //Console.WriteLine("Order failed for " + symbol + " retrying after 45 seconds...");
                    Thread.Sleep(TimeSpan.FromSeconds(60));
                    PlaceOrder(order, AccountUrl);
                }
                else
                {
                    order.Result = $"Order failed for {order.Symbol}.";
                }
            }
            catch (Exception e)
            {
                order.Result = $"Order failed for {order.Symbol} with error: {e.Message}";
            }


        }




    }
}

