# EASYINVEST - Custódias

## EasyInvest.Api.Client.Investimentos
Projeto onde são consumidos os endpoints, este tipo de projeto geralmente é criado como um Nuget dentro da corporação onde todos podem consumir, chamadas utilizando Refit

## API

1 - Entry point. (Memory cache)

WEB API - Controller

## Camadas

2 - APPLICATION

.NET STANDARD 

As regras encontram-se em InvestmentHandler, o projeto utiliza MediatorR com arquitetura Clean

## Testes

xUnit

Testes do Handler e regras de negócios, como cálculo de resgate.

```
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

```


## Hospedagem do projeto (Swagger)
http://easyinvest.azurewebsites.net/swagger/index.html (Removido)

## Monitoria(HealthCheck)
http://easyinvest.azurewebsites.net/check (Removido)
