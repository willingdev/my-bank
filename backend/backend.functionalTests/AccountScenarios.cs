using NUnit.Framework;
using Moq;
using MediatR;
using MyBank.Account.Domain.Aggregates;
using backend.Controllers;
using MyBank.Account.Application.Commands;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using MyBank.Backend;
using MyBank.Account.Domain.Models;

namespace backend.functionalTests
{
    public class AccountScenarios
    {
        private Mock<IAccountRepository> _accountRepository;

        public TestServer CreateServer()
        {
            _accountRepository = new Mock<IAccountRepository>();

            var path = Assembly.GetAssembly(typeof(AccountScenarios))
                .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables();
                }).UseStartup<Startup>()
                .ConfigureTestServices((services) =>
                    {
                        //Setup injection
                        services.AddSingleton<IAccountRepository>(_accountRepository.Object);

                    });

            var testServer = new TestServer(hostBuilder);
            return testServer;
        }
        [SetUp]
        public void Setup()
        {



        }

        [Test]
        public async Task Create_account_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                _accountRepository.Setup(s => s.Add(It.IsAny<AccountModel>())).Returns(Task.FromResult(new AccountModel()));
                var content = new StringContent(BuildCreateAccountCommand(), UTF8Encoding.UTF8, "application/json");
                var response = await server.CreateClient()
                    .PostAsync("account/create", content);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }
        string BuildCreateAccountCommand()
        {
            var order = new CreateAccountCommmand()
            {
                CustomerId = "test1"
            };
            return JsonConvert.SerializeObject(order);
        }
    }
    public static class WebHostBuilderExt
    {
        // public static WebHostBuilder ConfigureServicesTest(this WebHostBuilder @this, Action<IServiceCollection> configureServices)
        // {
        //     @this.ConfigureServices(services =>
        //         {
        //             configureServices(services);


        //         })
        //         .Configure(builder =>
        //         {

        //         });
        //     return @this;
        // }
    }
}