using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TradeRobo.Service;

namespace TestConsole
{




    class Program
    {
        static void Main(string[] args)
        {



            //var tickers = await service.get_quotes("BAC");

            //var result = await service.PlaceOrder(tickers[0]);

            //var result = service.PlaceOrder("fav", 100);

            //FolioService service = new FolioService();
            //var list = service.GetFolios();
            //var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJleHAiOjE1OTQzMTQzODcsInRva2VuIjoiajVadmhkd0oyY0JLTUJYN0xTbHdETkMyREtxY1VHIiwidXNlcl9pZCI6IjEyMTMzNWU3LTY3NTMtNGFmOS1iYmZmLTRlMWQ2MmE4OTM4OCIsImRldmljZV9oYXNoIjoiNGYxMjA2M2U0MzY3YTIwN2JjNzE2MzdjZGJhNmJlZTgiLCJzY29wZSI6ImludGVybmFsIiwidXNlcl9vcmlnaW4iOiJVUyIsIm9wdGlvbnMiOnRydWUsImxldmVsMl9hY2Nlc3MiOnRydWV9.jIW7iyryHBQOg3qhvApRPAmn0hgHdyVk_jcVQIaEIcFWi_Yeh8nE5bW1 - IVEgDb_C1jvqrQImNBnlUIxyH - I9dlmwRBWvVLta4pSPNfdFDaUnQ31a4zGKCrpBVPLtDstWKY3fRBU0_YFz8rRCBah53WOfe6plXFUYIpdMGea96Bsd11BTjVc1QbJGMYmEONqaQWYUmjk51snd8Hwr7mPJ5QP6y2 - L2doKF8Vpo6iZHUKg8VmaZSJdsqG7tUlUW8yesuFSYvwqEKRGaqZ0tAnWPtEJfqhbfcyq7IfFG2TJsCu8yqzp9iJ2LDxNdIrYnP4oJT4A - ceBSDVhjJmQ3jV1w";
            //var key = "glbw-1rd9-coro19";
            //var enc = Helper.Encrypt(token, key);
            //Console.WriteLine(enc);
            //Console.WriteLine("New Line");
            //var dec = Helper.Decrypt(enc, key);
            //Console.WriteLine(dec);

            Settings.CSVPath = @"C:\My Stuff\Dev\Trading\TradeRobo\TradeRobo\bin\Release\netcoreapp3.1\publish/Content/";

            //try
            //{
            //    var token = Login();
            //    var service = new RHClient(token);
            //    var result = service.PlaceOrder("growth.csv", 100.0);


            //    Console.WriteLine(result);
            //}

            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            Console.ReadLine();

        }

        public static JwtToken Login()
        {

            var fileName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\.tokens\robinhood.json";

            var token = new JwtToken();

            if (File.Exists(fileName))
            {

                token = JsonConvert.DeserializeObject<JwtToken>(File.ReadAllText(fileName));
            }

            if (!token.isAuthenticated)
            {
                var loginDetaiils = new Credentials();

                Console.WriteLine("Please Enter UserName:");
                loginDetaiils.userName = Console.ReadLine();

                Console.WriteLine("Please Enter Password:");

                loginDetaiils.passWord = Console.ReadLine();

                var service = new LoginService();

                service.Login(loginDetaiils, token);

                Console.WriteLine("Please Enter MFA:");

                loginDetaiils.mfaToken = Console.ReadLine();

                loginDetaiils.deviceToken = token.deviceToken;

                service.Login(loginDetaiils, token);

                File.WriteAllText(fileName, JsonConvert.SerializeObject(token));
            }

            return token;

        }




    }
}

