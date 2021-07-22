using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TradeRobo.Service
{
    


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
