using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyInvest.Api.Client.Investimentos.Response.TesouroDireto
{
    public class TesouroDiretoResponse
    {
         [JsonProperty("tds")]
        public List<TesouroDireto> TesourosDireto { get; set; }
    }
}