using System;
using System.Collections.Generic;
using System.Text;

namespace TradeRobo.Service
{
    public class TDOrder: Order
    {

        public string OrderStrategyType { get; set; }
        public TDOrder(): base()
        {
            TimeInForce = "DAY";
            OrderStrategyType = "SINGLE";
        }
    }
}
