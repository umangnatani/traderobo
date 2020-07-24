using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeRobo.Service;
using Newtonsoft.Json;
using System.Security.Claims;

namespace TradeRobo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PieController : BaseController
    {


        private DBService _service;
        public PieController(IWebHostEnvironment env, MyDatabaseContext context): base(env, context)
        {

            _service = new DBService(_context);
        }


        
        [HttpGet]
        public List<Pie> GetPies()
        {
            var UserId = GetUserId();
            return _service.GetPies(UserId);
        }


        [HttpPost]
        public ReturnType Save(Pie poco)
        {
            if (poco.Id == 0)
                poco.UserId = GetUserId();

            return _service.SavePie(poco);
        }



        [HttpGet]
        [Route("fav")]
        public List<FavStocks> GetAllFavStocks()
        {
            return _service.GetAllFavStocks();
        }


        [HttpGet]
        [Route("detail/{PieId}")]
        public List<PieDetail> GetPieDetails(Int32 PieId)
        {

            return _service.GetPieDetails(PieId);
        }

        [HttpGet]
        [Route("detail/{PieId}/{flag}")]
        public List<PieDetail> GetPieDetails(Int32 PieId, string flag)
        {
            var UserId = GetUserId();

            var tradeService = new TradeService(_context, UserId, true);

            return tradeService.GetPieDetailsWithQuote(PieId, true);
        }


        //[HttpPost]
        //[Route("save")]
        //public string Save(List<PieDetail> list)
        //{
        //    foreach (var item in list)
        //    {
        //        _service.SavePieDetails(item);
        //    }

        //    return "Saved Details";

        //}


        [HttpPost]
        [Route("save")]
        public IActionResult Save(List<PieDetail> list)
        {
            foreach (var item in list)
            {
                _service.SavePieDetails(item);
            }

            return Ok(JsonConvert.SerializeObject("Saved Details successfully."));

        }



    }
}
