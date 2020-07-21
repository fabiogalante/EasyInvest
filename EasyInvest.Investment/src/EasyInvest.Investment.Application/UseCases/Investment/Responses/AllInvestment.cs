using System;
using System.Collections.Generic;
using System.Text;
using EasyInvest.Api.Client.Investimentos.Resposta.Fundos;
using EasyInvest.Api.Client.Investimentos.Resposta.Lci;
using EasyInvest.Api.Client.Investimentos.Resposta.TesouroDireto;

namespace EasyInvest.Investment.Application.UseCases.Investment.Responses
{
    public class AllInvestment
    {
        public TesouroDiretoResposta TesouroDiretoResposta { get; set; }

        public LciResposta LciResposta { get; set; }

        public FundosResposta FundosResposta { get; set; }
    }
}
