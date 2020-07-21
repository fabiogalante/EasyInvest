using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyInvest.Api.Client.Investimentos.Response.Lci
{
    public class LciResponse
    {
        [JsonProperty("lcis")]
        public List<Lcis> Lcis { get; set; }
    }
}
