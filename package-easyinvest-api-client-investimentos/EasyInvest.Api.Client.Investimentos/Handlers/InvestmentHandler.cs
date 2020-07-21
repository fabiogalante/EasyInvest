using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyInvest.Api.Client.Investimentos.Cliente;
using EasyInvest.Api.Client.Investimentos.Settings;
using Microsoft.Extensions.Options;

namespace EasyInvest.Api.Client.Investimentos.Handlers
{
    public class InvestmentHandler : DelegatingHandler
    {
        private readonly IDistributedCache _cache;
        private readonly IFundos _fundos;
        private readonly Settings.InvestmentSettings _investmentSettings;

        const string InvestmentKey = "investment_key";

        public InvestmentHandler(IDistributedCache cache, IFundos fundos, IOptions<InvestmentSettings> settings)
        {
            _cache = cache;
            _fundos = fundos;
            _investmentSettings = settings.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var tokenInCache = await _cache.GetStringAsync(InvestmentKey, cancellationToken);

            var fd = _fundos.GetFundos();

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
