using EasyInvest.Api.Client.Investimentos.Cliente;
using EasyInvest.Api.Client.Investimentos.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using System;

namespace EasyInvest.Api.Client.Investimentos.Extensions
{
    public static class InvestmentServiceCollectionExtensions
    {
        public static void AddInvestClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<InvestmentSettings>(configuration.GetSection(nameof(InvestmentSettings)));
            var configs = services.BuildServiceProvider().GetRequiredService<IOptions<InvestmentSettings>>().Value;

            services.AddRefitClient<ITesouroDireto>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configs.Uri));

            services.AddRefitClient<ILcis>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configs.Uri));

            services.AddRefitClient<IFundos>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configs.Uri));

        }
    }
}
