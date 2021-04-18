using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserImagesController : ControllerBase
    {
        private readonly IUserImageService _userImageService;

        public UserImagesController(IUserImageService userImageService)
        {
            _userImageService = userImageService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _userImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _userImageService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyuserid")]
        public IActionResult GetByUserId(int id)
        {
            var result = _userImageService.GetByUserId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var userImage = _userImageService.GetById(id).Data;
            var result = _userImageService.Delete(userImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("update")]
        [HttpPost("add")]
        public IActionResult Update([FromForm] UserImage userImage, [FromForm(Name = "UserImage")] IFormFile formFile)
        {
            var result = _userImageService.Update(userImage, formFile);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
