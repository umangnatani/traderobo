using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TradeRobo.Service
{

    public static class Settings
    {
      
        public static string ClientId { get; set; }
        public static string TDClientId { get; set; }
        public static string TDRedirectURI { get; set; }
        public static string CSVPath { get; set; }
        public static string Proxy { get; set; }
        public static bool UseProxy { get; set; }

        public const string AVKey = "ZIRV238H7WHUQ7YH";

        static Settings()
        {
            ClientId = "c82SH0WZOsabOXGP2sxqcj34FxkvfnWRZBKlBjFS";
            CSVPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"/Content/";

        }

        
    }


    
    public static class Helper
    {

        public static string Key { get; set; }

        static Helper()
        {
            Key = "glbw-1rd9-coro19";
        }

      
        public static double Round(double price)
        {
            if (price <= 1e-2)
                return Math.Round(price, 6);
            else if (price < 1e0)
                return Math.Round(price, 4);
            else
                return Math.Round(price, 2);

        }


        public static string GenerateDeviceToken()
        {
            List<int> rands = new List<int>();
            var rng = new Random();
            for (int i = 0; i < 16; i++)
            {
                var r = rng.NextDouble();
                double rand = 4294967296.0 * r;
                rands.Add(((int)((uint)rand >> ((3 & i) << 3))) & 255);
            }

            List<string> hex = new List<string>();
            for (int i = 0; i < 256; ++i)
            {
                hex.Add(Convert.ToString(i + 256, 16).Substring(1));
            }

            string id = "";
            for (int i = 0; i < 16; i++)
            {
                id += hex[rands[i]];

                if (i == 3 || i == 5 || i == 7 || i == 9)
                {
                    id += "-";
                }
            }

            return id;
        }

        public static string Encrypt(string input)
        {
            return Encrypt(input, Key);
        }

        public static string Decrypt(string input)
        {
            return Decrypt(input, Key);
        }

        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

    }
}
