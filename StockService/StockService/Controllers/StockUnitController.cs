using Microsoft.AspNetCore.Mvc;
using StockService.Data.DTOs;
using StockService.Repo.IRepo;

namespace StockService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockUnitController : ControllerBase
    {
        private readonly IStockUnitRepo _stockUnitRepo;
        public StockUnitController(IStockUnitRepo stockUnitRepo)
        {
            _stockUnitRepo = stockUnitRepo;
        }
        [HttpPost("StockUser")]
        public async Task<ActionResult<int>> GetStockUnitsByStockId(StockUserDTO stockuser)
        {
            var allstockunitss = await _stockUnitRepo.GetAllAsync(s => s.Stock,s=>s.User);
            var specificstockunits = allstockunitss.Where(s=>s.Stock.Id == stockuser.StockId).Where( s=>s.User.Id== stockuser.UserId).ToList();
            return Ok(specificstockunits.Count);
        }
    }
}
