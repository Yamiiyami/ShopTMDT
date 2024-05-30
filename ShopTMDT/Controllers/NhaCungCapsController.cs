using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopTMDT.services;
using ShopTMDT.ViewModel;


namespace ShopTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NhaCungCapsController : ControllerBase
    {
        private readonly INhaCungCapReepository _nhaCungCapReepository;

        public NhaCungCapsController(INhaCungCapReepository nhaCungCapReepository)
        {
            _nhaCungCapReepository = nhaCungCapReepository;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var nha = _nhaCungCapReepository.GetAll();
            return Ok(nha);
        }
        [HttpPost("AddNhaCungCap")]
        public IActionResult AddNhaCungCap(int id, NhaCungCapVM nhaCungCapVM)
        {
            var nha = _nhaCungCapReepository.AddNhaCungCap(id, nhaCungCapVM);
            return Ok(nha);
        }
        [Authorize(Roles ="Admin")]
        [HttpPut("EditNhaCungCap")]
        public IActionResult EditNhaCungCap(int id, NhaCungCapVM nhaCungCapVM)
        {
            var nha = _nhaCungCapReepository.EditNhaCungCap(id, nhaCungCapVM);
            return Ok(nha);
        }
        [HttpDelete("DeleteNhaCungCap")]
        public IActionResult DeleteNhaCungCap(int id)
        {
            var nha = _nhaCungCapReepository.DeleteNhaCungCap(id);
            return Ok(nha);
        }
    }
}
