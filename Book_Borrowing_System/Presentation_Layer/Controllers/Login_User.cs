using Business_Layer.Book_Logic;
using Business_Layer.Login_Logic;
using Data_Access_Layer.DTO;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login_User : ControllerBase
    {
        private readonly IUserAuthentication _userAuthentication;
        private readonly IGetUserLogic _userService;

        public Login_User(IUserAuthentication userAuthentication, IGetUserLogic userService)
        {
            _userAuthentication = userAuthentication ?? throw new ArgumentNullException(nameof(userAuthentication));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("Login_User")]
        public async Task<IActionResult> Login([FromBody] User_Login_Dto userLoginDto)
        {
            try
            {
                var token = await _userAuthentication.AuthenticateAsync(userLoginDto);

                if (token == null)
                {
                    return Unauthorized("Invalid email or password.");
                }

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Register([FromBody] User_Register_Dto userRegisterDto)
        {
            try
            {
                var token = await _userAuthentication.RegisterAsync(userRegisterDto);

                if (token == null)
                {
                    return BadRequest("User registration failed.");
                }

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetUserDetailById/{id}")]
        public ActionResult<User> GetUserDetailsById(int id)
        {
            try
            {
                var userDetails = _userService.GetUserDetailsById(id);

                if (userDetails == null)
                {
                    return NotFound();
                }

                return Ok(userDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
