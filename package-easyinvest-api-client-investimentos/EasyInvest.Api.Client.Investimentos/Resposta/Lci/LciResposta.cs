using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyInvest.Api.Client.Investimentos.Resposta.Lci
{
    public class LciResposta
    {
        [JsonProperty("lcis")]
        public List<Lcis> Lcis { get; set; }
    }
}
