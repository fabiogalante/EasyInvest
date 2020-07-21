using EasyInvest.Investment.Application.UseCases.Investment.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace EasyInvest.Investment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvestmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvestmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetInvestment([FromServices] IMemoryCache cache)
        {
            var cacheEntry = await
                cache.GetOrCreateAsync("keyInvestment", entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6); 
                    var query = new InvestmentQuery();
                    return  _mediator.Send(query);
                });

            return Ok(cacheEntry);
        }
    }
}
