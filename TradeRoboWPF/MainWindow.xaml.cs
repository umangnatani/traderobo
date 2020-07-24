using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TradeRobo.Service;

namespace TradeRoboWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string fileName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\.tokens\td.json";

        private TDClient client;
        private TDToken token;

        private TradeService orderService;

        public MainWindow()
        {
            InitializeComponent(); 
            Settings.CSVPath = @"C:\My Stuff\Dev\Trading\TradeRobo\TradeRobo\bin\Release\netcoreapp3.1\publish/Content/";

            //client = new TDClient();

            token = new TDToken();

            //orderService = new OrderService();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TDLogin();

            var tdOrder = new TDOrder { Symbol = Symbol.Text , Price = Convert.ToDouble(Price.Text), Quantity =  Convert.ToInt32(Quantity.Text) , Increment = Convert.ToDouble(Increment.Text), Total = Convert.ToInt32(Total.Text) };

            //var tdOrder = new TDOrder { Symbol = "KBE", Price = 26.25, Quantity = 10, Increment = .2, Total = 5 };

            orderService.PlaceTDOrder(tdOrder, 1);

            //client.PlaceOrder(tdOrder, token);

            //client.GetAccount(token);

        }



        private void TDLogin()
        {

            if (File.Exists(fileName))
            {
                token = JsonConvert.DeserializeObject<TDToken>(File.ReadAllText(fileName));
            }

            if (true)
            {

                client.Authenticate();

                File.WriteAllText(fileName, JsonConvert.SerializeObject(token));
            }
        }

    }
}
