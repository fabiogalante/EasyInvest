﻿using System;
using System.Collections.Generic;
using System.Linq;
using EasyInvest.Api.Client.Investimentos.Resposta.Fundos;
using EasyInvest.Api.Client.Investimentos.Resposta.Lci;
using EasyInvest.Api.Client.Investimentos.Resposta.TesouroDireto;
using EasyInvest.Investment.Application.UseCases.Investment.Responses;

namespace EasyInvest.Investment.Application.UseCases.Investment.Handlers
{
    public class Calculation : ICalculation
    {
        readonly List<Investimento> _investments = new List<Investimento>();

        


        public InvestmentResponse ExecuteAllInvestment(AllInvestment allInvestment)
        {
            var result = new InvestmentResponse();

            GetTotalInvestment(allInvestment, result);

            GetInvestments(allInvestment, result);

            return result;
        }

        private static void GetTotalInvestment(AllInvestment allInvestment, InvestmentResponse result)
        {
            var rendaFixaValorInvestido = allInvestment.LciResposta.Lcis.Select(v => v.CapitalInvestido).Sum();

            var tesouroValorInvestido =
                allInvestment.TesouroDiretoResposta.TesourosDireto.Select(t => t.ValorInvestido).Sum();

            var fundosValorInvestido = allInvestment.FundosResposta.Fundos.Select(f => f.CapitalInvestido).Sum();

            result.ValorTotal = rendaFixaValorInvestido + tesouroValorInvestido + fundosValorInvestido;
        }

        private void GetInvestments(AllInvestment allInvestment, InvestmentResponse result)
        {

            foreach (var tesouroDireto in allInvestment.TesouroDiretoResposta.TesourosDireto)
            {
                var investment = new Investimento();
                CalculationTesouro(investment, tesouroDireto);

                _investments.Add(investment);
            }

            foreach (var fundos in allInvestment.FundosResposta.Fundos)
            {
                var investment = new Investimento();
                CalculationFundos(investment, fundos);

                _investments.Add(investment);
            }


            foreach (var rendaFixa in allInvestment.LciResposta.Lcis)
            {
                var investment = new Investimento();
                CalculationRendaFixa(investment, rendaFixa);

                _investments.Add(investment);
            }

            result.Investimentos = _investments;
        }

        public void CalculationTesouro(Investimento investment, TesouroDireto tesouroDireto)
        {
            const decimal discountPercentage = 0.1m;

            investment.Nome = tesouroDireto.Nome;
            investment.ValorInvestido = tesouroDireto.ValorInvestido;
            investment.ValorTotal = tesouroDireto.ValorTotal;
            investment.Vencimento = tesouroDireto.Vencimento;
            investment.Ir = (tesouroDireto.ValorTotal - tesouroDireto.ValorInvestido) * discountPercentage;
            investment.ValorResgate = CalculationRescue(tesouroDireto.Vencimento, tesouroDireto.DataDeCompra, DateTime.Today, tesouroDireto.ValorTotal);
        }

        private void CalculationFundos(Investimento investment, Fundos fundos)
        {
            const decimal discountPercentage = 0.15m;

            investment.Nome = fundos.Nome;
            investment.ValorInvestido = fundos.CapitalInvestido;
            investment.ValorTotal = fundos.ValorAtual;
            investment.Vencimento = fundos.DataResgate;
            investment.Ir = (fundos.ValorAtual - fundos.CapitalInvestido) * discountPercentage;
            investment.ValorResgate = CalculationRescue(fundos.DataResgate, fundos.DataCompra, DateTime.Today, fundos.ValorAtual);

        }


        private void CalculationRendaFixa(Investimento investment, Lcis lcis)
        {
            const decimal discountPercentage = 0.05m;

            investment.Nome = lcis.Nome;
            investment.ValorInvestido = lcis.CapitalInvestido;
            investment.ValorTotal = lcis.CapitalAtual;
            investment.Vencimento = lcis.Vencimento;
            investment.Ir = (lcis.CapitalAtual - lcis.CapitalInvestido) * discountPercentage;
            investment.ValorResgate = CalculationRescue(lcis.Vencimento, lcis.DataOperacao, DateTime.Today, lcis.CapitalAtual);

        }

        public decimal CalculationRescue(DateTime dueDate, DateTime purchaseDate, DateTime today, decimal amount)
        {
            var threeMonths = (dueDate - today);
                if (threeMonths.Days <= 90)
                return amount * 0.94m;


            var halfOftheTime = (dueDate - purchaseDate);
            var halfOftheToday = (dueDate - today);
            if (halfOftheToday.Days < (halfOftheTime.Days / 2)) 
                return amount * 0.85m;

            return amount * 0.70m;
        }



    }
}