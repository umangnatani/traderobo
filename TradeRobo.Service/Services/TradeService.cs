using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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

        private AllyClient allyClient;

        private AlpacaClient alpacaClient;

        public User User { get; set; }
        public List<UserConfig> UserConfig { get; set; }


        public TradeService(MyDatabaseContext context, int UserId) : base(context)
        {
            this.User = GetUser(UserId);
            this.UserConfig = GetUserConfig(UserId);
            InitRH();
            InitTD();
            InitAlpaca();

            //allyClient = new AllyClient();
        }


        private void InitRH()
        {
            _rhToken = new RHToken();

            _rhToken.accessToken = GetUserConfig(this.UserConfig, SettingsKey.RHToken);
            _rhToken.deviceToken = GetUserConfig(this.UserConfig, SettingsKey.RHDeviceToken);

            rhClient = new RHClient(_rhToken);
        }

        private void InitAlpaca()
        {

            Settings.AlpacaKey  = GetAppSetting(SettingsKey.AlpacaKey);
            Settings.AlpacaSecret = GetAppSetting(SettingsKey.AlpacaSecret);
         
            alpacaClient = new AlpacaClient();
        }






        public RHAuthResponse RHLogin(Credentials credentials)
        {
            var response = rhClient.Authenticate(credentials);

            if (response.MFARequired)
            {
                SaveUserConfig(UserConfig, new UserConfig { UserId = User.Id, Key = SettingsKey.RHDeviceToken, Value = _rhToken.deviceToken });
            }
            else if (!string.IsNullOrWhiteSpace(_rhToken.accessToken))
            {
                SaveUserConfig(UserConfig, new UserConfig { UserId = User.Id, Key = SettingsKey.RHToken, Value = _rhToken.accessToken });
            }

            return response;
        }


        public void TDLogin(string Code)
        {
            tdClient.Authenticate(Code);
            SaveUserConfig(UserConfig, new UserConfig { Key = SettingsKey.TDToken, Value = _tdToken.access_token, UserId = User.Id });
            SaveUserConfig(UserConfig, new UserConfig { Key = SettingsKey.TDRefreshToken, Value = _tdToken.refresh_token, UserId = User.Id });

        }

        public List<RHPosition> GePositions()
        {
            return rhClient.GePositions();
        }

        public string GetAccountProfile()
        {
            return rhClient.GetAccountProfile();
        }


        public ReturnType PlaceMultiOrder(MultiOrder multiOrder, string TDAccountId = "")
        {
            var rt = new ReturnType();

            var orderGroup = new OrderGroup { UserId = User.Id, Broker = multiOrder.Broker, Side = multiOrder.Side, Strategy = Strategy.Multi, Type = "market" };

            foreach (var item in multiOrder.Symbols)
            {
                Order order = new Order { Symbol = item.Symbol, OrderGroup = orderGroup };

                if (multiOrder.Broker == "robinhood" || multiOrder.Broker == "alpaca")
                {
                    order.Amount = multiOrder.Amount;
                }
                else if (multiOrder.Broker == "td")
                {
                    order.TDAccountId = TDAccountId;
                    order.Quantity = CalcQuantity(multiOrder.Amount, item.last_trade_price);
                }


                orderGroup.Orders.Add(order);

                PlaceClientOrder(order);

            }

            //SaveBatch(orderGroup);

            rt.Object = orderGroup;

            return rt;


        }

        private decimal CalcQuantity(decimal Amount, decimal Price)
        {
            var qty = Amount / Price;

            if (qty < 1 && qty > Convert.ToDecimal(0.7))
                return 1;
            else return Math.Round(qty);
        }



        public ReturnType PlaceAllOrder(List<RHPosition> specialOrder, string broker = "RH", string side = "sell")
        {
            var rt = new ReturnType();

            var orderGroup = new OrderGroup { UserId = User.Id, Broker = broker, Side = side, Strategy = Strategy.Buyback, Type = "market" };

            foreach (var item in specialOrder)
            {
                var order = new Order { Symbol = item.symbol, Quantity = Convert.ToDecimal(item.quantity), OrderGroup = orderGroup };

                orderGroup.Orders.Add(order);

                PlaceClientOrder(order);

            }

            //SaveBatch(orderGroup);

            rt.Object = orderGroup;

            return rt;


        }


        public ReturnType PlaceOrder(List<Order> orders)
        {
            var rt = new ReturnType();

            foreach (var item in orders)
            {
                var order = _context.Order.Include(x => x.OrderGroup).Single(x => x.Id == item.Id);

                order.OrderGroup.Side = "buy";
                order.OrderGroup.Type = "market";
                order.Amount = 0;

                PlaceClientOrder(order);

                if (order.Success)
                    order.CoverTimeStamp = DateTime.Now;

                order.Success = true;

                //_context.Update(order);
            }

            //_context.SaveChanges();

            rt.Object = orders;

            return rt;


        }


        public ReturnType PlaceOrder(OrderGroup orderGroup)
        {
            var rt = new ReturnType();

            orderGroup.UserId = User.Id;

            for (int i = 0; i < orderGroup.Total; i++)
            {
                var order = new Order { Symbol = orderGroup.Symbol, Quantity = orderGroup.Quantity, OrderGroup = orderGroup };

                orderGroup.Orders.Add(order);

                order.Price = orderGroup.Side == "buy" ? orderGroup.Price - (i * orderGroup.Increment) : orderGroup.Price + (i * orderGroup.Increment);

                order.TDAccountId = orderGroup.TDAccountId;

                PlaceClientOrder(order);

            }

            //SaveBatch(orderGroup);

            rt.Object = orderGroup;

            return rt;


        }

        public Account GetAccount()
        {
            return alpacaClient.GetAccount();
        }

            private void PlaceClientOrder(Order order)
        {
            if (order.OrderGroup.Broker == "robinhood")
            {
                rhClient.PlaceOrder(order);
            }
            else if (order.OrderGroup.Broker == "td")
            {
                tdClient.PlaceOrder(order);
                UpdateToken();
            }
            else if (order.OrderGroup.Broker == "ally")
            {
                allyClient.PlaceOrder(order);
            }
            else if (order.OrderGroup.Broker == "alpaca")
            {
                alpacaClient.PlaceOrder(order);
            }
        }

        private void CalcPriceWeight(List<PieDetail> list, string side)
        {
            list.ForEach(x =>
            {
                if (!x.GlobalQuote.isRed && side == "buy")
                    x.Weight = x.Weight / x.PriceWeight;
                else if (!x.GlobalQuote.isRed && side == "sell")
                    x.Weight = x.Weight * x.PriceWeight;
                else if (x.GlobalQuote.isRed && side == "buy")
                    x.Weight = x.Weight * x.PriceWeight;
                else if (x.GlobalQuote.isRed && side == "sell")
                    x.Weight = x.Weight / x.PriceWeight;
            });

        }



        public ReturnType RunSchedule()
        {
            var rt = new ReturnType();

            var schedules = _context.Schedule.Where(x => x.Pie.UserId == User.Id && x.Enabled).ToList();

            IndexQuote nasdaq = new IndexQuote();
            IndexQuote spy = new IndexQuote();

            try
            {
                MarketClient marketClient = new MarketClient();
                var indexes = marketClient.GetIndexes();
                nasdaq = indexes.Where(x => x.symbol == "^IXIC").FirstOrDefault();
                spy = indexes.Where(x => x.symbol == "^GSPC").FirstOrDefault();

            }
            catch
            {

            }



            foreach (var schedule in schedules)
            {
                decimal marketWeightRatio = 1;

                if (schedule.MarketWeight)
                {
                    if (nasdaq != null && nasdaq.changesPercentage < 0)
                        marketWeightRatio = Math.Abs(nasdaq.changesPercentage) + 1;
                }

                var pieOrder = new PieOrder { Amount = schedule.Amount * marketWeightRatio, PieId = schedule.PieId, Side = "buy" };
                rt = PlaceOrder(pieOrder);
            }

            return rt;
        }



        public ReturnType PlaceOrder(PieOrder pieOrder)
        {
            var rt = new ReturnType();


            var StockList = GetPieDetailsWithQuote(pieOrder.PieId, pieOrder.PriceWeighted);

            var retValue = new List<Order>();

            if (pieOrder.PriceWeighted)
                CalcPriceWeight(StockList, pieOrder.Side);

            var TotalWeight = StockList.Sum(x => x.Weight);

            var orderGroup = new OrderGroup { UserId = User.Id, Broker = pieOrder.Broker, Side = pieOrder.Side, Strategy = Strategy.Pie, PieId = pieOrder.PieId, Amount = pieOrder.Amount };


            foreach (var stock in StockList)
            {
                var order = new Order { Symbol = stock.Symbol, Amount = Math.Round(stock.Weight * pieOrder.Amount / TotalWeight, 1), OrderGroup = orderGroup };

                orderGroup.Orders.Add(order);

                if (order.Amount > 0)
                    PlaceClientOrder(order);

            }

            //SaveBatch(orderGroup);

            rt.Object = orderGroup;

            return rt;


        }




        // TD Related Methods
        private void InitTD()
        {
            _tdToken = new TDToken();


            _tdToken.refresh_token = GetUserConfig(this.UserConfig, SettingsKey.TDRefreshToken);
            _tdToken.access_token = GetUserConfig(this.UserConfig, SettingsKey.TDToken);


            //_tdToken.refresh_token = GetAppSetting(SettingsKey.TDRefreshToken);
            // _tdToken.access_token = GetAppSetting(SettingsKey.TDToken);
            Settings.TDClientId = GetAppSetting(SettingsKey.TDClientId);
            //Settings.TDClientId = GetUserConfig(this.UserConfig, SettingsKey.TDClientId);

            tdClient = new TDClient(_tdToken);
        }

        private decimal CalcPriceWeight(decimal PctChange)
        {
            return Math.Abs(PctChange) * 2 / 10 + 1;
        }



        public void FillPiesWithQuotes(List<PieDetail> list)
        {
            var symbols = string.Join(",", list.Select(x => x.Symbol).ToList());

            var results = tdClient.GetCMLQuote(symbols);

            // var results = tdClient.GetQuote(symbols);

            // var MAs = tdClient.GetMA(symbols);

            list.ForEach(x =>
            {
                x.GlobalQuote = results.SingleOrDefault(m => m.symbol == x.Symbol);
                // x.GlobalQuote.MA = MAs.SingleOrDefault(m => m.Ticker == x.Symbol);
                x.PriceWeight = CalcPriceWeight(x.GlobalQuote.percentChange);
            });

            //var rhQuotes = rhClient.GetQuote(symbols);

            //list.ForEach(x =>
            //{
            //    x.Quote = rhQuotes.Where(r => r.Symbol == x.Symbol).FirstOrDefault();
            //    x.PriceWeight = CalcPriceWeight(x.Quote.pct_change);
            //});
        }



        private void UpdateToken()
        {
            if (_tdToken.isNewToken)
            {
                SaveUserConfig(UserConfig, new UserConfig { Key = SettingsKey.TDToken, Value = _tdToken.access_token, UserId = User.Id });
                _tdToken.isNewToken = false;

            }
        }


        public ReturnType RefreshMA()
        {



            var pieList = _context.Set<Pie>().Where(x => x.UserId == User.Id).ToList();


            foreach (var pie in pieList)
            {
                var list = _context.Set<PieDetail>().Where(x => x.PieId == pie.Id).ToList();

                // var list = _context.Set<PieDetail>().Where(x => x.PieId == 9).ToList();


                var symbols = string.Join(",", list.Select(x => x.Symbol).ToList());


                var MAs = tdClient.GetMA(symbols);

                if (MAs.Count > 0)
                {
                    list.ForEach(x =>
                {

                    var MA = MAs.FirstOrDefault(m => m.Ticker == x.Symbol);
                    if (MA != null)
                    {
                        x.ma5 = MA.ma5;
                        x.ma8 = MA.ma8;
                        x.ma10 = MA.ma10;
                        x.ma13 = MA.ma13;
                        x.ma21 = MA.ma21;
                        x.ma50 = MA.ma50;
                        x.ma200 = MA.ma200;
                    }
                });


                    _context.SaveChanges();
                }
            }





            return new ReturnType();
        }



        public List<PieDetail> GetPieDetailsWithQuote(Int32 PieId, bool GetQuote)
        {

            List<PieDetail> list = new List<PieDetail>();

            if (PieId > 0)
            {
                list = _context.Set<PieDetail>().AsNoTracking().Where(x => x.PieId == PieId).OrderBy(x => x.Symbol).ToList();
            }
            else
            {
                list = _context.Set<PieDetail>().AsNoTracking().Where(x => x.Pie.UserId == User.Id).OrderBy(x => x.Symbol).ToList();
            }

            if (GetQuote)
            {
                try
                {
                    FillPiesWithQuotes(list);
                    list = list.AsEnumerable().OrderBy(x => x.GlobalQuote.percentChange).ToList();
                }
                catch
                {
                }


            }
            return list;
        }




    }
}
