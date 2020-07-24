using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradeRobo.Service
{
    public class AVClient
    {

        RestClient client;
        public AVClient()
        {
            client = new RestClient();

        }
        //public GlobalQuote GetQuote(string Symbol){

        //    var url = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={Symbol}&apikey={Settings.AVKey}";
        //    var json = client.Get(url);

        //    JObject rss = JObject.Parse(json);

        //    var gq = new GlobalQuote
        //    {
        //        Symbol = Symbol,
        //        Open = Convert.Todecimal(rss["Global Quote"]["1. symbol"]),
        //        // ChangePercent = Convert.Todecimal(rss["Global Quote"]["10. change percent"]),
        //    };

        //    return gq;

        //}

    }
}
