using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace TradeRobo.Service
{
    public class FolioService
    {
        public List<string> GetFolios(string directory)
        {
            //if (string.IsNullOrWhiteSpace(directory))
            //    directory = @"C:\My Stuff\Dev\Trading\TradeRobo\TradeRobo";
            return Directory.GetFiles(Settings.CSVPath, "*.csv").Select(x => Path.GetFileName(x)).ToList();
        }
    }
}
