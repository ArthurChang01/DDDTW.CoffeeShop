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
using System;
using System.Linq;
using System.Threading;

namespace DDDTW.CoffeeShop.Inventory.UnitTest.Inventories
{
    [Parallelizable(ParallelScope.All)]
    public class ServiceTests
    {
        private readonly InventoryId id;
        private readonly Domain.Inventories.Models.Inventory inventory;
        private readonly IInventoryRepository mockRepository;

        public ServiceTests()
        {
            id = new InventoryId();
            inventory = new Domain.Inventories.Models.Inventory(id, 10, new InventoryItem(),
                new[] { new InventoryConstraint(InventoryConstraintType.MaxQty, "20", TypeCode.Int32) });

            mockRepository = NSubstitute.Substitute.For<IInventoryRepository>();
            mockRepository.GenerateInventoryId().Returns(id);
        }

        [Test]
        public void GetInventory()
        {
            var cmd = new GetInventoryQry() { Id = $"inv-{DateTimeOffset.Now:yyyyMMdd}-0" };
            var expect = new InventoryVM()
            {
                Id = id.ToString(),
                Qty = 10,
                Item = new InventoryItemVM(inventory.Item),
                Constraints = inventory.Constraint.Select(o => new InventoryConstraintVM(o))
            };
            mockRepository.GetBy(Arg.Any<InventoryId>()).Returns(inventory);

            var svc = new GetInventorySvc(new IdTranslator(), new InventoryVMTranslator(), mockRepository);
            var actual = svc.Handle(cmd, new CancellationToken()).Result;

            actual.Should().Be(expect);
        }

        [Test]
        public void AddInventory()
        {
            var item = new InventoryItemVM("name", "sku", 20, "manu", ItemCategory.Milk, "name", 10);
            var constraint = new InventoryConstraintVM(InventoryConstraintType.MaxQty, "10", TypeCode.Int32);
            var expect = new InventoryVM()
            {
                Id = $"inv-{DateTimeOffset.Now:yyyyMMdd}-0",
                Qty = 10,
                Item = item,
                Constraints = new[] { constraint }
            };
            var cmd = new AddInventoryCmd() { Qty = 10, Item = item, Constraints = new[] { constraint } };
            var itemTranslator = new InventoryItemsTranslator();
            var constraintsTranslator = new InventoryConstrainsTranslator();
            var vmTranslator = new InventoryVMTranslator();

            var svc = new AddInventorySvc(itemTranslator, constraintsTranslator, vmTranslator, mockRepository);
            var actual = svc.Handle(cmd, new CancellationToken()).Result;

            actual.Should().Be(expect);
        }

        [Test]
        public void Inbound()
        {
            var result = new Domain.Inventories.Models.Inventory(id, 10, new InventoryItem(),
               new[] { new InventoryConstraint(InventoryConstraintType.MaxQty, "20", TypeCode.Int32) });
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
                new[] { new InventoryConstraint(InventoryConstraintType.MaxQty, "20", TypeCode.Int32) });
            var repository = NSubstitute.Substitute.For<IInventoryRepository>();
            repository.GetBy(Arg.Any<InventoryId>()).Returns(result);
            var cmd = new OutBoundCmd() { Id = $"inv-{DateTimeOffset.Now:yyyyMMdd}-0", Amount = 5 };

            var svc = new OutboundSvc(new IdTranslator(), repository);
            var actual = svc.Handle(cmd, new CancellationToken()).Result;

            actual.Qty.Should().Be(5);
        }
    }
}