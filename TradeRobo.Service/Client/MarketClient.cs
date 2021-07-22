using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradeRobo.Service
{
    public class MarketClient
    {

        RestClient client;
        public MarketClient()
        {
            client = new RestClient();

        }
        public List<IndexQuote> GetIndexes()
        {

            var url = $"https://financialmodelingprep.com/api/v3/quotes/index?apikey=65d51415d8a62691d12909ddf1cbdf0f";

            var json = client.Get(url);

            return JsonConvert.DeserializeObject<List<IndexQuote>>(json);

        }

    }
}
