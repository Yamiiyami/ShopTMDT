using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopTMDT.services;
using ShopTMDT.ViewModel;


namespace ShopTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhuyenMaisController : ControllerBase
    {
        private readonly IKhuyenMaiRepository _khuyenMaiRepository;

        public KhuyenMaisController(IKhuyenMaiRepository khuyenMaiRepository)
        {
            _khuyenMaiRepository = khuyenMaiRepository;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var khuyen = _khuyenMaiRepository.GetAll();
            return Ok(khuyen);
        }
        [HttpPost("AddKhuyenMai")]
        public IActionResult AddKhuyenMai(KhuyenMaiVM khuyenMaiVM)
        {
            var khuyen = _khuyenMaiRepository.AddKhuyenMai(khuyenMaiVM);
            return Ok(khuyen);
        }
        [HttpPut("EditKhuyenMai")]
        public IActionResult EditKhuyenMai(int id, KhuyenMaiVM khuyenMaiVM)
        {
            var khuyen = _khuyenMaiRepository.EditKhuyenMai(id, khuyenMaiVM);
            return Ok(khuyen);
        }
        [HttpDelete("DeleteKhuyenMai")]
        public IActionResult DeleteKhuyenMai(int id)
        {
            var khuyen = _khuyenMaiRepository.DeleteKhuyenMai(id);
            return Ok(khuyen);
        }
    }
}
