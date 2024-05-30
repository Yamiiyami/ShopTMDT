using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopTMDT.services;
using ShopTMDT.ViewModel;

namespace ShopTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Admin")]
    public class SlideController : ControllerBase
    {
        //khai baos ddeer su dung ISlideRepository trong controller
        private readonly ISlideRepository _slideRepository;
        public SlideController(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var slide = _slideRepository.GetAll();
            return Ok(slide);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id) 
        {
            var slide = _slideRepository.GetByiD(id);
            return Ok(slide);
        }

        [HttpPost("Create")]
        public IActionResult Create(SlideVM slideVM)
        {
            var result = _slideRepository.Add(slideVM);
            return Ok(result);
        }

        [HttpPut("Edit")]
        public IActionResult Edit(SlideMD slideMD)
        {
            var result = _slideRepository.Edit(slideMD);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var result = _slideRepository.Delete(id);
            return Ok(result);
        }

    }
}
