using DDDTW.CoffeeShop.Infrastructures.EventSourcings;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.Repositories;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Commands;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using DDDTW.CoffeeShop.Inventories.WebAPI;
using DDDTW.CoffeeShop.Inventories.WebAPI.Models.RequestModels;
using DDDTW.CoffeeShop.Inventories.WebAPI.Models.Requests;
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

namespace DDDTW.CoffeeShop.Inventories.UnitTest.Inventories
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class ApiTests
    {
        private readonly List<Inventory> inventories = new List<Inventory>();
        private readonly WebApplicationFactory<Startup> factory;

        public ApiTests()
        {
            var repository = this.InventoryRepository();
            this.factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(config =>
                {
                    config.ConfigureServices(svc =>
                    {
                        svc.AddScoped<IInventoryRepository>(provider => repository);
                    });
                });
        }

        [Test]
        public async Task WhenGet()
        {
            var client = this.factory.CreateClient();
            var response = await client.GetAsync("api/Inventory?pageNo=1&pageSize=5");
            var result = JsonConvert.DeserializeObject<IEnumerable<Inventory>>(await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();
            result.Count().Should().Be(5);
        }

        [Test]
        public async Task WhenGetBy()
        {
            var id = this.inventories.First().Id;
            var client = this.factory.CreateClient();

            var response = await client.GetAsync($"api/Inventory/{id}");
            var result = JsonConvert.DeserializeObject<Inventory>(await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();
            result.Id.Should().Be(id);
        }

        [Test]
        public async Task WhenPost()
        {
            var client = this.factory.CreateClient();
            var response = await client.PostAsJsonAsync("api/Inventory", new AddInventoryReq
            {
                Qty = 10,
                Item = A.New<InventoryItemRM>(),
                Constraints = new[] { new InventoryConstraintRM()
                {
                    Type = InventoryConstraintType.MaxQty, Value = "10", DataTypeOfValue = TypeCode.Int32
                } }
            });

            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task WhenPut()
        {
            var id = this.inventories.First().Id;
            var client = this.factory.CreateClient();
            var response = await client.PutAsJsonAsync($"api/Inventory/{id}/qty", new ChangeQtyReq()
            {
                ActionMode = InventoryOperation.Inbound,
                Amount = 1
            });

            response.EnsureSuccessStatusCode();
        }

        private IInventoryRepository InventoryRepository()
        {
            var item = A.New<InventoryItem>();
            for (int i = 0; i < 10; i++)
            {
                CreateInventory cmd = new CreateInventory(new InventoryId(i, DateTimeOffset.Now), i, item, new[]
                {
                    new InventoryConstraint(InventoryConstraintType.MaxQty, "10", TypeCode.Int32),
                });
                this.inventories.Add(Inventory.Create(cmd));
            }

            IInventoryRepository repository = new InventoryRepository(new ESRepositoryBase<Inventory, InventoryId>());
            foreach (var inventory in this.inventories)
            {
                repository.Save(inventory);
            }

            return repository;
        }
    }
}