using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopTMDT.services;
using ShopTMDT.ViewModel;



namespace ShopTMDT.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class HinhAnhsController : ControllerBase
    {
        private readonly IHinhAnhRepository _hinhAnhRepository;

        public HinhAnhsController(IHinhAnhRepository hinhAnhRepository)
        {
            _hinhAnhRepository = hinhAnhRepository;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var hinh = _hinhAnhRepository.GetAll();
            return Ok(hinh);
        }
        [HttpPost("AddHinhAnh")]
        public async Task<IActionResult> AddHinhAnh(UpLoadHinhAnh hinhAnhVM)
        {
            var hinh =await _hinhAnhRepository.AddHinhAnhAsync(hinhAnhVM);
            return Ok(hinh);
        }
        [HttpPut("EditHinhAnh")]
        public IActionResult EditHinhAnh(int id, HinhAnhVM hinhAnhVM)
        {
            var hinh = _hinhAnhRepository.EditHinhAnh(id, hinhAnhVM);
            return Ok(hinh);
        }
        [HttpDelete("DeleteHinhAnh")]
        public IActionResult DeleteHinhAnh(int id)
        {
            var hinh = _hinhAnhRepository.DeleteHinhAnh(id);
            return Ok(hinh);
        }
    }
}
