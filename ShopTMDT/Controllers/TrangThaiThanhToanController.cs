using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopTMDT.ViewModel;
using SpQuanAo.services;

namespace ShopTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrangThaiThanhToanController : ControllerBase
    {
        private readonly ITrangThaiThanhToanRepository _trangThaiThanhToanRepository;
        public TrangThaiThanhToanController(ITrangThaiThanhToanRepository trangThaiThanhToanRepository)
        {
            _trangThaiThanhToanRepository = trangThaiThanhToanRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var status = await _trangThaiThanhToanRepository.GetAll();
            return Ok(status);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(StatusPaymentRespoonse status)
        {
            var result = await _trangThaiThanhToanRepository.Create(status);
            return Ok(result);
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(StatusPaymentRespoonse status)
        {
            var result = await _trangThaiThanhToanRepository.Edit(status);
            return Ok(result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _trangThaiThanhToanRepository.Delete(id);
            return Ok(result);
        }
    }
}
