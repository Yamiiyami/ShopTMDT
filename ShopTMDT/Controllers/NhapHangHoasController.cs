using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopTMDT.services;
using ShopTMDT.ViewModel;


namespace ShopTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhapHangHoasController : ControllerBase
    {
        private readonly INhapHangHoaRepository _nhapHangHoaRepository;

        public NhapHangHoasController(INhapHangHoaRepository nhapHangHoaRepository)
        {
            _nhapHangHoaRepository = nhapHangHoaRepository;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var nhap = _nhapHangHoaRepository.GetAll();
            return Ok(nhap);
        }
        [HttpPost("AddNhapHangHoa")]
        public IActionResult AddNhapHangHoa(NhaphanghoaReposet nhapHangHoa)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var nhap = _nhapHangHoaRepository.AddNhapHangHoa(nhapHangHoa);
            return Ok(nhap);
        }
        [HttpPut("EditNhapHangHoa")]
        public IActionResult EditNhapHangHoa(int id, NhapHangHoaVM nhapHangHoaVM)
        {
            var nhap = _nhapHangHoaRepository.EditNhapHangHoa(id, nhapHangHoaVM);
            return Ok(nhap);
        }
        [HttpDelete("DeleteNhapHangHoa")]
        public IActionResult DeleteNhapHangHoa(int id)
        {
            var nhap = _nhapHangHoaRepository.DeleteNhapHangHoa(id);
            return Ok(nhap);
        }
    }
}
