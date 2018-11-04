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
    public class AccountsController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public AccountsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationDTO registrationDto)
        {
            //Validation of the model is done by using the ApiController tag. 
            // To do: add validation tags to DTO object? 

            // Catch exception thrown by registration service. 
            try
            {
             await _registrationService.RegisterWithStandardEmailAsync(registrationDto);
            }
            catch (InvalidOperationException e)
            {
                // Return 400 status code with error message
                return BadRequest(e.Message);
            }
            //To do: Return 201 status code with the link to the account profile.
            return Ok("Account created.");  
        }
    }
}