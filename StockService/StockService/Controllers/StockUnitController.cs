using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockService.Data.IRepo;
using StockService.Data.Repo;
using StockService.DTOs;
using StockService.Models;

namespace StockService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockUnitController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IStockRepo _stockRepo;
        private readonly IMapper _mapper;
        public StockUnitController(IMapper mapper, IStockRepo stockRepo, IUserRepo userRepo)
        {
            _userRepo = userRepo;
            _stockRepo = stockRepo;
            _mapper = mapper;
        }
        [HttpGet("/Stock/{id}", Name = "GetStockUnitsByStockId")]
        public async Task<ActionResult<List<StockUnit>>> GetStockUnitsByStockId(string id)
        {
            var stockitem = await _stockRepo.GetByIdAsync(id, s => s.StockUnits);
            if (stockitem != null)
            {
                return Ok(stockitem.StockUnits);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("/User/{id}", Name = "GetStockUnitsByUserId")]
        public async Task<ActionResult<List<StockUnit>>> GetStockUnitsByUserId(string id)
        {
            var useritem = await _userRepo.GetByIdAsync(id, s=>s.StockUnits);
            if ( useritem != null )
            {
                return Ok(useritem.StockUnits);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
