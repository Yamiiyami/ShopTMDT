using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopTMDT.services;
using ShopTMDT.ViewModel;

namespace ShopTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiHangHoaController : ControllerBase
    {
        private readonly ILoaiHangHoaRepository _loaiHangHoaRepository;
        public LoaiHangHoaController(ILoaiHangHoaRepository loaiHangHoaRepository) 
        {
            _loaiHangHoaRepository = loaiHangHoaRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var lhh = await _loaiHangHoaRepository.GetAll();
            return Ok(lhh);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var lhh = await _loaiHangHoaRepository.GetById(id);
            return Ok(lhh);
        }
        [Authorize("Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(LoaihangHoaVM loaihanghoa)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _loaiHangHoaRepository.Create(loaihanghoa);
            return Ok(result);
        }
        [Authorize("Admin")]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit(LoaiHangHoaResponse loaihanghoa)
        {
            var result = await _loaiHangHoaRepository.Edit(loaihanghoa);
            return Ok(result);
        }
        [Authorize("Admin")]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _loaiHangHoaRepository.Remove(id);
            return Ok(result);
        }
    }
}
