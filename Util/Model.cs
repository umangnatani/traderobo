using System;
using System.Collections.Generic;
using System.Text;

namespace TradeRobo.Service
{


    public class Order
    {
        public string Folio { get; set; }
        public string Symbol { get; set; }
        public string Side { get; set; }
        public string Type { get; set; }
        public string TimeInForce { get; set; }

        public bool ExtendedHours { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }

        public Quote Quote { get; set; }

        public int ResultCode { get; set; }
        public string Result { get; set; }

        public Order()
        {
            Side = "buy";
            Type = "limit";
            TimeInForce = "gfd";
            ExtendedHours = false;
            Quantity = 10;
            ResultCode = 1;
        }
    }


    public class OrderEntry
    {
        public string Folio { get; set; }
        public string Amount { get; set; }
    }

    public class Credentials
    {
        public string userName { get; set; }
        public string passWord { get; set; }

        public string mfaToken { get; set; }

        public string deviceToken { get; set; }
    }

    public interface IJwtToken
    {
        string accessToken { get; set; }
        string deviceToken { get; set; }
        bool isAuthenticated { get; set; }
        string userName { get; set; }
    }

    public class JwtToken : IJwtToken
    {
        public string accessToken { get; set; }

        public string userName { get; set; }

        public string deviceToken { get; set; }

        public bool isAuthenticated { get; set; }


    }
    public class Quote
    {
        public string Symbol { get; set; }
        public double Weight { get; set; }
        public double Amount { get; set; }
        public double ask_price { get; set; }
        public double bid_price { get; set; }

        public double last_trade_price { get; set; }

        public double price { get; set; }

        public double quantity { get; set; }



        public string instrument { get; set; }
        public int ask_size { get; set; }
        public int bid_size { get; set; }


    }

    public class Stock
    {
        public string Ticker { get; set; }
        public double Weight { get; set; }
    }



   

}
