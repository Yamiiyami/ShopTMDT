using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopTMDT.ViewModel;
using SpQuanAo.services;

namespace ShopTMDT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ThongTinXuatController : ControllerBase
    {
        private readonly IThongTinXuatRepository _thongTinXuatRepository;
        public ThongTinXuatController(IThongTinXuatRepository thongTinXuatRepository)
        {
            _thongTinXuatRepository = thongTinXuatRepository;
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string Id)
        {
            var tthd = await _thongTinXuatRepository.GetByIdHoaDon(Id);
            
            return Ok(tthd);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ThongTinXuatResponse thontin)
        {
            var result = await _thongTinXuatRepository.Create(thontin);
            return Ok(result);

        }
    }
}
