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
    }


    public static class EndPoint
    {
        public const string Login = "https://api.tdameritrade.com/v1/oauth2/token";

        public const string Quotes = "https://api.tdameritrade.com/v1/marketdata/quotes";

        public const string Orders = "https://api.tdameritrade.com/v1/accounts/{accountId}/orders";

        public const string Portfolio = "https://api.robinhood.com/portfolios/";

        public const string Accounts = "https://api.tdameritrade.com/v1/accounts";

        public const string AllyOrders = "https://devapi.invest.ally.com/v1/accounts/{accountId}/orders.json";
    }


   


}
