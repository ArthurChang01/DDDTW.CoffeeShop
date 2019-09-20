using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Orders.Application.Orders.Repositories;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Commands;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using DDDTW.CoffeeShop.Orders.WebAPI;
using DDDTW.CoffeeShop.Orders.WebAPI.Models.Orders.RequestModels;
using DDDTW.CoffeeShop.Orders.WebAPI.Models.Orders.Requests;
using FluentAssertions;
using GenFu;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Order = DDDTW.CoffeeShop.Orders.Domain.Orders.Models.Order;

namespace DDDTW.CoffeeShop.Orders.UnitTest.Orders
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class ApiTests
    {
        private readonly List<Order> orders = new List<Order>();
        private readonly WebApplicationFactory<Startup> factory;
        private readonly HttpClient client;

        public ApiTests()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            this.factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(config =>
            {
                config.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(configPath);
                });

                config.ConfigureServices(svc =>
                {
                    svc.AddScoped<IOrderRepository>(provider => this.GetAndRegisterOrderRepository(provider).Result);
                });
            });
            this.client = this.factory.CreateClient();
        }

        [Test]
        public async Task WhenGet()
        {
            var response = await client.GetAsync("api/Order?pageNo=1&pageSize=5");
            var result = JsonConvert.DeserializeObject<IEnumerable<Order>>(await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();
            result.Count().Should().Be(5);
        }

        [Test]
        public async Task WhenGetBy()
        {
            var orderId = this.orders.First().Id;

            var response = await client.GetAsync($"api/Order/{orderId}");
            var result = JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();
            result.Id.Should().Be(orderId);
        }

        [Test]
        public async Task WhenPost()
        {
            var client = this.factory.CreateClient();
            var response = await client.PostAsJsonAsync("api/Order", new AddOrderReq()
            {
                Items = new[]
                {
                    new OrderItemRM() {ProductId = $"prd-{DateTimeOffset.Now:yyyyMMdd}-3", Qty = 11, Price = 11},
                }
            });

            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task WhenPatchItem()
        {
            var orderId = this.orders.First().Id;
            var response = await client.PutAsJsonAsync($"api/Order/{orderId}/orderItems",
               new ChangeOrderItemReq()
               {
                   Items = new List<OrderItemRM>()
               {
                   new OrderItemRM() {ProductId = $"prd-{DateTimeOffset.Now:yyyyMMdd}-3", Qty = 10, Price = 10}
               }
               });

            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task WhenPatchStatus()
        {
            var orderId = this.orders.First().Id;
            var response = await client.PutAsJsonAsync($"api/Order/{orderId}/status",
                new ChangeStatusReq()
                {
                    OrderStatus = OrderStatus.Processing
                });

            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task WhenDelete()
        {
            var orderId = new OrderId(0, DateTimeOffset.Now);
            var response = await client.DeleteAsync($"api/Order/{orderId}");

            response.EnsureSuccessStatusCode();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            this.factory.Dispose();
            this.client.Dispose();
        }

        private async Task<IOrderRepository> GetAndRegisterOrderRepository(IServiceProvider provider)
        {
            for (int i = 0; i < 10; i++)
            {
                CreateOrder cmd = new CreateOrder(new OrderId(i, DateTimeOffset.Now), i.ToString(), OrderStatus.Initial,
                    A.ListOf<OrderItem>(5));
                orders.Add(Order.Create(cmd));
            }

            List<Task> saveTasks = new List<Task>();
            var repository = new OrderRepository(provider.GetService<IRepository<Order, OrderId>>());
            foreach (var order in this.orders)
            {
                saveTasks.Add(repository.Save(order));
            }

            await Task.WhenAll(saveTasks);

            return repository;
        }
    }
}