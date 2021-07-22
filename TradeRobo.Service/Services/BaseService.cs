using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.Globalization;
using System.Threading.Tasks;

namespace TradeRobo.Service
{
    public class BaseService
    {
        protected MyDatabaseContext _context;
        protected GenericRepository<IEntity> _repository;

        public BaseService(MyDatabaseContext context)
        {
            _context = context;
            SetProxy();
            _repository = new GenericRepository<IEntity>(_context);
        }


        public async Task<ReturnType> Save(IEntity poco)
        {
            await _repository.Update(poco);
            return new ReturnType();
        }


        public async Task<IEntity> GetById(int Id)
        {
            return await _repository.GetById(Id);
        }


        public List<Order> GetBuyBackPositions()
        {
            return _context.Order.Include(x=> x.OrderGroup).Where(x => x.OrderGroup.Strategy == Strategy.Buyback && x.CoverTimeStamp == null && x.Success).ToList();
        }

        private void SetProxy()
        {
            Settings.UseProxy = Convert.ToBoolean(GetAppSetting(SettingsKey.UseProxy));
            Settings.Proxy = GetAppSetting(SettingsKey.Proxy);
        }

        public string GetAppSetting(string key)
        {
            return _context.AppSettings.Where(x => x.Key == key).Select(x => x.Value).FirstOrDefault();
        }


        public List<UserConfig> GetUserConfig(int UserId)
        {
            return _context.UserConfig.Where(x => x.UserId == UserId).ToList() ;
        }

        public List<TDAccount> GetAccounts(int UserId)
        {
            return _context.TDAccount.Where(x => x.UserId == UserId).ToList();
        }

        public string GetUserConfig(List<UserConfig> userConfig, string key)
        {
            return userConfig.Where(x => x.Key == key).Select(x=> x.Value).FirstOrDefault();
        }

        public void SaveUserConfig(List<UserConfig> list, UserConfig poco)
        {
            var userConfig = list.Where(x => x.Key == poco.Key).FirstOrDefault();

            if (userConfig == null)
            {
                _context.UserConfig.Add(poco);
            }
            else
            {
                userConfig.Value = poco.Value;
                _context.UserConfig.Update(userConfig);
            }

            _context.SaveChanges();
        }

        //public async Task<User> GetUserAsync(int Id, bool getPassword = true)
        //{
        //    var user = await GetById(Id);

        //    if (!getPassword)
        //        user.Password = null;

        //    return user;
        //}

        public User GetUser(int Id, bool getPassword = true)
        {
            var user = _context.User.Find(Id);

            if (!getPassword)
                user.Password = null;

            return user;
        }

        public void SaveBatch(OrderGroup orderGroup)
        {
            orderGroup.EndTime = DateTime.Now;

            _context.OrderGroup.Add(orderGroup);
            _context.SaveChanges();
        }


    }
}
