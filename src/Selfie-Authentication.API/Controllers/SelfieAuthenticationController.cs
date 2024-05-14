using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selfie_Authentication.API.Model;
using Selfie_Authentication.API.Services;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Selfie_Authentication.API.Controllers
{
    [Route("api/v1/selfie")]
    [ApiController]
    public class SelfieAuthenticationController : ControllerBase
    {
        private readonly ILogger<SelfieAuthenticationController> _logger;
        private readonly ISelfieAuthenticationService _selfieAuthenticationService;

        public SelfieAuthenticationController(ILogger<SelfieAuthenticationController> logger, ISelfieAuthenticationService selfieAuthenticationService)
        {
            _logger = logger;
            _selfieAuthenticationService = selfieAuthenticationService;                
        }


        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(RequestResult), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(RequestResult), (int)HttpStatusCode.BadRequest)]       
        [ProducesResponseType(typeof(RequestResult), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterSelfie([FromBody] string base64Image)
        {
            try
            {

                var result = await _selfieAuthenticationService.RegisterUserSelfie(base64Image);
               
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result);

                }
                return StatusCode(StatusCodes.Status201Created, result);

            }
            catch(Exception ex)
            {
                _logger.LogError($"An error occurred, See details {ex.Message}, {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, new RequestResult { code = "INTERNAL_SERVER_ERROR", message = "An unexpected error occurred", Succeeded = false });
            }
        }

        [HttpPost]
        [Route("authenticate")]
        [ProducesResponseType(typeof(RequestResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(RequestResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(RequestResult), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AuthenticateSelfie([FromBody] string base64Image)
        {
            try
            {

                var result = await _selfieAuthenticationService.AuthenticateUserSelfie(base64Image);

                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result);

                }
                return StatusCode(StatusCodes.Status200OK, result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred, See details {ex.Message}, {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, new RequestResult { code = "INTERNAL_SERVER_ERROR", message = "An unexpected error occurred", Succeeded = false });
            }
        }
    }
}
