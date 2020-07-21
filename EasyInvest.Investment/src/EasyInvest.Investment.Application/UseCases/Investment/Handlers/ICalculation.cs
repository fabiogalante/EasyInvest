using EasyInvest.Investment.Application.UseCases.Investment.Responses;
using System;

namespace EasyInvest.Investment.Application.UseCases.Investment.Handlers
{
    public interface ICalculation
    {
        decimal CalculationRescue(DateTime dueDate, DateTime purchaseDate, DateTime today, decimal amount);

        InvestmentResponse ExecuteAllInvestment(AllInvestment allInvestment);
    }
}