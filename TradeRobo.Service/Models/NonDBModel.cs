using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TradeRobo.Service
{
    public class TDOrder : Order
    {

        public string OrderStrategyType { get; set; }
        public TDOrder() : base()
        {
            TimeInForce = "DAY";
            OrderStrategyType = "SINGLE";
        }
    }


    public partial class PieDetail
    {
        [NotMapped]
        public GlobalQuote GlobalQuote { get; set; }

        [NotMapped]
        public Quote Quote { get; set; }

        [NotMapped]
        public double PriceWeight { get; set; }
    }
}
