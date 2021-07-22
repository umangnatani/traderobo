using System;
using System.Collections.Generic;
using System.Text;

namespace TradeRobo.Service
{


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class RHPosition
    {
        public string url { get; set; }

        public string symbol { get; set; }
        public string instrument { get; set; }
        public string account { get; set; }
        public string account_number { get; set; }
        public string average_buy_price { get; set; }
        public string pending_average_buy_price { get; set; }
        public string quantity { get; set; }
        public string intraday_average_buy_price { get; set; }
        public string intraday_quantity { get; set; }
        public string shares_held_for_buys { get; set; }
        public string shares_held_for_sells { get; set; }
        public string shares_held_for_stock_grants { get; set; }
        public string shares_held_for_options_collateral { get; set; }
        public string shares_held_for_options_events { get; set; }
        public string shares_pending_from_options_events { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }

    }



    public class MultiOrder
    {
        public List<Quote> Symbols { get; set; }
        public string Side { get; set; }
        public decimal Amount { get; set; }
        public bool PriceWeighted { get; set; }
    }


    public class PieOrder
    {
        public int PieId { get; set; }
        public string Side { get; set; }

        public decimal Amount { get; set; }
        public bool PriceWeighted { get; set; }
    }



   


    public class ReturnType
    {
        public int Code { get; set; } = 1;
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "Operation Successful.";

        public Object Object { get; set; }

    }


    public class PasswordRequest
    {
        public int  UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class RHAuthResponse
    {
        public bool MFARequired { get; set; }
        public bool isRHAuthenticated { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class Credentials
    {
        public string userName { get; set; }
        public string passWord { get; set; }

        public string mfaToken { get; set; }
    }

    public interface IJwtToken
    {
        int UserId { get; set; }
        string accessToken { get; set; }
        string deviceToken { get; set; }
    }

    public class RHToken : IJwtToken
    {

        public int UserId { get; set; }

        public string accessToken { get; set; }

        public string deviceToken { get; set; }


    }


    public class TDToken
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public string userName { get; set; }

        public string refresh_token { get; set; }

        public int refresh_token_expires_in { get; set; }

        public bool isNewToken { get; set; }


    }


    public class IndexQuote
    {
        public string symbol { get; set; }

        public string name { get; set; }
        public decimal price { get; set; }
        public decimal changesPercentage { get; set; }
        public decimal change { get; set; }
        public decimal dayLow { get; set; }

    }

    public class GlobalQuote
    {
        public string symbol { get; set; }
        public decimal bidPrice { get; set; }
        public decimal askPrice { get; set; }
        public decimal lastPrice { get; set; }
        public decimal openPrice { get; set; }
        public decimal highPrice { get; set; }
        public decimal lowPrice { get; set; }

        public decimal closePrice { get; set; }

        public decimal netChange { get; set; }
        public decimal netPercentChangeInDouble { get; set; }
        public decimal regularMarketLastPrice { get; set; }


    }


    //public class Quote
    //{
    //    public string Symbol { get; set; }
    //    public decimal Weight { get; set; }
    //    public decimal Amount { get; set; }
    //    public decimal ask_price { get; set; }
    //    public decimal bid_price { get; set; }

    //    public decimal last_trade_price { get; set; }

    //    public decimal previous_close { get; set; }

    //    public decimal price { get; set; }

    //    public decimal quantity { get; set; }

    //    public string instrument { get; set; }
    //    public int ask_size { get; set; }
    //    public int bid_size { get; set; }

    //    public decimal pct_change
    //    {
    //        get
    //        {
    //            return Math.Round((last_trade_price - previous_close) * 100 / previous_close, 4);
    //        }
    //    }



    //}


    public class Quote
    {
        public string Symbol { get; set; }
        public decimal Weight { get; set; }
        public decimal Amount { get; set; }
        public decimal ask_price { get; set; }
        public decimal bid_price { get; set; }

        public decimal last_trade_price { get; set; }

        public decimal previous_close { get; set; }

        public decimal price { get; set; }

        public decimal quantity { get; set; }

        public string instrument { get; set; }
        public int ask_size { get; set; }
        public int bid_size { get; set; }

        public decimal pct_change
        {
            get
            {
                return Math.Round((last_trade_price - previous_close) * 100 / previous_close, 4);
            }
        }



    }

    public class Stock
    {
        public string Ticker { get; set; }
        public decimal Weight { get; set; }
    }





}
