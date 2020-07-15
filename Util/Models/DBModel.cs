using System;
using System.Collections.Generic;
using System.Text;

namespace TradeRobo.Service
{

    public class FavStocks
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string RHToken { get; set; }
        public string RHNumber { get; set; }
        public string TDNumber { get; set; }
    }


    public class Pie
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public ICollection<PieDetail> PieDetails { get; set; } = new List<PieDetail>();

    }

    public class PieDetail
    {
        public int Id { get; set; }
        public int PieId { get; set; }
        public string Symbol { get; set; }
        public double Weight { get; set; }
        public Pie Pie { get; set; }
        public bool Enabled { get; set; } = true;
    }

    public class AppSettings
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
