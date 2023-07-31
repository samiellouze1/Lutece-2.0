using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockService.Data;
using StockService.Data.Enums;
using StockService.Data.Models;
using StockService.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtTokenController : ControllerBase
    {
        public IConfiguration _configuration;
        public SignInManager<User> _signInManager;
        public UserManager<User> _userManager;
        public readonly AppDbContext _context;
        public JwtTokenController(IConfiguration configuration, AppDbContext context, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _configuration = configuration;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Post(User user)
        {
            if (user!=null && user.UserName != null && user.Password != null)
            {
                var userData = await _userManager.FindByNameAsync(user.UserName);
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                if (userData !=null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("UserName", user.UserName),
                        new Claim("Password",user.Password)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(jwt.Issuer, jwt.Audience,claims,expires: DateTime.UtcNow.AddMinutes(20), signingCredentials: signIn);
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the user already exists
                var existingUser = await _userManager.FindByNameAsync(model.UserName);
                if (existingUser != null)
                {
                    return BadRequest("User with the provided username already exists.");
                }

                // Create a new User entity and add it to the database
                var newUser = new User
                {
                    UserName = model.UserName,
                    UserType = UserTypeEnum.Real,
                    Balance = 0,
                    // Set other properties as needed
                };

                _userManager.CreateAsync(newUser, model.Password);
                await _context.SaveChangesAsync();

                // Return a success message or the created user data
                return Ok("User registered successfully.");
            }
            else
            {
                return BadRequest("Invalid registration data.");
            }
        }

    }
}
