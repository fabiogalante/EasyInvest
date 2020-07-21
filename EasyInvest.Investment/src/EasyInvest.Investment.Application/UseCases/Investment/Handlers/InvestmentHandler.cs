using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyInvest.Api.Client.Investimentos.Cliente;
using EasyInvest.Api.Client.Investimentos.Resposta.Fundos;
using EasyInvest.Api.Client.Investimentos.Resposta.Lci;
using EasyInvest.Api.Client.Investimentos.Resposta.TesouroDireto;
using EasyInvest.Investment.Application.UseCases.Investment.Queries;
using EasyInvest.Investment.Application.UseCases.Investment.Responses;
using MediatR;

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
                FundosResposta = fundos,
                LciResposta = rendaFixa,
                TesouroDiretoResposta = tesouroDireto
            });

            return result;
        }

        private async Task<FundosResposta> GetFundos()
        {
            return await _fundos.GetFundos();
        }

        private async Task<LciResposta> GetLcis()
        {
            return await _lcis.GetLcis();
        }


        private async Task<TesouroDiretoResposta> GetTesouro()
        {
            return await _tesouroDireto.GetTesouroDireto();
        }


        
    }
}
