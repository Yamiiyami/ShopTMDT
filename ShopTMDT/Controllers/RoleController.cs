using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopTMDT.services;
using ShopTMDT.ViewModel;

namespace ShopTMDT.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _roleRepository.GetAll());
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> Add(RoleVM rolevm)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _roleRepository.CreateRole(rolevm);
            return Ok(result);
        }

        [HttpDelete("deleterole")]
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var result = await _roleRepository.DeleteRole(id);
            return Ok(result);
        }
    }
}
