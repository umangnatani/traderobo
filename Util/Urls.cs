using System;
using System.Collections.Generic;
using System.Text;

namespace TradeRobo.Service
{
    public static class Urls
    {
        public const string Login = "https://api.robinhood.com/oauth2/token/";
        public const string Quotes = "https://api.robinhood.com/quotes/";

        public const string Orders = "https://api.robinhood.com/orders/";

        public const string Portfolio = "https://api.robinhood.com/portfolios/";

        public const string Accounts = "https://api.robinhood.com/accounts/";
    }
}
