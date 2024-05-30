using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ShopTMDT.Data;
using ShopTMDT.services;
using ShopTMDT.ViewModel;
using System.Security.Claims;

namespace ShopTMDT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository) 
        {
            _accountRepository = accountRepository;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(Register register)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            var result =await _accountRepository.Register(register);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("registeradmin")]
        public async Task<IActionResult> AdminCreate(AdminRegsister account)
        {
            var result = await _accountRepository.AdminRegister(account);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRepuest login)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _accountRepository.Login(login);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("getalluser")]
        public async Task<IActionResult> GetUsers()
        {
             
            var users =await _accountRepository.GetUsers();
            return Ok(users);
        }//Account/detail
        [HttpGet("detail")]
        public async Task<IActionResult> GetDetail()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _accountRepository.GetUserDetail(currentUserId!);
            return Ok(user);
        }


        
        [Authorize(Roles="Admin")]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _accountRepository.Delete(id);
            return Ok(result);
        }


    }
}
