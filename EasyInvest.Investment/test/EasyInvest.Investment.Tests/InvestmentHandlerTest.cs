using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using EasyInvest.Api.Client.Investimentos.Cliente;
using EasyInvest.Api.Client.Investimentos.Response.Fundos;
using EasyInvest.Api.Client.Investimentos.Response.Lci;
using EasyInvest.Api.Client.Investimentos.Response.TesouroDireto;
using EasyInvest.Api.Client.Investimentos.Resposta.Fundos;
using EasyInvest.Investment.Application.UseCases.Investment.Handlers;
using EasyInvest.Investment.Application.UseCases.Investment.Queries;
using EasyInvest.Investment.Application.UseCases.Investment.Responses;
using FluentAssertions;
using Moq;
using Xunit;

namespace EasyInvest.Investment.Tests
{
    public class InvestmentHandlerTest
    {
        private readonly Mock<ICalculation> _mockCalculation = new Mock<ICalculation>();
        private readonly Mock<ITesouroDireto> _mockTesouroDireto = new Mock<ITesouroDireto>();
        private readonly Mock<ILcis> _mockLicis = new Mock<ILcis>();
        private readonly Mock<IFundos> _mockFundos = new Mock<IFundos>();
        private readonly TesouroDiretoResponse _tesouroDireto;
        private readonly LciResponse _lciResponse;
        private readonly FundosResponse _fundosResponse;


        private readonly InvestmentHandler _sut;



        public InvestmentHandlerTest()
        {
            _sut = new InvestmentHandler(_mockTesouroDireto.Object, _mockLicis.Object, _mockFundos.Object, _mockCalculation.Object);

            _lciResponse = new LciResponse
            {
                Lcis = new List<Lcis>
                {
                    new Lcis
                    {
                        DataOperacao = Convert.ToDateTime("2015-03-01T00:00:00"),
                        CapitalInvestido = 10000m,
                        CapitalAtual = 12000m,
                        Nome = "Selic",
                        Vencimento = Convert.ToDateTime("2022-03-01T00:00:00"),
                    }
                }
            };

            _tesouroDireto = new TesouroDiretoResponse
            {
                TesourosDireto = new List<TesouroDireto>
                {
                    new TesouroDireto
                    {
                        DataDeCompra = Convert.ToDateTime("2015-03-01T00:00:00"),
                        ValorInvestido = 10000m,
                        ValorTotal = 12000m,
                        Nome = "Selic",
                        Vencimento = Convert.ToDateTime("2022-03-01T00:00:00"),
                    }
                }
            };


            _fundosResponse = new FundosResponse
            {
                Fundos = new List<Fundos>
                {
                    new Fundos
                    {
                        DataCompra = Convert.ToDateTime("2015-03-01T00:00:00"),
                        CapitalInvestido = 10000m,
                        ValorAtual = 12000m,
                        Nome = "Selic",
                        DataResgate = Convert.ToDateTime("2022-03-01T00:00:00"),
                    }
                }
            };
        }

        [Fact]
        public async Task Handle_Execute_Investment()
        {
            // Arrange

            var investMents = new Faker<Application.UseCases.Investment.Responses.Investment>("pt_BR")
                .RuleFor(o => o.ValorTotal, f => f.Finance.Amount())
                .RuleFor(o => o.Nome, f => f.Finance.TransactionType())
                .RuleFor(o => o.Vencimento, f => f.Date.Future(5))
                .RuleFor(o => o.ValorResgate, f => f.Finance.Amount());


            var response = new Faker<InvestmentResponse>("pt_BR")
                .RuleFor(c => c.ValorTotal, 1800)
                .RuleFor(c => c.Investimentos, investMents.Generate(3).ToList());
                

            _mockCalculation.Setup(c => c.ExecuteAllInvestment(It.IsAny<AllInvestment>())).Returns(response);
            _mockTesouroDireto.Setup(t => t.GetTesouroDireto()).ReturnsAsync(_tesouroDireto);
            _mockFundos.Setup(t => t.GetFundos()).ReturnsAsync(_fundosResponse);
            _mockLicis.Setup(t => t.GetLcis()).ReturnsAsync(_lciResponse);
            var result =  await _sut.Handle(new InvestmentQuery(), CancellationToken.None);

            result.ValorTotal.Should().Be(1800);
            result.Should().BeOfType<InvestmentResponse>();
        }
    }
}