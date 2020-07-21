using System;

namespace EasyInvest.Api.Client.Investimentos.Response.TesouroDireto
{
    public class TesouroDireto
    {
        public decimal ValorInvestido { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime DataDeCompra { get; set; }
        public int Iof { get; set; }
        public string Indice { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }

    }
}
