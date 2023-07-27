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
        private readonly IOrderRepo _repository;
        private readonly IMapper _mapper;
        public OrderController(IOrderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<OrderReadDTO>> GetOrders()
        {
            Console.WriteLine("--------- Getting Orders --------");
            var orderitem = _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<OrderReadDTO>>(orderitem));
        }
        [HttpGet("{id}",Name = "GetOrderById")]
        public ActionResult<OrderReadDTO> GetOrderById (int id)
        {
            var orderitem = _repository.GetOrderById(id);
            if (orderitem != null)
            {
                return Ok(_mapper.Map<OrderReadDTO>(orderitem));
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<ActionResult<OrderReadDTO>> CreateOrder (OrderCreateDTO ordercreatedto)
        {
            var orderModel = _mapper.Map<Order>(ordercreatedto);
            _repository.CreateOrder(orderModel);
            _repository.SaveChanges();
            var orderreaddto = _mapper.Map<OrderReadDTO>(orderModel);
            return CreatedAtRoute(nameof(GetOrderById), new { id = orderreaddto.Id }, orderreaddto);
        }
    }
}
