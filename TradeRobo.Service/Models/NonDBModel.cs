using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TradeRobo.Service
{




    public class Position
    {
        public string symbol { get; set; }
        public decimal qty { get; set; }
        public decimal avg_entry_price { get; set; }
        public decimal market_value { get; set; }
        public decimal current_price { get; set; }

        public decimal lastday_price { get; set; }
        public decimal change_today { get; set; }

        public decimal pct_change {
            get
            {
                return (current_price - lastday_price) * 100/ lastday_price;
            }
        }

        public bool isRed
        {
            get
            {
                return pct_change < 0 ? true : false;
            }
        }
        
    }


    public class Account
    {
        public string account_number { get; set; }
        public decimal cash { get; set; }
        public decimal portfolio_value { get; set; }
        public decimal buying_power { get; set; }

        public List<Position> positions { get; set; }
    }


    public partial class PieDetail
    {
        [NotMapped]
        public GlobalQuote GlobalQuote { get; set; }

        [NotMapped]
        public Quote Quote { get; set; }

        [NotMapped]
        public decimal PriceWeight { get; set; }
    }
}
