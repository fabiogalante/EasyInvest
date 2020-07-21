using EasyInvest.Api.Client.Investimentos.Response.Fundos;
using EasyInvest.Api.Client.Investimentos.Response.Lci;
using EasyInvest.Api.Client.Investimentos.Response.TesouroDireto;

namespace EasyInvest.Investment.Application.UseCases.Investment.Responses
{
    public class AllInvestment
    {
        public TesouroDiretoResponse TesouroDiretoResponse { get; set; }

        public LciResponse LciResponse { get; set; }

        public FundosResponse FundosResponse { get; set; }
    }
}
