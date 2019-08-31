using System;
using System.Threading;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.ApplicationServices;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Commands;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.QueryModels;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Inventory.Application.Inventories.DomainServices;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Interfaces;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace DDDTW.CoffeeShop.Inventory.UnitTest.Inventories
{
    [Parallelizable(ParallelScope.All)]
    public class ApplicationTests
    {
        private readonly InventoryId id;
        private readonly Domain.Inventories.Models.Inventory inventory;
        private readonly IInventoryRepository mockRepository;

        public ApplicationTests()
        {
            id = new InventoryId();
            inventory = new Domain.Inventories.Models.Inventory(id, 10, new InventoryItem(),
                new[] { new InventoryConstraint(InventoryConstraintType.MaxQty, "20", typeof(int).ToString()) });

            mockRepository = NSubstitute.Substitute.For<IInventoryRepository>();
            mockRepository.GenerateInventoryId().Returns(id);
        }

        [Test]
        public void GetInventory()
        {
            var cmd = new GetInventoryQry() { Id = $"inv-{DateTimeOffset.Now:yyyyMMdd}-0" };
            var expect = new InventoryVM() { Id = id.ToString(), Item = inventory.Item, Constraints = inventory.Constraint };
            mockRepository.GetBy(Arg.Any<InventoryId>()).Returns(inventory);

            var svc = new GetInventorySvc(new IdTranslator(), new InventoryVMTranslator(), mockRepository);
            var actual = svc.Handle(cmd, new CancellationToken()).Result;

            actual.Should().Be(expect);
        }

        [Test]
        public void AddInventory()
        {
            var item = new InventoryItem("name", "sku", 20, "manu", ItemCategory.Milk, "name", 10);
            var constraint = new InventoryConstraint(InventoryConstraintType.MaxQty, "10", typeof(int).ToString());
            var expect = new InventoryVM()
            {
                Id = $"inv-{DateTimeOffset.Now:yyyyMMdd}-0",
                Item = item,
                Constraints = new[] { constraint }
            };
            var cmd = new AddInventoryCmd() { Qty = 10, Item = item, Constraints = new[] { constraint } };

            var svc = new AddInventorySvc(new InventoryVMTranslator(), mockRepository);
            var actual = svc.Handle(cmd, new CancellationToken()).Result;

            actual.Should().Be(expect);
        }

        [Test]
        public void Inbound()
        {
            var result = new Domain.Inventories.Models.Inventory(id, 10, new InventoryItem(),
               new[] { new InventoryConstraint(InventoryConstraintType.MaxQty, "20", typeof(int).ToString()) });
            var repository = NSubstitute.Substitute.For<IInventoryRepository>();
            repository.GetBy(Arg.Any<InventoryId>()).Returns(result);
            var cmd = new InboundCmd() { Id = $"inv-{DateTimeOffset.Now:yyyyMMdd}-0", Amount = 10 };

            var svc = new InboundSvc(new IdTranslator(), repository);
            var actual = svc.Handle(cmd, new CancellationToken()).Result;

            actual.Qty.Should().Be(20);
        }

        [Test]
        public void Outbound()
        {
            var result = new Domain.Inventories.Models.Inventory(id, 10, new InventoryItem(),
                new[] { new InventoryConstraint(InventoryConstraintType.MaxQty, "20", typeof(int).ToString()) });
            var repository = NSubstitute.Substitute.For<IInventoryRepository>();
            repository.GetBy(Arg.Any<InventoryId>()).Returns(result);
            var cmd = new OutBoundCmd() { Id = $"inv-{DateTimeOffset.Now:yyyyMMdd}-0", Amount = 5 };

            var svc = new OutboundSvc(new IdTranslator(), repository);
            var actual = svc.Handle(cmd, new CancellationToken()).Result;

            actual.Qty.Should().Be(5);
        }
    }
}