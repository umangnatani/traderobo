using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradeRobo.Service
{
    public class TradeService : BaseService
    {

        TDClient tdClient;
        TDToken _token;

        public TradeService(MyDatabaseContext context) : base(context)
        {
            _token = new TDToken();
            _token.refresh_token = GetAppSetting(SettingsKey.TDRefreshToken);
            _token.access_token  = GetAppSetting(SettingsKey.TDToken);

            Settings.TDClientId = string.IsNullOrWhiteSpace(Settings.TDClientId)? GetAppSetting(SettingsKey.TDClientId): SettingsKey.TDClientId;

            tdClient = new TDClient(_token);
        }

        public void PlaceOrder(TDOrder specialOrder)
        {

            for (int i = 0; i < specialOrder.Total ; i++)
            {
                var order = new TDOrder { Symbol = specialOrder.Symbol, Quantity = specialOrder.Quantity, Side = specialOrder.Side };

                order.Price = order.Side == "buy" ? specialOrder.Price - (i * specialOrder.Increment) : specialOrder.Price + (i * specialOrder.Increment);

                tdClient.PlaceOrder(order);
                
                UpdateToken();


            }
        }

        private void UpdateToken()
        {
            if (_token.isNewToken)
            {
                var entity = _context.AppSettings.Where(x => x.Key == SettingsKey.TDToken).SingleOrDefault();
                entity.Value = _token.access_token;

                try { 
                    _context.SaveChanges();
                    _token.isNewToken = false;
                }
                catch
                {

                }

            }
        }

     


    }
}
