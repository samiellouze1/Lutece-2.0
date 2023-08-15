using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockService.Data.DTOs;
using StockService.Data.Enums;
using StockService.Models;
using StockService.Repo.IRepo;
using StockService.Services.IServices;

namespace StockService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OriginalOrderController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IOriginalOrderRepo _originalOrderRepo;
        private readonly IMapper _mapper;
        private readonly ICreateOriginalOrderService _createooService;

        public OriginalOrderController(IMapper mapper, IOriginalOrderRepo originalOrderRepo, UserManager<User> userManager, ICreateOriginalOrderService createooService)
        {
            _originalOrderRepo = originalOrderRepo;
            _mapper = mapper;
            _userManager = userManager;
            _createooService = createooService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OriginalOrderReadDTO>>> GetOriginalOrders()
        {
            Console.WriteLine("--------- Getting Original Orders --------");
            var originalorders = await _originalOrderRepo.GetAllAsync(oo=>oo.Orders);
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
        [HttpPost]
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
            //Buying Order
            if (originalorderModel.OriginalOrderType == OriginalOrderTypeEnum.Buy)
            {
                if (user.Balance < originalorderModel.Price * originalorderModel.OriginalQuantity)
                {
                    return BadRequest("you don't have enough balance to execute this order");
                }
                else
                {
                    var sellingOriginalOrdersList = await _createooService.GetCorrespondantOrders(originalorderModel,OriginalOrderTypeEnum.Buy);
                    var quantityneeded = originalorderModel.OriginalQuantity;
                    foreach (var originalorder in sellingOriginalOrdersList.Where(oo=>oo.User!=user))
                    {
                        var originalorderexecuted = true;
                        foreach (var order in originalorder.Orders.Where(o => o.OrderStatus == OrderStatusEnum.Active))
                        {
                            if (quantityneeded == 0)
                            {
                                originalorderexecuted = false;
                                break;
                            }
                            await _createooService.ExecuteOrder(order, originalorderModel.Price);
                            await _createooService.ChangeInformationOfAStockUnit(originalorder, user);
                            await _createooService.CreateExecutedOrder(originalorderModel);
                            quantityneeded -= 1;
                        }
                        if (originalorderexecuted)
                        {
                            await _createooService.ExecuteOriginalOrder(originalorder);
                        }
                    }
                    await _createooService.StoreRemainingQuantity(originalorderModel, quantityneeded);
                    if (quantityneeded == 0)
                    {
                        await _createooService.ExecuteOriginalOrder(originalorderModel);
                    }
                    for (int i = 0; i < quantityneeded; i++)
                    {
                        await _createooService.CreateInMarketOrder(originalorderModel);
                    }
                }
            }
            //selling order
            else
            {
                var stockunittolist = await _createooService.GetAllCorrespondingStockUnits(originalorderModel);
                if (stockunittolist.Count < originalorderModel.OriginalQuantity)
                {
                    return BadRequest("you do not have enough stock units to execute this order");
                }
                else
                {
                    var correspondingoriginalorders = await _createooService.GetCorrespondantOrders(originalorderModel, OriginalOrderTypeEnum.Sell);
                    var neededquantity = originalorderModel.OriginalQuantity;
                    foreach ( var originalorder in correspondingoriginalorders.Where(oo=>oo.User!=user).ToList())
                    {
                        var executedoriginalorder = true;
                        foreach ( var order in originalorder.Orders)
                        {
                            if (neededquantity==0)
                            {
                                executedoriginalorder = false;
                                break;
                            }
                            await _createooService.ExecuteOrder(order,order.OriginalOrder.Price);
                            Console.WriteLine("order executed");
                            await _createooService.ChangeInformationOfAStockUnitFromUsertoUser(originalorder, user);
                            Console.WriteLine("stock unit information done");
                            if (executedoriginalorder)
                            {
                                await _createooService.ExecuteOriginalOrder(originalorder);
                            }
                            neededquantity -= 1;
                        }
                    }
                    await _createooService.StoreRemainingQuantity(originalorderModel, neededquantity);
                    for (int i = 0;i< originalorderModel.RemainingQuantity;i++)
                    {

                        await _createooService.ChangeInformationOfAStockUnit(originalorderModel);
                        await _createooService.CreateInMarketOrder(originalorderModel);
                    }
                }
            }
            var orignalorderreaddto = _mapper.Map<OriginalOrderReadDTO>(originalorderModel);
            return CreatedAtRoute(nameof(GetOriginalOrderById), new { id = orignalorderreaddto.Id }, orignalorderreaddto);
        }

    }
}
