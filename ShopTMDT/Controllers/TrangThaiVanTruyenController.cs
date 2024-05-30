using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopTMDT.ViewModel;
using SpQuanAo.services;

namespace ShopTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrangThaiVanTruyenController : ControllerBase
    {
        private readonly ITrangThaiVanchuyenRepository _trangThaiVanchuyenRepository;
        public TrangThaiVanTruyenController(ITrangThaiVanchuyenRepository trangThaiVanchuyenRepository)
        {
            _trangThaiVanchuyenRepository = trangThaiVanchuyenRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var status =await _trangThaiVanchuyenRepository.GetAll();
            return Ok(status);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(StatusTransportResponse status)
        {
            var result = await _trangThaiVanchuyenRepository.Create(status);
            return Ok(result);
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(StatusTransportResponse status)
        {
            var result = await _trangThaiVanchuyenRepository.Edit(status);
            return Ok(result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _trangThaiVanchuyenRepository.Delete(id);
            return Ok(result);
        }
    }
}
