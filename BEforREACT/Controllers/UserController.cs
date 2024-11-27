using BEforREACT.Data.Entities;
using BEforREACT.DTOs;
using BEforREACT.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BEforREACT.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserServices _userServices;
        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRes user)
        {
            var token = await _userServices.LoginJwt(user.Email, user.Password);
            var userInfo = await _userServices.GetUserByEmail(user.Email);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { status = "error", message = "Email hoặc mật khẩu không chính xác." });
            }
            return Ok(new
            {
                id = userInfo.Id,
                token = token,
                status = "success",
                message = "Login success."
            });
        }



        [HttpGet("all")]
        [Authorize]
        public IActionResult GetAll()
        {
            var result = _userServices.GetUsers();
            return Ok(result);
        }
        [HttpGet("{Id}")]



        public async Task<IActionResult> GetByIdAsync(Guid Id)
        {


            var result = await _userServices.GetUserById(Id);

            if (result == null)
            {
                return NotFound(new { status = "error", message = "User not found." });
            }

            return Ok(result);
        }


        [HttpPost("register")]
        //[Authorize]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var result = await _userServices.Register(user);
            if (!result)
                return BadRequest(new { status = "error", message = "Email or Username already exist." });

            return Ok(new { status = "success", message = "Đăng ký thành công." });
        }

        [HttpPatch("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User user)
        {

            var updatedUser = await _userServices.UpdateUser(id, user);

            if (updatedUser == null)
            {
                return NotFound(new { status = "error", message = "User not found." });
            }

            return Ok(new { status = "success", message = "Cập nhật thông tin thành công." });
        }
    }




}


