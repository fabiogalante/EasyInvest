using EasyInvest.Api.Client.Investimentos.Cliente;
using EasyInvest.Api.Client.Investimentos.Response.Fundos;
using EasyInvest.Api.Client.Investimentos.Response.Lci;
using EasyInvest.Api.Client.Investimentos.Response.TesouroDireto;
using EasyInvest.Investment.Application.UseCases.Investment.Queries;
using EasyInvest.Investment.Application.UseCases.Investment.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EasyInvest.Investment.Application.UseCases.Investment.Handlers
{
    public class InvestmentHandler : IRequestHandler<InvestmentQuery, InvestmentResponse>
    {
        private readonly ITesouroDireto _tesouroDireto;
        private readonly ILcis _lcis;
        private readonly IFundos _fundos;
        private readonly ICalculation _calculation;

        public InvestmentHandler(ITesouroDireto tesouroDireto, ILcis lcis, IFundos fundos, ICalculation calculation)
        {
            _tesouroDireto = tesouroDireto;
            _lcis = lcis;
            _fundos = fundos;
            _calculation = calculation;
        }

        public async Task<InvestmentResponse> Handle(InvestmentQuery request, CancellationToken cancellationToken)
        {
            var tesouroDireto = await GetTesouro();

            var rendaFixa = await GetLcis();

            var fundos = await GetFundos();

            var result = _calculation.ExecuteAllInvestment(new AllInvestment
            {
                FundosResponse = fundos,
                LciResponse = rendaFixa,
                TesouroDiretoResponse = tesouroDireto
            });

            return result;
        }

        private async Task<FundosResponse> GetFundos()
        {
            return await _fundos.GetFundos();
        }

        private async Task<LciResponse> GetLcis()
        {
            return await _lcis.GetLcis();
        }

        private async Task<TesouroDiretoResponse> GetTesouro()
        {
            return await _tesouroDireto.GetTesouroDireto();
        }


        
    }
}
