using System.Collections.Generic;
using EasyInvest.Api.Client.Investimentos.Resposta.Lci;
using Newtonsoft.Json;

namespace EasyInvest.Api.Client.Investimentos.Response.Lci
{
    public class LciResponse
    {
        [JsonProperty("lcis")]
        public List<Lcis> Lcis { get; set; }
    }
}
