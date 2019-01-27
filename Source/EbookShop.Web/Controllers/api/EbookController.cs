using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbookShop.Services.Dtos;
using EbookShop.Services.Ebooks.Queries.GetEbooksList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EbookShop.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EbookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EbookController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/Ebook?term=foo
        
        [HttpGet]
        public async Task<IActionResult>Get(string term)
        {


            var result = (term != null) ?
                await _mediator.Send(new GetEbooksListQuery()
                {
                    Filter = x => x.Title.IndexOf(term,StringComparison.OrdinalIgnoreCase) >= 0
                })
                :
                await _mediator.Send(new GetEbooksListQuery());
            
            return Ok(result); 
        }

        // GET: api/Ebook/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Ebook
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Ebook/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
