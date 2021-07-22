using System;
using System.Collections.Generic;
using System.Text;

namespace TradeRobo.Service
{
    public static class Extensions
    {
        public static decimal RoundDecimal(this decimal price)
        {
            
            if (price <= 1)
                return Math.Round(price, 4);
            else
                return Math.Round(price, 2);

        }
    }
}
