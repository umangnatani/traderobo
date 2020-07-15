using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeRobo.Service;

namespace TradeRobo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TDController : BaseController
    {

        public TDController(IWebHostEnvironment env, MyDatabaseContext context, IJwtToken token) : base(env, context, token)
        {

        }

        [HttpPost]
        [Route("order")]
        public Order PlaceOrder(TDOrder poco)
        {
            TradeService service = new TradeService(_context);
            service.PlaceOrder(poco);
            return poco;
        }
    }
}
