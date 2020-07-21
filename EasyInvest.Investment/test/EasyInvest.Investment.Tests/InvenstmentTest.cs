using System;
using EasyInvest.Api.Client.Investimentos.Cliente;
using EasyInvest.Api.Client.Investimentos.Resposta.TesouroDireto;
using EasyInvest.Investment.Application.UseCases.Investment.Handlers;
using FluentAssertions;
using Moq;
using Xunit;

namespace EasyInvest.Investment.Tests
{
    //public class InvestmentHandlerTest
    //{
    //    private Mock<ICalculation> _calculation;
    //    private readonly Mock<ITesouroDireto> _mockTesouroDireto;
    //    private readonly Mock<ILcis> _mockLicis;
    //    private readonly Mock<IFundos> _mockFundos;

    //    private readonly InvestmentHandler _sut;

    //    public InvestmentHandlerTest()
    //    {
    //        _sut = new InvestmentHandler(_mockTesouroDireto.Object, _mockLicis.Object, _mockFundos.Object, _calculation.Object);
    //    }

    //    [Fact]
    //    public void CalculationRescue_GetMonth()
    //    {
    //    }
    //}


    public class CalculationTest
    {
        private ICalculation _sut;
        private readonly Mock<ITesouroDireto> _mockTesouroDireto;
        private readonly Mock<ILcis> _mockLicis;
        private readonly Mock<IFundos> _mockFundos;

        public CalculationTest()
        {
           _sut = new Calculation();
        }



       [Fact]
        public void CalculationRescue_ThreeMonth_CanReturnCalcTreeMonth()
        {
            //Arrange
            var dueData = Convert.ToDateTime("2025-12-31T00:00:00");
            var purchase = Convert.ToDateTime("2025-10-18T00:00:00");
            var today = Convert.ToDateTime("2025-10-18T00:00:00");
            var amount = 1000;

            var result = _sut.CalculationRescue(dueData, purchase, today, amount);


            //Asset
            result.Should().Be(940);
        }

        [Fact]
        public void CalculationRescue_HalfOftheTime_CanReturnHalf()
        {
            //Arrange
            var dueData = Convert.ToDateTime("2025-12-31T00:00:00");
            var purchase = Convert.ToDateTime("2010-10-10T00:00:00");
            var today = Convert.ToDateTime("2020-07-20T00:00:00");
            var amount = 1000;

            var result = _sut.CalculationRescue(dueData, purchase, today, amount);


            //Asset
            result.Should().Be(850);
        }


        [Fact]
        public void CalculationRescue_Example_CanReturnHalf()
        {
            //Arrange
            var dueData = Convert.ToDateTime("2025-03-01T00:00:00");
            var purchase = Convert.ToDateTime("2015-03-01T00:00:00");
            var today = Convert.ToDateTime("2020-07-20T00:00:00");
            var amount = 829.68m;

            var result = _sut.CalculationRescue(dueData, purchase, today, amount);


            //Asset
            result.Should().Be(705.228m);
        }


        [Fact]
        public void CalculationRescue_Others_Can30()
        {
            //Arrange
            var dueData = Convert.ToDateTime("2025-10-10T00:00:00");
            var purchase = Convert.ToDateTime("2010-10-10T00:00:00");
            var today = Convert.ToDateTime("2012-09-20T00:00:00");
            var amount = 1000;

            var result = _sut.CalculationRescue(dueData, purchase, today, amount);


            //Asset
            result.Should().Be(700);
        }
    }
}

