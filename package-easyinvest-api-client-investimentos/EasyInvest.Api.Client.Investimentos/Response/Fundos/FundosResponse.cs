using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyInvest.Api.Client.Investimentos.Response.Fundos
{
    public class FundosResponse
    {
        [JsonProperty("fundos")]
        public List<Resposta.Fundos.Fundos> Fundos { get; set; }
    }
}
