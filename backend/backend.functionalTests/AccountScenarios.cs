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

        [Test]
        public async Task Deposit_response_ok_status_code()
        {
            using (var server = CreateServer())
            {

                double expectedDeposit = 999.0;
                _accountRepository.Setup(s => s.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(new AccountModel()));
                _accountRepository.Setup(s => s.Update(It.IsAny<AccountModel>())).Returns(Task.FromResult(new AccountModel()));
                var content = new StringContent(BuildDepositCommand(), UTF8Encoding.UTF8, "application/json");
                var response = await server.CreateClient()
                    .PostAsync("account/deposit", content);
                string apiResponseStr = await response.Content.ReadAsStringAsync();
                AccountModel result = JsonConvert.DeserializeObject<AccountModel>(apiResponseStr);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(expectedDeposit, result.TotalMoney);
            }
        }

        [Test]
        public async Task Transfer_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                AccountModel fromAccount = new AccountModel();
                fromAccount.TotalMoney = 1000;
                AccountModel toAccount = new AccountModel();
                toAccount.TotalMoney = 0.0;
                _accountRepository.Setup(s => s.GetAsync(It.Is<string>(a => a == "test1"))).Returns(Task.FromResult(fromAccount));
                _accountRepository.Setup(s => s.GetAsync(It.Is<string>(a => a == "test2"))).Returns(Task.FromResult(toAccount));

                var content = new StringContent(BuildTransferCommand(), UTF8Encoding.UTF8, "application/json");
                var client = server.CreateClient();
                var response = await client
                    .PostAsync("account/transfer", content);
                string apiResponseStr = await response.Content.ReadAsStringAsync();
                AccountModel result = JsonConvert.DeserializeObject<AccountModel>(apiResponseStr);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(500.0, result.TotalMoney);

                var content2 = new StringContent(BuildGetCommand(), UTF8Encoding.UTF8, "application/json");
                var response2 = await client
                    .PostAsync("account/get", content2);
                string apiResponseStr2 = await response.Content.ReadAsStringAsync();
                AccountModel result2 = JsonConvert.DeserializeObject<AccountModel>(apiResponseStr2);
                Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode);
                Assert.AreEqual(500.0, result2.TotalMoney);

            }
        }


        string BuildGetCommand()
        {
            var order = new GetAccountCommand()
            {
                AccountId = "test1",
            };
            return JsonConvert.SerializeObject(order);
        }
        string BuildTransferCommand()
        {
            var order = new TransferCommand()
            {
                FromAccountId = "test1",
                ToAccountId = "test2",
                TransfertAmount = 500
            };
            return JsonConvert.SerializeObject(order);
        }

        string BuildDepositCommand()
        {
            var order = new DepositCommand()
            {
                AccountId = "test1",
                DepositAmount = 1000
            };
            return JsonConvert.SerializeObject(order);
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
}