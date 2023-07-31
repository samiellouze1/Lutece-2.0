using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockService.Data.IRepo;
using StockService.DTOs;
using StockService.Models;

namespace StockService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IOriginalOrderRepo _originalOrderRepo;
        private readonly IMapper _mapper;

        public OrderController(IMapper mapper, IOrderRepo orderRepo, IOriginalOrderRepo originalOrderRepo, IUserRepo userRepo, IStockUnitRepo StockUnitRepo, IStockRepo stockRepo)
        {
            _orderRepo = orderRepo;
            _originalOrderRepo = originalOrderRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OriginalOrderReadDTO>>> GetOriginalOrders()
        {
            Console.WriteLine("--------- Getting Original Orders --------");
            var originalorders = await _originalOrderRepo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<OriginalOrderReadDTO>>(originalorders));
        }
        [HttpGet("{id}", Name = "GetOrderById")]
        public async Task<ActionResult<OriginalOrderReadDTO>> GetOriginalOrderById(int id)
        {
            var originalorderitem = await _originalOrderRepo.GetByIdAsync(id);
            if (originalorderitem != null)
            {
                return Ok(_mapper.Map<OriginalOrderReadDTO>(originalorderitem));
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<ActionResult<OriginalOrderReadDTO>> CreateOriginalOrder(OriginalOrderCreateDTO ordercreatedto)
        {
            Console.WriteLine("--------- Posting an Original Order --------");
            var originalorderModel = _mapper.Map<OriginalOrder>(ordercreatedto);
            await _originalOrderRepo.AddAsync(originalorderModel);
            var orignalorderreaddto = _mapper.Map<OriginalOrderReadDTO>(originalorderModel);
            return CreatedAtRoute(nameof(GetOriginalOrderById), new { id = orignalorderreaddto.Id }, orignalorderreaddto);
        }
    }
}
