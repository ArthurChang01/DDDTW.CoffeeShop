using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Infrastructures.Repositories.Mongos;
using DDDTW.CoffeeShop.Orders.Application.Products.DataContracts.Results;
using DDDTW.CoffeeShop.Orders.Application.Products.Repositories;
using DDDTW.CoffeeShop.Orders.Domain.Products.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using DDDTW.CoffeeShop.Orders.WebAPI;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Orders.UnitTest.Products
{
    [TestFixture()]
    public class ApiTests
    {
        private readonly WebApplicationFactory<Startup> factory;
        private readonly HttpClient client;
        private readonly List<Product> products = new List<Product>();
        private MongoRepositoryBase<Product, ProductId> mongoRepository;

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
                            svc.AddScoped(provider => this.GetAndRegisterProductRepository(provider).Result);
                        });
                });
            this.client = this.factory.CreateClient();
        }

        [Test]
        public async Task WhenGet()
        {
            var response = await this.client.GetAsync("api/Order/Product?pageNo=1&pageSize=5");
            var result =
                JsonConvert.DeserializeObject<IEnumerable<ProductRst>>(await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();
            result.Count().Should().Be(5);
        }

        [Test]
        public async Task WhenGetBy()
        {
            var productId = new ProductId();

            var response = await this.client.GetAsync($"api/Order/Product/{productId}");
            var result = JsonConvert.DeserializeObject<ProductRst>(await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();
            result.Id.Should().Be(productId.ToString());
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            this.factory.Dispose();
            this.client.Dispose();
            await this.mongoRepository.ClearCollection(nameof(Product));
        }

        private async Task<IProductRepository> GetAndRegisterProductRepository(IServiceProvider provider)
        {
            for (int i = 0; i < 10; i++)
            {
                this.products.Add(new Product(new ProductId(i), "name", "desc",
                    new ProductPrice(i + 10), ProductStatus.Active, DateTimeOffset.Now));
            }

            var saveTasks = new List<Task>();
            this.mongoRepository = (MongoRepositoryBase<Product, ProductId>)provider.GetService<IRepository<Product, ProductId>>();
            var repository = new ProductRepository(this.mongoRepository);
            foreach (var product in this.products)
            {
                saveTasks.Add(repository.Save(product));
            }

            await Task.WhenAll(saveTasks);

            return repository;
        }
    }
}