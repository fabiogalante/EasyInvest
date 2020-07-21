using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using EasyInvest.Api.Client.Investimentos.Cliente;
using EasyInvest.Api.Client.Investimentos.Response.TesouroDireto;
using EasyInvest.Api.Client.Investimentos.Resposta.TesouroDireto;
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
       

        private readonly InvestmentHandler _sut;



        public InvestmentHandlerTest()
        {
            _sut = new InvestmentHandler(_mockTesouroDireto.Object, _mockLicis.Object, _mockFundos.Object, _mockCalculation.Object);

           

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
        }

        [Fact]
        public async Task Handle()
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

            var result =  await _sut.Handle(new InvestmentQuery(), CancellationToken.None);

            result.ValorTotal.Should().Be(1800);
            result.Should().BeOfType<InvestmentResponse>();
        }
    }
}