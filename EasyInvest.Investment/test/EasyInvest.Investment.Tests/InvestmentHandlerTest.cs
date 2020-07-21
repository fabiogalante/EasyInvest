using System.Threading;
using EasyInvest.Api.Client.Investimentos.Cliente;
using EasyInvest.Investment.Application.UseCases.Investment.Handlers;
using EasyInvest.Investment.Application.UseCases.Investment.Queries;
using Moq;
using Xunit;

namespace EasyInvest.Investment.Tests
{
    public class InvestmentHandlerTest
    {
        private Mock<ICalculation> _calculation;
        private readonly Mock<ITesouroDireto> _mockTesouroDireto;
        private readonly Mock<ILcis> _mockLicis;
        private readonly Mock<IFundos> _mockFundos;

        private readonly InvestmentHandler _sut;

        public InvestmentHandlerTest()
        {
            _sut = new InvestmentHandler(_mockTesouroDireto.Object, _mockLicis.Object, _mockFundos.Object, _calculation.Object);
        }

        [Fact]
        public void CalculationRescue_GetMonth()
        {
            //_mockTesouroDireto.Setup(t => t.GetTesouroDireto()).ReturnsAsync()
            //var result = _sut.Handle(new InvestmentQuery(), CancellationToken.None);
        }
    }
}