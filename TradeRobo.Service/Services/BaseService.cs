using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.Globalization;

namespace TradeRobo.Service
{
    public class BaseService
    {
        protected MyDatabaseContext _context;

        public BaseService(MyDatabaseContext context)
        {
            _context = context;
            SetProxy();
        }

        private void SetProxy()
        {
            Settings.UseProxy = Convert.ToBoolean(GetAppSetting(SettingsKey.UseProxy));
            Settings.Proxy = GetAppSetting(SettingsKey.Proxy);
        }

        public string GetAppSetting(string key)
        {
            return _context.AppSettings.Where(x => x.Key == key).Select(x => x.Value).SingleOrDefault();
        }

        public User GetUser(int Id)
        {
            return _context.User.Find(Id);
        }


    }
}
