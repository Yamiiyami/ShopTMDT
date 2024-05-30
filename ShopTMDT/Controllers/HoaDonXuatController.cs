using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ShopTMDT.ViewModel;
using SpQuanAo.services;
using System.Security.Claims;

namespace ShopTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HoaDonXuatController : ControllerBase
    {
        private readonly IXuatHangHoaRepository _xuatHangHoaRepository;
        public HoaDonXuatController(IXuatHangHoaRepository xuatHangHoaRepository)
        {
            _xuatHangHoaRepository = xuatHangHoaRepository;
        }
        [Authorize("Admin")]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var hoadons = await _xuatHangHoaRepository.GetAll();
            return Ok(hoadons);
        }
        [Authorize("Admin")]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string id)
        {
            var hoadon = await _xuatHangHoaRepository.GetById(id);
            return Ok(hoadon);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(HoaDonRequest hoadon)
        {
            var result = await _xuatHangHoaRepository.Create(hoadon);
            return Ok(result);
        }

        [HttpGet("getbyiduser")]
        public async Task<IActionResult> GetByIdUser()
        {
            var current = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(current == null)
            {
                return BadRequest("không tìm thấy");
            }
            var hoadons = await _xuatHangHoaRepository.GetByIdUser(current);
            return Ok(hoadons);
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(HoaDonEdit hoaDonEdit)
        {
            var currentIduser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _xuatHangHoaRepository.Edit(currentIduser, hoaDonEdit);
            return Ok(result);
        }

    }
}
