using EasyInvest.Api.Client.Investimentos.Resposta.Fundos;
using Refit;
using System.Threading.Tasks;
using EasyInvest.Api.Client.Investimentos.Response.Fundos;

namespace EasyInvest.Api.Client.Investimentos.Cliente
{
    public interface IFundos
    {
        [Get("/5e342ab33000008c00d96342")]
        Task<FundosResponse> GetFundos();
    }
}
