using FirzzApp.Business.Auth;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FrizzApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        /// var requestIP = HttpContext.Connection.RemoteIpAddress.ToString();


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            var authResponse = await _identityService.RegisterAsync(dto);

            return authResponse.Success
                ? Ok(new AuthSuccessResponse { Token = authResponse.Token })
                : BadRequest(new AuthFailureResponse { ErrorMessages = authResponse.ErrorMessages });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto request)
        {
            var authResponse = await _identityService.LoginAsync(request);

            return authResponse.Success
                ? Ok(new AuthSuccessResponse { Token = authResponse.Token })
                : BadRequest(new AuthFailureResponse { ErrorMessages = authResponse.ErrorMessages });
        }
    }
}
