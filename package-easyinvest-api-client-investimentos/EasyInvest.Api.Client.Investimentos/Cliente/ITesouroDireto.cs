using EasyInvest.Api.Client.Investimentos.Response.TesouroDireto;
using Refit;
using System.Threading.Tasks;

namespace EasyInvest.Api.Client.Investimentos.Cliente
{
    public interface ITesouroDireto
    {
        [Get("/5e3428203000006b00d9632a")]
        Task<TesouroDiretoResponse> GetTesouroDireto();
    }
}
