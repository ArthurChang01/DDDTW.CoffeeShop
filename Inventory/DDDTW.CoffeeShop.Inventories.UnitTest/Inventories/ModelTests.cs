using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Commands;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.DomainEvents;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Exceptions;
using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventories.UnitTest.Inventories
{
    [Parallelizable(ParallelScope.All)]
    public class ModelTests
    {
        [Test]
        public void CreateInventory()
        {
            Domain.Inventories.Models.Inventory inventory = new Domain.Inventories.Models.Inventory();

            inventory.Id.ToString().Should().Be($"inv-{DateTimeOffset.Now:yyyyMMdd}-0");
            inventory.Qty.Should().Be(0);
            inventory.Item.Should().Be(new Domain.Inventories.Models.InventoryItem());
            inventory.Constraint.Count.Should().Be(0);
        }

        [Test]
        public void CreateInventoryWithParameters()
        {
            var param = this.GetParameters(1, DateTimeOffset.Now.AddDays(-1));
            var cmd = new CreateInventory(param.id, 2, param.item, new[] { param.constraint });
            var inventory = Inventory.Create(cmd);

            inventory.Id.ToString().Should().Be($"inv-{DateTimeOffset.Now.AddDays(-1):yyyyMMdd}-1");
            inventory.Qty.Should().Be(2);
            inventory.Item.Should().Be(param.item);
            inventory.Constraint.Count.Should().Be(1);
            inventory.Constraint.First().Should().Be(param.constraint);
        }

        [Test]
        public void CreateInventory_And_ConstraintTypeIsMaxQty_And_ConstraintValueTypeIsString_Then_ThrowException()
        {
            var param = this.GetParameters(1, DateTimeOffset.Now);
            var cmd = new CreateInventory(param.id, 1, param.item, new[]
            {
                new InventoryConstraint(InventoryConstraintType.MaxQty, "10", TypeCode.String),
            });

            Action action = () => Inventory.Create(cmd);

            action.Should().ThrowExactly<AggregateException>()
                .WithInnerException<ConstraintValueIncorrectException>();
        }

        [Test]
        public void WhenCreateInventory_And_QtyIsNegativeDigital_ThenThrowNegativeQtyException()
        {
            var param = this.GetParameters();
            var cmd = new CreateInventory(param.id, -13, param.item, new[] { param.constraint });

            Action action = () => Inventory.Create(cmd);

            action.Should().Throw<AggregateException>()
                .WithInnerException<NegativeQtyException>();
        }

        [Test]
        public void WhenCreateInventory_And_ItemIsNull_ThenThrowInventoryItemIsNullException()
        {
            var param = this.GetParameters();
            var cmd = new CreateInventory(param.id, 27, null, new[] { param.constraint });

            Action action = () => Inventory.Create(cmd);

            action.Should().Throw<AggregateException>()
                .WithInnerException<InventoryItemIsNullException>();
        }

        [Test]
        public void When_CreateInventory_And_ConstraintIsEmpty_Then_ThrowEmptyConstraintException()
        {
            var param = this.GetParameters();
            var cmd = new CreateInventory(param.id, 27, param.item, null);

            Action action = () => Inventory.Create(cmd);

            action.Should().Throw<AggregateException>()
                .WithInnerException<EmptyConstraintException>();
        }

        [Test]
        public void Given_QtyIs0_When_InboundIs50_Then_QtyIs50()
        {
            var param = this.GetParameters(maxQty: 50);
            var expectQty = 50;
            var createdCmd = new CreateInventory(param.id, 0, param.item, new[] { param.constraint });
            var inboundCmd = new Inbound(50);

            var actual = Inventory.Create(createdCmd);
            actual.Inbound(inboundCmd);
            var evt = (Inbounded)actual.DomainEvents.Last();

            actual.Qty.Should().Be(expectQty);
            evt.Amount.Should().Be(50);
            evt.Qty.Should().Be(50);
        }

        [Test]
        public void When_InboundIsNegativeDigital_Then_ThrowAmountIncorrectException()
        {
            var param = this.GetParameters();
            var createdCmd = new CreateInventory(param.id, 0, param.item, new[] { param.constraint });
            var inboundCmd = new Inbound(-3);

            var actual = Inventory.Create(createdCmd);

            Action action = () => actual.Inbound(inboundCmd);

            action.Should().Throw<AmountIncorrectException>();
        }

        [Test]
        public void Given_QtyIs26_And_MaxQtyIs50_When_InboundIs30_Then_ThrowOverQtyLimitationException()
        {
            var param = this.GetParameters(maxQty: 50);
            var createdCmd = new CreateInventory(param.id, 26, param.item, new[] { param.constraint });
            var inboundCmd = new Inbound(30);
            var actual = Inventory.Create(createdCmd);

            Action action = () => actual.Inbound(inboundCmd);

            action.Should().Throw<OverQtyLimitationException>();
        }

        [Test]
        public void Given_QtyIs37_When_OutboundIs2_ThenQtyIs35()
        {
            var param = this.GetParameters();
            var expectQty = 35;
            var createdCmd = new CreateInventory(param.id, 37, param.item, new[] { param.constraint });
            var outboundCmd = new Outbound(2);

            var actual = Inventory.Create(createdCmd);
            actual.Outbound(outboundCmd);
            var evt = (Outbounded)actual.DomainEvents.Last();

            actual.Qty.Should().Be(expectQty);
            evt.Amount.Should().Be(2);
            evt.Qty.Should().Be(35);
        }

        [Test]
        public void When_OutboundIsNegativeDigital_Then_ThrowAmountIncorrectException()
        {
            var param = this.GetParameters();
            var createdCmd = new CreateInventory(param.id, 0, param.item, new[] { param.constraint });
            var outboundCmd = new Outbound(-3);
            var actual = Inventory.Create(createdCmd);

            Action action = () => actual.Outbound(outboundCmd);

            action.Should().Throw<AmountIncorrectException>();
        }

        [Test]
        public void Given_QtyIs17_When_OutboundIs38_Then_ThrowInventoryShortageException()
        {
            var param = this.GetParameters();
            var createdCmd = new CreateInventory(param.id, 17, param.item, new[] { param.constraint });
            var outboundCmd = new Outbound(38);
            var actual = Inventory.Create(createdCmd);

            Action action = () => actual.Outbound(outboundCmd);

            action.Should().Throw<InventoryShortageException>();
        }

        private (InventoryId id, InventoryItem item, InventoryConstraint constraint)
            GetParameters(int seqNo = 0, DateTimeOffset? occuredDate = null, int maxQty = 10)
        {
            return (new InventoryId(seqNo, occuredDate ?? DateTimeOffset.Now),
                new InventoryItem("Milk", "X-R-200", 80, "MilkShop", ItemCategory.Milk,
                    "Bottle", 2000),
                new InventoryConstraint(InventoryConstraintType.MaxQty, maxQty.ToString(), TypeCode.Int32));
        }
    }
}