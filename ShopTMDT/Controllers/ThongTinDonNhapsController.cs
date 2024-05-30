using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopTMDT.services;
using ShopTMDT.ViewModel;


namespace ShopTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongTinDonNhapsController : ControllerBase
    {
        private readonly IThongTinDonNhapRepository _thongTinDonNhapRepository;

        public ThongTinDonNhapsController(IThongTinDonNhapRepository thongTinDonNhapRepository)
        {
            _thongTinDonNhapRepository = thongTinDonNhapRepository;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var thong = _thongTinDonNhapRepository.GetAll();
            return Ok(thong);
        }
        [HttpPost("AddThongTinDonNhap")]
        public IActionResult AddThongTinDonNhap(ThongTinDonNhapVM thongTinDonNhapVM)
        {
            var thong = _thongTinDonNhapRepository.AddThongTinDonNhap(thongTinDonNhapVM);
            
            return Ok(thong);
        }
        [HttpPut("EditThongTinDonNhap")]
        public IActionResult EditThongTinDonNhap(int id, ThongTinDonNhapVM thongTinDonNhapVM)
        {
            var thong = _thongTinDonNhapRepository.EditThongTinDonNhap(id, thongTinDonNhapVM);
            return Ok(thong);
        }
        [HttpDelete("DeleteThongTinDonNhap")]
        public IActionResult DeleteThongTinDonNhap(int id)
        {
            var thong = _thongTinDonNhapRepository.DeleteThongTinDonNhap(id);
            return Ok(thong);
        }
    }
}
