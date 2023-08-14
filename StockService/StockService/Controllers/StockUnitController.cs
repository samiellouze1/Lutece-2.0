using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using StockService.Models;
using StockService.Repo.IRepo;
using StockService.Repo.Repo;

namespace StockService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockUnitController : ControllerBase
    {
        private readonly IStockUnitRepo _stockUnitRepo;
        private readonly IMapper _mapper;
        public StockUnitController(IMapper mapper,IStockUnitRepo stockUnitRepo)
        {
            _stockUnitRepo = stockUnitRepo;
            _mapper = mapper;
        }
        [HttpGet("/{stockid}/{userid}", Name = "GetStockUnitsByStockId")]
        public async Task<ActionResult<int>> GetStockUnitsByStockId(string stockid, string userid)
        {
            var allstockunitss = await _stockUnitRepo.GetAllAsync(s => s.Stock,s=>s.User);
            var specificstockunits = allstockunitss.Where(s=>s.Stock.Id == stockid).Where( s=>s.User.Id== userid).ToList();
            return Ok(specificstockunits.Count);
        }
    }
}
