using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;


namespace TradeRobo.Service
{
    public class AllyClient
    {
        


        RestClient client;
        

        public AllyClient()
        {
            
            client = new RestClient();

        }


        public void PlaceOrder(Order order)
        {

            var AccountId = "84743083";

            var APIEndPoint = EndPoint.AllyOrders.Replace("{accountId}", AccountId);


            //order.Quote = get_quotes(order.Symbol);

            var payload = @"<FIXML xmlns='http://www.fixprotocol.org/FIXML-5-0-SP2'><Order TmInForce='0' Typ='{Type}' Side='{Side}' Acct='{accountId}' Px='{LimitPrice}'><Instrmt SecTyp='CS' Sym='{Symbol}'/><OrdQty Qty='{Quantity}'/></Order></FIXML>";


            //var orderType = 

            payload = payload.Replace("{accountId}", AccountId);
            payload = payload.Replace("{Symbol}", order.Symbol);
            payload = payload.Replace("{Type}", order.OrderGroup.Type == "limit"? "2":"1" );
            payload = payload.Replace("{Side}", order.OrderGroup.Side == "buy"? "1": "2");
            payload = payload.Replace("{LimitPrice}", order.Price.RoundDecimal().ToString());
            payload = payload.Replace("{Quantity}", order.Quantity.ToString());

            if (order.Quantity > 0)
            {


                try
                {
                    var returnValue = client.PostAsRaw(APIEndPoint, payload);

                    //var json = JsonConvert.DeserializeObject<>(returnValue);

                    order.Success = true;


                }
                catch (Exception e)
                {
                    order.ExecMessage = $"Order failed for {order.Symbol} with error: {e.Message}";
                }
            }
            else
                order.ExecMessage = $"Order failed for {order.Symbol} with 0 quantity";



        }


    }
}
