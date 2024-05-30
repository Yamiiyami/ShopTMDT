using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ShopTMDT.services;
using ShopTMDT.ViewModel;
using System.Net.WebSockets;
using System.Security.Claims;

namespace ShopTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;
        public RatingController(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var rating = _ratingRepository.GetAll();
            return Ok(rating);
        }

        [HttpGet("getbyidhanghoa")]
        public IActionResult GetById(int idHanghoa)
        {
            var rating = _ratingRepository.GetByiD(idHanghoa);
            return Ok(rating);
        }

        [HttpPost("create")]

        public IActionResult Create(RatingVM ratingVM)
        {
            var result = _ratingRepository.Add(ratingVM);
            return Ok(result);
        }

        [HttpPut("edit")]
        
        public IActionResult Edit( RatingRepuest ratingMD)
        {
            
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(currentUserId == null)
            {
                return NotFound("Không tìm thấy User");
            }
            var result = _ratingRepository.Edit(currentUserId, ratingMD);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int idrating)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null)
            {
                return NotFound("Không tìm thấy User");
            }
            var resulr = _ratingRepository.Delete(idrating, currentUserId);
            return Ok(resulr);
        }
    }
}
