using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.AspNetCore.Mvc;
using EbookShop.Services;

namespace EbookShop.Web.Controllers.api
{
    
    
    [Authorize("ApiUser")]
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IUserService _userService;

        public DashboardController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Post()
        {
            try
            {
                var dto = await _userService.IsValidUserHTTP();
                return Ok(dto);
            }catch(InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }
   
    }
}