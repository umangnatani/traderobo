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
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace TradeRobo.Service
{
    public class DBService : BaseService
    {

        public DBService(MyDatabaseContext context) : base(context)
        {

        }





        public void SavePieDetails(PieDetail poco)
        {
            var entity = _context.PieDetail.Single(x => x.Id == poco.Id);
            if (entity != null) {
                _context.Update(poco);
            }
            else
                _context.PieDetail.Add(poco);

            _context.SaveChanges();
        }


        public ReturnType SavePie(Pie poco)
        {
            var entity = _context.PieDetail.Single(x=> x.Id == poco.Id);
            if (entity != null)
                _context.Update(poco);
            else
                _context.Pie.Add(poco);

            _context.SaveChanges();

            return new ReturnType();
        }


        public ReturnType ToggleProxy()
        {
            var entity = _context.AppSettings.Where(x => x.Key == "UseProxy").ToList()[0];

            if (entity.Value == "True")
                entity.Value = "False";
            else
                entity.Value = "True";


            _context.SaveChanges();

            return new ReturnType();
        }






        public List<User> GetAllUsers()
        {

            return _context.User.ToList();

        }


        public List<Menu> GetMenu(int UserId)
        {
            // var roleIds = _context.UserRole.Where(x => x.UserId == UserId).Select(x=> x.RoleId).ToList();

            // var tmp = _context.RoleMenu.Include(x=> x.Menu).Where(x => roleIds.Contains(x.RoleId));

            return _context.Menu.Include(x=> x.Children).Where(x => x.Roles.Any(x=> x.Role.Users.Any(x=> x.UserId == UserId))).OrderBy(x=> x.SortOrder).ToList();

        }



        public ReturnType SaveWatchListSymbols(int Id, string Symbols)
        {
            foreach (var item in Symbols.Split(","))
            {
                var watchlistSymbol = new PieDetail  { PieId = Id, Symbol = item.Trim().ToUpper() };
                _context.PieDetail.Add(watchlistSymbol);
            }

            _context.SaveChanges();

            return new ReturnType();
        }

        public ReturnType SaveUser(User user, int CurUserId)
        {
            var curUser = GetUser(CurUserId);
            curUser.Name = user.Name;
            curUser.Email = user.Email;


                //_context.Update(user);

                _context.SaveChanges();

                return new ReturnType();
        }


        public ReturnType DeletePieDetail(Int32 PieDetailId)
        {

            var pieDetail = _context.PieDetail.Find(PieDetailId);


            _context.PieDetail.Remove(pieDetail);

            _context.SaveChanges();

            return new ReturnType();
        }


        public ReturnType SaveNewUser(User user, int CurUserId)
        {
            var curUser = GetUser(CurUserId);
            if (CurUserId == 1)
            {
                user.Password = Helper.Encrypt(user.Password);
                _context.Set<User>().Add(user);

                _context.SaveChanges();

                return new ReturnType();
            }
            else
                return new ReturnType { Message = "You do not have permission", Success = false };

               
        }

        public async Task<ReturnType> ChangePassword(PasswordRequest poco, int CurUserId)
        {
            var user = GetUser(CurUserId);

            var curPassword = Helper.Decrypt(user.Password);

            var rt = new ReturnType();


            if (poco.OldPassword == curPassword)
            {
                user.Password = Helper.Encrypt(poco.NewPassword);
                rt = await Save(user);
                rt.Message = "Passwrod changed succesfully.";

            }
            else
                rt = new ReturnType  { Message = "The old password is not correct.", Success = false };

            return rt;
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

        public List<Role> GetRoles(int UserId)
        {
            return _context.Role.Where(x => x.Users.Select(x => x.UserId).Contains(UserId)).ToList();
            //return _context.Role.Where(x => x.Users.Select(x=> x.UserId).Contains(UserId) && x.Users.Any(y=> y.EndDate == null || y.EndDate > DateTime.Now)).ToList();
        }

        private Claim[] getClaims(User user)
        {
            List<Claim> claims = new List<Claim>();
            var roles = GetRoles(user.Id);
            claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item.Code));
            }
            return claims.ToArray();
        }


        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Helper.Key);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {


                Subject = new ClaimsIdentity(getClaims(user)),
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

            List<PieDetail> list = new List<PieDetail>();

            //if (PieId > 0)
            //{
                list = _context.Set<PieDetail>().AsNoTracking().Where(x => x.PieId == PieId).OrderBy(x => x.Symbol).ToList();
            //}
            //else
            //{
            //    list = _context.Set<PieDetail>().AsNoTracking().ToList();
            //}

            return list;
        }

        public List<Pie> GetPies(int UserId)
        {
            return _context.Pie.Where(x=> x.UserId == UserId).OrderBy(x=> x.SortOrder).ToList();
        }


    }
}
