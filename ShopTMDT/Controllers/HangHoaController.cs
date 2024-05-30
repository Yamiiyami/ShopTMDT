using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopTMDT.services;
using ShopTMDT.ViewModel;

namespace ShopTMDT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        private readonly IHangHoaRepository _hangHoaRepository;
        public HangHoaController(IHangHoaRepository hangHoaRepository)
        {
            _hangHoaRepository = hangHoaRepository;
        }
        [AllowAnonymous]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var hanghoas = await _hangHoaRepository.Getall();


            return Ok(hanghoas);
        }
        [AllowAnonymous]
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hanghoa = await _hangHoaRepository.GetById(id);
            return Ok(hanghoa);
        }
        [AllowAnonymous]
        [HttpGet("GetByIdCate/{id}")]
        public async Task<IActionResult> GetByIdCate(int id)
        {
            var hanghoas = await _hangHoaRepository.GetByIdCate(id);
            return Ok(hanghoas);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm]HangHoaRequest hanghoa,List<IFormFile> files)
        {
            var result = await _hangHoaRepository.Create(hanghoa, files);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit(int id,HangHoaRequest hanghoa)
        {
            var result = await _hangHoaRepository.Edit(id, hanghoa);
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _hangHoaRepository.Remove(id);
            return Ok(result);
        }



    }
}
