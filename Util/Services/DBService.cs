using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace TradeRobo.Service
{
    public class DBService : BaseService
    {

        public DBService(MyDatabaseContext context) : base(context)
        {

        }


        public void SavePieDetails(PieDetail pieDetail)
        {
            var entity = _context.PieDetail.SingleOrDefault(x => x.Id == pieDetail.Id);
            if (entity != null) { 
                entity.Symbol = pieDetail.Symbol;
                entity.Weight = pieDetail.Weight;
            }
            else
                _context.PieDetail.Add(pieDetail);

            _context.SaveChanges();
        }



        public User GetUser(int Id)
        {

            return _context.User.Find(Id);

        }

        public List<User> GetAllUsers()
        {

            return _context.User.ToList();

        }

        public List<FavStocks> GetAllFavStocks()
        {

            return _context.FavStocks.ToList();

        }

        public void SaveUser(User user)
        {
            user.Password = Helper.Encrypt(user.Password);
            _context.Set<User>().Add(user);
            _context.SaveChanges();
        }


        public AuthenticateResponse Authenticate(User model)
        {
            model.Password = Helper.Encrypt(model.Password);

            var user = _context.User.SingleOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Helper.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        //public void SavePie()
        //{
        //    var Pies = GetPies("");

        //    foreach (var str in Pies)
        //    {
        //        var pie = new Pie{Name = str.Replace(".csv", "") };
        //        _context.Pie.Add(pie);
        //    }

        //    //Pie pie = new Pie();

        //    _context.SaveChanges();
        //}

        public List<PieDetail> GetPieDetails(Int32 PieId)
        {
            return _context.PieDetail.Where(x => x.PieId == PieId).OrderByDescending(x => x.Weight).ThenBy(x => x.Symbol).ToList();
        }

        public List<Pie> GetPies()
        {
            return _context.Pie.ToList();
        }
    }
}
