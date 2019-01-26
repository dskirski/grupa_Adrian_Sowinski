using System;
using System.Threading.Tasks;
using EbookShop.Services;
using EbookShop.Services.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EbookShop.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login(CredentialsDto credentialsDto, [FromServices]IAuthenticationService authenticationService)
        {
            try
            {
                // Returns JWT if credentials are valid otherwise throws InvalidOperationException
                var jwt = await authenticationService.Authenticate(credentialsDto);
                return Ok(jwt);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}