using DDDTW.CoffeeShop.Infrastructures.EventSourcings;
using DDDTW.CoffeeShop.Orders.Application.Orders.Repositories;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Commands;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using DDDTW.CoffeeShop.Orders.WebAPI;
using DDDTW.CoffeeShop.Orders.WebAPI.Models.RequestModels;
using DDDTW.CoffeeShop.Orders.WebAPI.Models.Requests;
using FluentAssertions;
using GenFu;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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

        public ApiTests()
        {
            var repository = OrderRepository();

            this.factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(config =>
                {
                    config.ConfigureServices(svc =>
                    {
                        svc.AddScoped<IOrderRepository>(provider => repository);
                    });
                });
        }

        [Test]
        public async Task WhenGet()
        {
            var client = this.factory.CreateClient();
            var response = await client.GetAsync("api/Order?pageNo=1&pageSize=5");
            var result = JsonConvert.DeserializeObject<IEnumerable<Order>>(await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();
            result.Count().Should().Be(5);
        }

        [Test]
        public async Task WhenGetBy()
        {
            var orderId = this.orders.First().Id;
            var client = this.factory.CreateClient();

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
                    new OrderItemRM() {Product = new ProductRM() {Id = "11", Name = "Prod"}, Qty = 11, Price = 11},
                }
            });

            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task WhenPatchItem()
        {
            var orderId = this.orders.First().Id;
            var client = this.factory.CreateClient();
            var response = await client.PutAsJsonAsync($"api/Order/{orderId}/orderItems",
               new ChangeOrderItemReq()
               {
                   OrderItems = new List<OrderItemRM>()
               {
                   new OrderItemRM() {Product = new ProductRM() { Id = "0", Name = "pp"}, Qty = 10, Price = 10}
               }
               });

            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task WhenPatchStatus()
        {
            var orderId = this.orders.First().Id;
            var client = this.factory.CreateClient();
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
            var orderId = this.orders.First().Id;
            var client = this.factory.CreateClient();
            var response = await client.DeleteAsync($"api/Order/{orderId}");

            response.EnsureSuccessStatusCode();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            this.factory.Dispose();
        }

        private IOrderRepository OrderRepository()
        {
            for (int i = 0; i < 10; i++)
            {
                CreateOrder cmd = new CreateOrder(new OrderId(i, DateTimeOffset.Now), i.ToString(), OrderStatus.Initial,
                    A.ListOf<OrderItem>(5));
                orders.Add(Order.Create(cmd));
            }

            IOrderRepository repository = new OrderRepository(new ESRepositoryBase<Order, OrderId>());
            foreach (var order in orders)
            {
                repository.Save(order);
            }

            return repository;
        }
    }
}