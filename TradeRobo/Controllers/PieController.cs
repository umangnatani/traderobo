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


    public class WatchListSymbolVM
    {
        public string Symbols { get; set; }
    }

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
        public async Task<ReturnType> Save(Pie poco)
        {
            if (poco.Id == 0)
                poco.UserId = GetUserId();

            return await _service.Save(poco);
        }


        [HttpPost]
        [Route("save")]
        public async Task<ReturnType> Save(List<PieDetail> list)
        {
            foreach (var item in list)
            {
                item.Symbol = item.Symbol.ToUpper();
                await _service.Save(item);
            }

            return new ReturnType();

        }

        [HttpPost]
        [Route("watchlist/{Id}")]
        public ReturnType SaveWatchListSymbols(int Id, WatchListSymbolVM poco)
        {

            return _service.SaveWatchListSymbols(Id, poco.Symbols);
            

        }




        [HttpPost]
        [Route("delete/{PieDetailId}")]
        public ReturnType DeletePieDetail(Int32 PieDetailId)
        {

            return _service.DeletePieDetail(PieDetailId);
        }


        [HttpGet]
        [Route("detail/{PieId}")]
        public List<PieDetail> GetPieDetails(Int32 PieId)
        {

            return _service.GetPieDetails(PieId);
        }



        [HttpGet]
        [Route("refresh")]
        public ReturnType RefreshMA()
        {
            var UserId = GetUserId();

            var tradeService = new TradeService(_context, UserId);

            return tradeService.RefreshMA();
        }


        [HttpGet]
        [Route("toggle")]
        public ReturnType ToggleProxy ()
        {
            return _service.ToggleProxy();
        }


        [HttpGet]
        [Route("detail/{PieId}/{flag}")]
        public List<PieDetail> GetPieDetails(Int32 PieId, string flag)
        {
            var UserId = GetUserId();

            var tradeService = new TradeService(_context, UserId);

            return tradeService.GetPieDetailsWithQuote(PieId, true);
        }



        



    }
}
