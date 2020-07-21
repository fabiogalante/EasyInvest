using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyInvest.Api.Client.Investimentos.Resposta.TesouroDireto
{
    public class TesouroDiretoResposta
    {
         [JsonProperty("tds")]
        public List<TesouroDireto> TesourosDireto { get; set; }
    }
}