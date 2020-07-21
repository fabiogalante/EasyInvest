using EasyInvest.Investment.Application.UseCases.Investment.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetInvestment()
        {
            var query = new InvestmentQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
