using System;
using System.Linq;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.DomainEvents;
using DDDTW.CoffeeShop.Inventory.Domain.Inventories.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using Models = DDDTW.CoffeeShop.Inventory.Domain.Inventories.Models;

namespace DDDTW.CoffeeShop.Inventory.UnitTest
{
    public class InventoryTests
    {
        [Test]
        public void CreateInventory()
        {
            Models.Inventory inventory = new Models.Inventory();

            inventory.Id.ToString().Should().Be($"inv-{DateTimeOffset.Now:yyyyMMdd}-0");
            inventory.Qty.Should().Be(0);
            inventory.Item.Should().Be(new Models.InventoryItem());
            inventory.Constraint.Count.Should().Be(0);
        }

        [Test]
        public void CreateInventoryWithParameters()
        {
            var param = this.GetParameters(1, DateTimeOffset.Now.AddDays(-1));
            var inventory = new Models.Inventory(
                param.id, 2, param.item, new[] { param.constraint });

            inventory.Id.ToString().Should().Be($"inv-{DateTimeOffset.Now.AddDays(-1):yyyyMMdd}-1");
            inventory.Qty.Should().Be(2);
            inventory.Item.Should().Be(param.item);
            inventory.Constraint.Count.Should().Be(1);
            inventory.Constraint.First().Should().Be(param.constraint);
        }

        [Test]
        public void When_Create_Inventory_And_Qty_Is_Negative_Digital_Then_Throw_NegativeQtyException()
        {
            var param = this.GetParameters();

            Action action = () => new Models.Inventory(param.id, -13, param.item, new[] { param.constraint });

            action.Should().Throw<AggregateException>()
                .WithInnerException<NegativeQtyException>();
        }

        [Test]
        public void When_Create_Inventory_And_Item_Is_Null_Then_Throw_InventoryItemIsNullException()
        {
            var param = this.GetParameters();

            Action action = () => new Models.Inventory(param.id, 27, null, new[] { param.constraint });

            action.Should().Throw<AggregateException>()
                .WithInnerException<InventoryItemIsNullException>();
        }

        [Test]
        public void When_Create_Inventory_And_Constraint_Is_Empty_Then_Throw_EmptyConstraintException()
        {
            var param = this.GetParameters();

            Action action = () => new Models.Inventory(param.id, 27, param.item, null);

            action.Should().Throw<AggregateException>()
                .WithInnerException<EmptyConstraintException>();
        }

        [Test]
        public void Given_Qty_Is_0_When_Inbound_50_Then_Qty_Is_50()
        {
            var param = this.GetParameters(maxQty: 50);
            var expectQty = 50;

            var actual = new Models.Inventory(param.id, 0, param.item, new[] { param.constraint });
            actual.Inbound(50);
            var evt = (Inbounded)actual.DomainEvents.First();

            actual.Qty.Should().Be(expectQty);
            evt.Amount.Should().Be(50);
            evt.Qty.Should().Be(50);
        }

        [Test]
        public void When_Inbound_negative_Digital_Then_Throw_ArgumentException()
        {
            var param = this.GetParameters();
            var actual = new Models.Inventory(param.id, 0, param.item, new[] { param.constraint });

            Action action = () => actual.Inbound(-3);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Given_Qty_Is_26_And_MaxQty_Is_50_When_Inbound_30_Then_Throw_OverQtyLimitationException()
        {
            var param = this.GetParameters(maxQty: 50);
            var actual = new Models.Inventory(param.id, 26, param.item, new[] { param.constraint });

            Action action = () => actual.Inbound(30);

            action.Should().Throw<OverQtyLimitationException>();
        }

        [Test]
        public void Given_Qty_Is_37_When_Outbound_2_Then_Qty_Is_35()
        {
            var param = this.GetParameters();
            var expectQty = 35;

            var actual = new Models.Inventory(param.id, 37, param.item, new[] { param.constraint });
            actual.Outbound(2);
            var evt = (Outbounded)actual.DomainEvents.First();

            actual.Qty.Should().Be(expectQty);
            evt.Amount.Should().Be(2);
            evt.Qty.Should().Be(35);
        }

        [Test]
        public void When_Outbound_Negative_Digital_Then_Throw_ArgumentException()
        {
            var param = this.GetParameters();
            var actual = new Models.Inventory(param.id, 0, param.item, new[] { param.constraint });

            Action action = () => actual.Outbound(-3);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Given_Qty_Is_17_When_Outbound_38_Then_Throw_InventoryShortageException()
        {
            var param = this.GetParameters();
            var actual = new Models.Inventory(param.id, 17, param.item, new[] { param.constraint });

            Action action = () => actual.Outbound(38);

            action.Should().Throw<InventoryShortageException>();
        }

        private (Models.InventoryId id, Models.InventoryItem item, Models.InventoryConstraint constraint)
            GetParameters(int seqNo = 0, DateTimeOffset? occuredDate = null, int maxQty = 10)
        {
            return (new Models.InventoryId(seqNo, occuredDate ?? DateTimeOffset.Now),
                new Models.InventoryItem("Milk", "X-R-200", 80, "MilkShop", Models.ItemCategory.Milk,
                    "Bottle", 2000),
                new Models.InventoryConstraint(Models.InventoryConstraintType.MaxQty, maxQty.ToString(), typeof(int).ToString()));
        }
    }
}