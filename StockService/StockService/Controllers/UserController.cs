using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockService.Data.DTOs;
using StockService.Data.Enums;
using StockService.Models;
using StockService.Repo.IRepo;
using StockService.Repo.Repo;

namespace StockService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserController(IMapper mapper, IUserRepo userRepo, UserManager<User> userManager, SignInManager<User> signInManager)
        {

            _userRepo = userRepo;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
                    if (result.Succeeded)
                    {
                        return Ok("authenticated");
                    }
                }
            }
            return BadRequest("Invalid Credentials");
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Signed Out");
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                var newUser = new User()
                {
                    UserName = username,
                    UserType=UserTypeEnum.Real,
                    Balance = 0
                    //needs other details
                };
                var newUserResponse = await _userManager.CreateAsync(newUser, password);
                if ( newUserResponse.Succeeded)
                {
                    //await _userManager.AddToRoleAsync(newUser);
                    var result = await _signInManager.PasswordSignInAsync(newUser, password, false, false);
                    if (result.Succeeded)
                    {
                        return Ok("Registered and logged in");
                    }
                    else
                    {
                        return BadRequest("Signin error");
                    }
                }
                else
                {
                    return BadRequest("Creation error");
                }
            }
            else
            {
                return BadRequest("User already exists");
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetUsers()
        {
            Console.WriteLine("--------- Getting users --------");
            var stocks = await _userRepo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<UserReadDTO>>(stocks));
        }
    }
}
