using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockService.Data.IRepo;
using StockService.Data.Repo;
using StockService.DTOs;
using StockService.Models;

namespace StockService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        public UserController(IMapper mapper, IUserRepo userRepo)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetUsers()
        {
            Console.WriteLine("--------- Getting users --------");
            var users = await _userRepo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<UserReadDTO>>(users));
        }
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<UserReadDTO>> GetUserById(string id)
        {
            var useritem = await _userRepo.GetByIdAsync(id);
            if (useritem != null)
            {
                return Ok(_mapper.Map<UserReadDTO>(useritem));
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<ActionResult<UserReadDTO>> CreateUser(UserCreateDTO usercreatedto)
        {
            Console.WriteLine("--------- Posting a User --------");
            var userModel = _mapper.Map<User>(usercreatedto);
            await _userRepo.AddAsync(userModel);
            var userreaddto = _mapper.Map<UserReadDTO>(userModel);
            return CreatedAtRoute(nameof(GetUserById), new { id = userreaddto.Id }, userreaddto);
        }
    }
}
