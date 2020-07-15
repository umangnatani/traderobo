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
    public class PieController : BaseController
    {


        private DBService _service;
        public PieController(IWebHostEnvironment env, MyDatabaseContext context, IJwtToken token): base(env, context, token)
        {

            _service = new DBService(_context);
        }


        [Authorize]
        [HttpGet]
        public List<Pie> GetPies()
        {
            return _service.GetPies();
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
