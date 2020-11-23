using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyBank.Account.Application.Commands;
using MyBank.Account.Domain.Aggregates;
using MyBank.Account.Infrastructure;

namespace MyBank.Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddMediatR(typeof(CreateAccountCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetAccountCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DepositCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(TransferCommandHandler).GetTypeInfo().Assembly);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
