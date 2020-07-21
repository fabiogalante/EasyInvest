using System;

namespace EasyInvest.Api.Client.Investimentos.Resposta.Fundos
{
    public class Fundos
    {
        public decimal CapitalInvestido { get; set; }
        public decimal ValorAtual { get; set; }
        public DateTime DataResgate { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal Iof { get; set; }
        public string Nome { get; set; }
        public decimal TotalTaxas { get; set; }
        public int Quantity { get; set; }
    }
}