using DDDTW.CoffeeShop.Orders.Domain;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Commands;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Exceptions;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.Orders.UnitTest.Orders
{
    [Parallelizable(ParallelScope.All)]
    public class ModelTests
    {
        [Test]
        public void CreateOrder()
        {
            Order order = new Order();

            order.Id.ToString().Should().Be($"ord-{DateTimeOffset.Now:yyyyMMdd}-0");
            order.Status.Should().Be(OrderStatus.Initial);
            order.OrderItems.Should().BeEmpty();
            order.CreatedDate.ToString("yyyyMMddHHmm").Should().Be(DateTimeOffset.Now.ToString("yyyyMMddHHmm"));
            order.ModifiedDate.Should().BeNull();
        }

        [Test]
        public void CreateOrderWithParameter()
        {
            var order = this.GetOrderBuildingParam(status: OrderStatus.Deliver).Order;

            order.Id.ToString().Should().Be($"ord-{DateTimeOffset.Now:yyyyMMdd}-0");
            order.Status.Should().Be(OrderStatus.Deliver);
            order.OrderItems.First().Should().Be(new OrderItem());
            order.CreatedDate.ToString("yyyyMMddHHmm").Should().Be(DateTimeOffset.Now.ToString("yyyyMMddHHmm"));
        }

        [Test]
        public void CreateOrder_And_OrderIdIsNull()
        {
            var cmd = new CreateOrder(null, "0", OrderStatus.Initial, new[] { new OrderItem(), });
            Action action = () => Order.Create(cmd);

            action.Should().ThrowExactly<AggregateException>()
                .WithInnerException<OrderIdIsNullException>();
        }

        [Test]
        public void CreateOrder_And_OrderItemIsEmpty()
        {
            var cmd = new CreateOrder(new OrderId(), "0", OrderStatus.Initial, new List<OrderItem>());

            Action action = () => Order.Create(cmd);

            action.Should().ThrowExactly<AggregateException>()
                .WithInnerException<OrderItemEmptyException>();
        }

        [Test]
        public void ChangeItem()
        {
            var newItem = new OrderItem($"prd-{DateTimeOffset.Now:yyyyMMdd}-0", 10, 10);
            var order = this.GetOrderBuildingParam().Order;

            order.ChangeItem(new ChangeItem(new[] { newItem }));

            order.OrderItems.First().Should().Be(newItem);
            order.OrderItems.Count.Should().Be(1);
        }

        [Test]
        public void WhenProcessMethodIsCalled_ThenOrderStatusChangesStatus_FromInitialToProcess()
        {
            var order = this.GetOrderBuildingParam().Order;

            order.Process();

            order.Status.Should().Be(OrderStatus.Processing);
        }

        [Test]
        public void WhenProcessMethodIsCalled_And_OrderStatusIsNotInitial_ThenThrowStatusTransitionException()
        {
            var oriStatus = OrderStatus.Deliver;
            var errorMessage =
                $"Code: Order-{OrderErrorCode.StatusTransit}, Message: Can not transit order status from {oriStatus} to {OrderStatus.Processing}";
            var order = this.GetOrderBuildingParam(status: OrderStatus.Deliver).Order;

            Action action = () => order.Process();

            action.Should().ThrowExactly<StatusTransitionException>()
                .And.Message.Should().Be(errorMessage);
        }

        [Test]
        public void WhenDeliverIsMethodCalled_ThenOrderStatusChangesStatus_FromProcessingToDeliver()
        {
            var order = this.GetOrderBuildingParam(status: OrderStatus.Processing).Order;

            order.Deliver();

            order.Status.Should().Be(OrderStatus.Deliver);
        }

        [Test]
        public void WhenDeliverMethodIsCalled_And_OrderStatusIsNotProcessing_ThenThrowStatusTransitionException()
        {
            var oriStatus = OrderStatus.Closed;
            var errorMessage =
                $"Code: Order-{OrderErrorCode.StatusTransit}, Message: Can not transit order status from {oriStatus} to {OrderStatus.Deliver}";
            var order = this.GetOrderBuildingParam(status: oriStatus).Order;

            Action action = () => order.Deliver();

            action.Should().ThrowExactly<StatusTransitionException>()
                .And.Message.Should().Be(errorMessage);
        }

        [Test]
        public void WhenClosedIsMethodCalled_ThenOrderStatusChangesStatusFromDeliverToClosed()
        {
            var order = this.GetOrderBuildingParam(status: OrderStatus.Deliver).Order;

            order.Closed();

            order.Status.Should().Be(OrderStatus.Closed);
        }

        [Test]
        public void WhenClosedMethodIsCalled_And_OrderStatusIsNotDeliver_ThenThrowStatusTransitionException()
        {
            var oriStatus = OrderStatus.Processing;
            var errorMessage =
                $"Code: Order-{OrderErrorCode.StatusTransit}, Message: Can not transit order status from {oriStatus} to {OrderStatus.Closed}";
            var order = this.GetOrderBuildingParam(status: oriStatus).Order;

            Action action = () => order.Closed();

            action.Should().ThrowExactly<StatusTransitionException>()
                .And.Message.Should().Be(errorMessage);
        }

        [Test]
        public void WhenCancelMethodIsCalled_And_OrderStatusIsDeliver_ThenOrderStatusChangesStatusFromDeliverToCancel()
        {
            var order = this.GetOrderBuildingParam(status: OrderStatus.Deliver).Order;

            order.Cancel();

            order.Status.Should().Be(OrderStatus.Cancel);
        }

        private (OrderId OrderId, OrderItem OrderItem, Order Order) GetOrderBuildingParam(
            int seqNo = 0, DateTimeOffset? occuredDate = null,
            OrderStatus? status = OrderStatus.Initial,
            int? qty = null, decimal? price = null)
        {
            var orderId = new OrderId(seqNo, occuredDate ?? DateTimeOffset.Now);
            var item = new OrderItem(string.Empty, qty ?? 0, price ?? 0);
            var cmd = new CreateOrder(orderId, "0", status ?? OrderStatus.Initial, new[] { item });
            var order = Order.Create(cmd);

            return (orderId, item, order);
        }
    }
}