using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockService.Data.Enums;
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
            //for visualizing progress
            Console.WriteLine("--------- Posting an Original Order --------");

            //mapping
            var originalorderModel = _mapper.Map<OriginalOrder>(ordercreatedto);

            // Get the current user
            var userId = User.FindFirst("Id").Value;
            var user = await _userManager.FindByIdAsync(userId);

            //adding the original order to db
            originalorderModel.User = user;
            originalorderModel.RemainingQuantity = ordercreatedto.OriginalQuantity;
            await _originalOrderRepo.AddAsync(originalorderModel);

            if (originalorderModel.OrderType==OrderTypeEnum.Buy)
            {
                if (user.Balance<originalorderModel.Price*originalorderModel.OriginalQuantity)
                {
                    return BadRequest("you don't have enough balance to do this order");
                }
                else
                {
                    var OriginalOrders = await _originalOrderRepo.GetAllAsync(oo => oo.Stock, oo => oo.Orders,oo=>oo.User) ;
                    var sellingOriginalOrdersList = OriginalOrders.
                                                                    ToList().
                                                                    Where(oo=>oo.OrderType==OrderTypeEnum.Sell).
                                                                    Where(oo=>oo.Stock==originalorderModel.Stock).
                                                                    Where(oo=>oo.OriginalOrderStatus== OriginalOrderStatusEnum.Active).
                                                                    OrderBy(oo=>oo.Price);
                    var quantityneeded = originalorderModel.OriginalQuantity;
                    foreach (var originalorder in sellingOriginalOrdersList) 
                    {
                        var originalorderexecuted = true;
                        foreach ( var order in originalorder.Orders.Where(o=>o.OrderStatus==OrderStatusEnum.Active))
                        {
                            if (quantityneeded == 0)
                            {
                                break;
                                originalorderexecuted = false;
                            }
                            order.OrderStatus= OrderStatusEnum.Executed;
                            order.ExecutedPrice = originalorderModel.Price;
                            await _orderRepo.SaveChangesAsync();
                            quantityneeded-=1;

                            //change information of the stock unit
                            var theseller = originalorder.User;
                            var stockunit = theseller.StockUnits.Where(su => su.Stock == originalorder.Stock).Where(su => su.StockUnitStatus == StockUnitStatusEnum.InMarket).ToList()[0];
                            stockunit.User = user;
                            stockunit.StockUnitStatus = StockUnitStatusEnum.InStock;
                            stockunit.DateBought= DateTime.Now;

                        }

                        if (originalorderexecuted)
                        {
                            originalorderModel.OriginalOrderStatus=OriginalOrderStatusEnum.Executed;
                            await _originalOrderRepo.SaveChangesAsync();                       
                        }
                        originalorderModel.RemainingQuantity = quantityneeded;
                        await _originalOrderRepo.SaveChangesAsync();
                    }
                }
            }
            else 
            {
                //mezel
                return BadRequest();
            }
            var orignalorderreaddto = _mapper.Map<OriginalOrderReadDTO>(originalorderModel);
            return CreatedAtRoute(nameof(GetOriginalOrderById), new { id = orignalorderreaddto.Id }, orignalorderreaddto);
        }
    }
}
