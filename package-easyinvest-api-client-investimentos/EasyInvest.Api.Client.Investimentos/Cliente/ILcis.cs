using Refit;
using System.Threading.Tasks;
using EasyInvest.Api.Client.Investimentos.Response.Lci;

namespace EasyInvest.Api.Client.Investimentos.Cliente
{
    public interface ILcis
    {
        [Get("/5e3429a33000008c00d96336")]
        Task<LciResponse> GetLcis();
    }
}
