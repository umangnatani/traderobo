using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradeRobo.Service
{
    public class TradeService : BaseService
    {

        private TDClient tdClient;
        private TDToken _tdToken;

        private RHClient rhClient;
        private RHToken _rhToken;

        public User User { get; set; }


        public TradeService(MyDatabaseContext context) : base(context)
        {
            InitTD();
        }

        public TradeService(MyDatabaseContext context, int UserId) : base(context)
        {
            this.User = GetUser(UserId);
            InitRH();
        }

        public TradeService(MyDatabaseContext context, int UserId, bool UseBoth) : base(context)
        {
            this.User = GetUser(UserId);
            InitRH();

            InitTD();
        }


        private void InitRH()
        {
            _rhToken = new RHToken();
            //_rhToken. = GetAppSetting(SettingsKey.TDRefreshToken);

            _rhToken.accessToken = User.RHToken;
            _rhToken.deviceToken = User.RHNumber;

            rhClient = new RHClient(_rhToken);
        }

    


      

        public RHAuthResponse RHLogin(Credentials credentials)
        {
            var response = rhClient.Authenticate(credentials);

            if (response.MFARequired)
            {
                User.RHNumber = _rhToken.deviceToken;
                _context.SaveChanges();
            }
            else
            {
                User.RHToken  = _rhToken.accessToken;
                _context.SaveChanges();
            }

            

            return response;
        }

        public string GetAccountProfile()
        {
            return rhClient.GetAccountProfile();
        }

        public ReturnType PlaceOrder(Order specialOrder)
        {
            var rt = new ReturnType();

            var list = new List<Order>();

            for (int i = 0; i < specialOrder.Total; i++)
            {
                var order = new Order { Symbol = specialOrder.Symbol, Quantity = specialOrder.Quantity, Side = specialOrder.Side };

                order.Price = order.Side == "buy" ? specialOrder.Price - (i * specialOrder.Increment) : specialOrder.Price + (i * specialOrder.Increment);

                rhClient.PlaceOrder(order);

                list.Add(order);

            }

            rt.Object = list;

            return rt;


        }

        private void CalcPriceWeight(List<PieDetail> list, string side)
        {
            list.ForEach(x =>
            {
                if (x.Quote.pct_change > 0 && side == "buy")
                    x.Weight = x.Weight / x.PriceWeight;
                else if (x.Quote.pct_change > 0 && side == "sell")
                    x.Weight = x.Weight * x.PriceWeight;
                else if (x.Quote.pct_change < 0 && side == "buy")
                    x.Weight = x.Weight * x.PriceWeight;
                else if (x.Quote.pct_change < 0 && side == "sell")
                    x.Weight = x.Weight / x.PriceWeight;
            });

        }


        public ReturnType PlaceOrder(PieOrder pieOrder)
        {
            var rt = new ReturnType();

            var list = new List<Order>();

            var StockList = GetPieDetailsWithQuote(pieOrder.PieId, pieOrder.PriceWeighted);

            var retValue = new List<Order>();

            var AccountUrl = rhClient.GetAccountProfile();

            if (pieOrder.PriceWeighted)
                CalcPriceWeight(StockList, pieOrder.Side);

            var TotalWeight = StockList.Sum(x => x.Weight);


            foreach (var stock in StockList)
            {
                var order = new Order { Symbol = stock.Symbol, Amount = Math.Round(stock.Weight * pieOrder.Amount / TotalWeight, 1), Side= pieOrder.Side };

                //ticker.Amount = ticker.Weight * amount / 100;

                if(order.Amount > 0)
                    rhClient.PlaceOrder(order, AccountUrl);

                list.Add(order);
            }

            rt.Object = list;

            return rt;


        }




        // TD Related Methods
        private void InitTD()
        {
            _tdToken = new TDToken();
            _tdToken.refresh_token = GetAppSetting(SettingsKey.TDRefreshToken);
            _tdToken.access_token = GetAppSetting(SettingsKey.TDToken);

            Settings.TDClientId = GetAppSetting(SettingsKey.TDClientId);

            tdClient = new TDClient(_tdToken);
        }

        private double CalcPriceWeight(double PctChange)
        {
          return Math.Abs(PctChange) *2/10 + 1;
        }

        public void FillPiesWithQuotes(List<PieDetail> list)
        {
            var symbols = string.Join(",", list.Select(x => x.Symbol).ToList());

            //var results = tdClient.GetQuote(symbols);
            //list.ForEach(x => {
            //    x.GlobalQuote = results[x.Symbol];
            //    x.PriceWeight = CalcPriceWeight(x.GlobalQuote.netPercentChangeInDouble);
            //    });

            var rhQuotes = rhClient.GetQuote(symbols);

            list.ForEach(x => {
                x.Quote = rhQuotes.Where(r => r.Symbol == x.Symbol).FirstOrDefault();
                x.PriceWeight = CalcPriceWeight(x.Quote.pct_change);
            });
        }

        public ReturnType PlaceTDOrder(TDOrder specialOrder, int Id)
        {
            var rt = new ReturnType();

            if (Id == 1) { 

            var list = new List<TDOrder>();


            for (int i = 0; i < specialOrder.Total ; i++)
            {
                var order = new TDOrder { Symbol = specialOrder.Symbol, Quantity = specialOrder.Quantity, Side = specialOrder.Side };

                order.Price = order.Side == "buy" ? specialOrder.Price - (i * specialOrder.Increment) : specialOrder.Price + (i * specialOrder.Increment);

                tdClient.PlaceOrder(order);

                list.Add(order);

                UpdateToken();


            }

            rt.Object = list;
            }
            else
            {
                rt.Message = "You do not have permission for this operation.";
                rt.Success = false;
            }

            return rt;
        }

        private void UpdateToken()
        {
            if (_tdToken.isNewToken)
            {
                var entity = _context.AppSettings.Where(x => x.Key == SettingsKey.TDToken).SingleOrDefault();
                entity.Value = _tdToken.access_token;

                try { 
                    _context.SaveChanges();
                    _tdToken.isNewToken = false;
                }
                catch
                {

                }

            }
        }



        public List<PieDetail> GetPieDetailsWithQuote(Int32 PieId, bool GetQuote)
        {

            DBService service = new DBService(_context);

            var list = service.GetPieDetails(PieId);

            if (GetQuote)
            {
                try
                {
                    FillPiesWithQuotes(list);
                    list = list.AsEnumerable().OrderBy(x => x.Quote.pct_change).ToList();
                }
                catch
                {
                }

                
            }
            return list;
        }




    }
}
