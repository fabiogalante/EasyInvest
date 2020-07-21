using EasyInvest.Api.Client.Investimentos.Extensions;
using EasyInvest.Api.Client.Investimentos.Settings;
using EasyInvest.Investment.Application;
using EasyInvest.Investment.Application.UseCases.Investment.Handlers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EasyInvest.Investment.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddInvestClient(Configuration);
            services.AddMediatR(typeof(Result));
            services.AddTransient<ICalculation, Calculation>();
            services.Configure<InvestmentSettings>(Configuration.GetSection(nameof(InvestmentSettings)));

            var uri = Configuration.GetSection("RedisCache:Host").Get<string>();

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = uri;
                options.InstanceName = Configuration.GetSection("RedisCache:InstanceName").Get<string>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
