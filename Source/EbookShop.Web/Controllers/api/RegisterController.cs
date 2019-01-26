using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EbookShop.Services.Dtos;
using EbookShop.Services;
using System.Net;

namespace EbookShop.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(RegistrationDto registrationDto, [FromServices] IRegistrationService registrationService)
        {
            //Validation of the model is done by using the ApiController tag.
            // Catch exception thrown by registration service. 
            try
            {
                var token = await registrationService.RegisterAsync(registrationDto);
                return Created("default", token);
            }
            catch (InvalidOperationException e)
            {
                // Return 400 status code with error message
                return BadRequest(e.Message);
            }
         
        }
    }
}