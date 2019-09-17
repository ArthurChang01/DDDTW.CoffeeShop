using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.ApplicationServices;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Messages;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Responses;
using DDDTW.CoffeeShop.Inventories.Application.Inventories.DomainServices;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Inventories.UnitTest.Inventories
{
    [Parallelizable(ParallelScope.All)]
    public class ServiceTests
    {
        private readonly InventoryId id;
        private readonly Inventory inventory;
        private readonly IInventoryRepository mockRepository;

        public ServiceTests()
        {
            id = new InventoryId();
            inventory = Inventory.Create(id, 10, new InventoryItem(),
                new[] { new InventoryConstraint(InventoryConstraintType.MaxQty, "20", TypeCode.Int32) });

            mockRepository = Substitute.For<IInventoryRepository>();
            mockRepository.GenerateInventoryId().Returns(id);
        }

        [Test]
        public async Task GetAllInventory()
        {
            var msg = new GetAllInventoryMsg(1, 1);
            var expect = new List<InventoryResp> { new InventoryResp(this.inventory) };
            this.mockRepository.Get(Arg.Any<Specification<Inventory>>(), 1, 1)
                .Returns(new List<Inventory>() { this.inventory });

            var svc = new GetAllInventorySvc(this.mockRepository);
            var actual = await svc.Handle(msg, new CancellationToken());

            actual.Should().BeEquivalentTo(expect.AsEnumerable());
        }

        [Test]
        public async Task GetInventory()
        {
            var msg = new GetInventoryMsg() { Id = this.id.ToString() };
            var expect = new InventoryResp(this.id.ToString(), 10, new InventoryItemResp(inventory.Item),
                inventory.Constraint.Select(o => new InventoryConstraintResp(o)));
            mockRepository.GetBy(Arg.Any<InventoryId>()).Returns(inventory);

            var svc = new GetInventorySvc(new IdTranslator(), mockRepository);
            var actual = await svc.Handle(msg, new CancellationToken());

            actual.Should().Be(expect);
        }

        [Test]
        public async Task AddInventory()
        {
            var item = new InventoryItemResp("name", "sku", 20, "manu", ItemCategory.Milk, "name", 10);
            var constraint = new InventoryConstraintResp(InventoryConstraintType.MaxQty, "10", TypeCode.Int32);
            var expect = new InventoryResp(this.id.ToString(), 10, item, new[] { constraint });
            var cmd = new AddInventoryMsg(10, item, new[] { constraint });
            var itemTranslator = new InventoryItemsTranslator();
            var constraintsTranslator = new InventoryConstrainsTranslator();

            var svc = new AddInventorySvc(itemTranslator, constraintsTranslator, mockRepository);
            var actual = await svc.Handle(cmd, new CancellationToken());

            actual.Should().Be(expect);
        }

        [Test]
        public async Task Inbound()
        {
            var result = Inventory.Create(id, 10, new InventoryItem(),
               new[] { new InventoryConstraint(InventoryConstraintType.MaxQty, "20", TypeCode.Int32) });
            var repository = NSubstitute.Substitute.For<IInventoryRepository>();
            repository.GetBy(Arg.Any<InventoryId>()).Returns(result);
            var cmd = new InboundMsg() { Id = $"inv-{DateTimeOffset.Now:yyyyMMdd}-0", Amount = 10 };

            var svc = new InboundSvc(new IdTranslator(), repository);
            var actual = await svc.Handle(cmd, new CancellationToken());

            actual.Qty.Should().Be(20);
        }

        [Test]
        public async Task Outbound()
        {
            var result = Inventory.Create(id, 10, new InventoryItem(),
                new[] { new InventoryConstraint(InventoryConstraintType.MaxQty, "20", TypeCode.Int32) });
            var repository = Substitute.For<IInventoryRepository>();
            repository.GetBy(Arg.Any<InventoryId>()).Returns(result);
            var cmd = new OutBoundMsg() { Id = $"inv-{DateTimeOffset.Now:yyyyMMdd}-0", Amount = 5 };

            var svc = new OutboundSvc(new IdTranslator(), repository);
            var actual = await svc.Handle(cmd, new CancellationToken());

            actual.Qty.Should().Be(5);
        }
    }
}