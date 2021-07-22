using System.ComponentModel.DataAnnotations.Schema;

namespace TradeRobo.Service
{

    public partial class OrderGroup
    {
        [NotMapped]
        public string Type { get; set; } =  "limit";

        [NotMapped]
        public string Symbol { get; set; } 

        [NotMapped]
        public string TimeInForce { get; set; } = "gfd";

        [NotMapped]
        public bool ExtendedHours { get; set; } = false;

        [NotMapped]
        public string TDOrderStrategyType { get; set; } = "SINGLE";

        [NotMapped]
        public string TDAccountId { get; set; } 

        [NotMapped]
        public decimal Increment { get; set; }

        [NotMapped]
        public int Total { get; set; }

        [NotMapped]
        public decimal Quantity { get; set; } = 10;

        [NotMapped]
        public decimal Price { get; set; }

        public void InitTDOrder()
        {
            TimeInForce = "DAY";
        }
    }

      

   
}