using System.Collections.Generic;

namespace EasyInvest.Investment.Application.UseCases.Investment.Responses
{
    public class InvestmentResponse
    {
        public decimal ValorTotal { get; set; }
        public List<Investimento> Investimentos { get; set; }
    }
}
