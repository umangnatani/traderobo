using System;
using System.Collections.Generic;
using System.Text;

namespace TradeRobo.Service
{

    public static class RHEndPoint
    {
        public const string Login = "https://api.robinhood.com/oauth2/token/";
        public const string Quotes = "https://api.robinhood.com/quotes/";

        public const string Orders = "https://api.robinhood.com/orders/";

        public const string Portfolio = "https://api.robinhood.com/portfolios/";

        public const string Accounts = "https://api.robinhood.com/accounts/";

        public const string Positions = "https://api.robinhood.com/positions/?nonzero=true";
    }


    public static class AlpacaEndPoint
    {

        //public const string Orders = "https://paper-api.alpaca.markets/v2/orders";
        public const string Orders = "https://api.alpaca.markets/v2/orders";
        public const string Positions = "https://api.alpaca.markets/v2/positions";
        public const string Account = "https://api.alpaca.markets/v2/account";
    }


    public static class SettingsKey
    {
        public const string RHClientId = "RHClientId";
        public const string TDClientId = "TDClientId";
        public const string TDRedirectURI = "TDRedirectURI";
        public const string Key = "Key";
        public const string RHDeviceToken = "RHDeviceToken";
        public const string RHToken = "RHToken";
        public const string TDToken = "TDToken";
        public const string TDRefreshToken = "TDRefreshToken";
        public const string Proxy = "Proxy";
        public const string UseProxy = "UseProxy";
        public const string AlpacaKey = "AlpacaKey";
        public const string AlpacaSecret = "AlpacaSecret";
    }


    public static class EndPoint
    {
        public const string Login = "https://api.tdameritrade.com/v1/oauth2/token";

        public const string Quotes = "https://api.tdameritrade.com/v1/marketdata/quotes";

        public const string CMLQuotes = "https://www.cmlviz.com/get_live_quotes.php?tickers=";

        public const string MAs = "https://www.cmlviz.com/get_live_technicals.php?tickers=";

        public const string Orders = "https://api.tdameritrade.com/v1/accounts/{accountId}/orders";

        public const string Portfolio = "https://api.robinhood.com/portfolios/";

        public const string Accounts = "https://api.tdameritrade.com/v1/accounts";

        public const string AllyOrders = "https://devapi.invest.ally.com/v1/accounts/{accountId}/orders.json";
    }


   


}
