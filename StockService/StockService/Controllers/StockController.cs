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
    public class StockController : ControllerBase
    {
        private readonly IStockRepo _stockRepo;
        private readonly IMapper _mapper;
        public StockController(IMapper mapper, IStockRepo stockRepo)
        {
            _stockRepo = stockRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockReadDTO>>> GetStocks()
        {
            Console.WriteLine("--------- Getting stocks --------");
            var stocks = await _stockRepo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<StockReadDTO>>(stocks));
        }
        [HttpGet("{id}", Name = "GetStockById")]
        public async Task<ActionResult<StockReadDTO>> GetStockById(string id)
        {
            var stockitem = await _stockRepo.GetByIdAsync(id);
            if (stockitem != null)
            {
                return Ok(_mapper.Map<StockReadDTO>(stockitem));
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<ActionResult<StockReadDTO>> CreateStock(StockCreateDTO stockcreatedto)
        {
            Console.WriteLine("--------- Posting a Stock --------");
            var stockModel = _mapper.Map<Stock>(stockcreatedto);
            await _stockRepo.AddAsync(stockModel);
            var stockreaddto = _mapper.Map<StockReadDTO>(stockModel);
            return CreatedAtRoute(nameof(GetStockById), new { id = stockreaddto.Id }, stockreaddto);
        }
    }
}
