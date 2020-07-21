using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyInvest.Api.Client.Investimentos.Resposta.Fundos
{
    public class FundosResposta
    {
        [JsonProperty("fundos")]
        public List<Fundos> Fundos { get; set; }
    }
}
