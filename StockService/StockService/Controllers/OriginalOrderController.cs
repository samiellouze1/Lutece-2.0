using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockService.Data.IRepo;
using StockService.DTOs;
using StockService.Models;
using System.Security.Claims;

namespace StockService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OriginalOrderController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IOrderRepo _orderRepo;
        private readonly IOriginalOrderRepo _originalOrderRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        private readonly IStockUnitRepo _stockUnitRepo;

        public OriginalOrderController(IMapper mapper, IOrderRepo orderRepo, IOriginalOrderRepo originalOrderRepo, IUserRepo userRepo, UserManager<User> userManager, IStockUnitRepo stockUnitRepo)
        {
            _originalOrderRepo = originalOrderRepo;
            _orderRepo = orderRepo;
            _mapper = mapper;
            _userRepo = userRepo;
            _userManager = userManager;
            _stockUnitRepo = stockUnitRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OriginalOrderReadDTO>>> GetOriginalOrders()
        {
            Console.WriteLine("--------- Getting Original Orders --------");
            var originalorders = await _originalOrderRepo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<OriginalOrderReadDTO>>(originalorders));
        }
        [HttpGet("{id}", Name = "GetOriginalOrderById")]
        public async Task<ActionResult<OriginalOrderReadDTO>> GetOriginalOrderById(string id)
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
      
        [HttpPost("CreateOriginalOrder"),Authorize]
        public async Task<ActionResult<OriginalOrderReadDTO>> CreateOriginalOrder(OriginalOrderCreateDTO ordercreatedto)
        {
            Console.WriteLine("--------- Posting an Original Order --------");
            var originalorderModel = _mapper.Map<OriginalOrder>(ordercreatedto);
            // Get the user's unique identifier from claims
            var userId = User.FindFirst("Id").Value;
            var user = await _userManager.FindByIdAsync(userId);
            originalorderModel.User = user;
            await _originalOrderRepo.AddAsync(originalorderModel);
            var orignalorderreaddto = _mapper.Map<OriginalOrderReadDTO>(originalorderModel);
            return CreatedAtRoute(nameof(GetOriginalOrderById), new { id = orignalorderreaddto.Id }, orignalorderreaddto);
        }
    }
}
