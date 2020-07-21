using System.Threading.Tasks;
using EasyInvest.Api.Client.Investimentos.Response.TesouroDireto;
using EasyInvest.Api.Client.Investimentos.Resposta.TesouroDireto;
using Refit;

namespace EasyInvest.Api.Client.Investimentos.Cliente
{
    public interface ITesouroDireto
    {
        [Get("/5e3428203000006b00d9632a")]
        Task<TesouroDiretoResponse> GetTesouroDireto();
    }
}
