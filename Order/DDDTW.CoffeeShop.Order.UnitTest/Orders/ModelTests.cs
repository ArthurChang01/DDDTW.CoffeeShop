using DDDTW.CoffeeShop.Order.Domain.Orders;
using DDDTW.CoffeeShop.Order.Domain.Orders.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Models = DDDTW.CoffeeShop.Order.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Order.UnitTest.Orders
{
    [Parallelizable(ParallelScope.All)]
    public class ModelTests
    {
        [Test]
        public void CreateOrder()
        {
            Models.Order order = new Models.Order();

            order.Id.ToString().Should().Be($"ord-{DateTimeOffset.Now:yyyyMMdd}-0");
            order.Status.Should().Be(Models.OrderStatus.Initial);
            order.OrderItems.Should().BeEmpty();
            order.CreatedDate.ToString("yyyyMMddHHmm").Should().Be(DateTimeOffset.Now.ToString("yyyyMMddHHmm"));
            order.ModifiedDate.Should().BeNull();
        }

        [Test]
        public void CreateOrderWithParameter()
        {
            var order = this.GetOrderBuildingParam(status: Models.OrderStatus.Deliver,
                modifiedDate: DateTimeOffset.Now).Order;

            order.Id.ToString().Should().Be($"ord-{DateTimeOffset.Now:yyyyMMdd}-0");
            order.Status.Should().Be(Models.OrderStatus.Deliver);
            order.OrderItems.First().Should().Be(new Models.OrderItem());
            order.CreatedDate.ToString("yyyyMMddHHmm").Should().Be(DateTimeOffset.Now.ToString("yyyyMMddHHmm"));
            order.ModifiedDate?.ToString("yyyyMMddHHmm").Should().Be(DateTimeOffset.Now.ToString("yyyyMMddHHmm"));
        }

        [Test]
        public void CreateOrder_And_OrderIdIsNull()
        {
            Action action = () => new Models.Order(null, Models.OrderStatus.Initial, new[] { new Models.OrderItem(), },
                DateTimeOffset.Now);

            action.Should().ThrowExactly<AggregateException>()
                .WithInnerException<OrderIdIsNullException>();
        }

        [Test]
        public void CreateOrder_And_OrderItemIsEmpty()
        {
            Action action = () => new Models.Order(new Models.OrderId(), Models.OrderStatus.Initial, new List<Models.OrderItem>(),
                DateTimeOffset.Now);

            action.Should().ThrowExactly<AggregateException>()
                .WithInnerException<OrderItemEmptyException>();
        }

        [Test]
        public void ChangeItem()
        {
            var newItem = new Models.OrderItem(new Models.Product(), 10, 10);
            var order = this.GetOrderBuildingParam().Order;

            order.ChangeItem(new[] { newItem });

            order.OrderItems.First().Should().Be(newItem);
            order.OrderItems.Count.Should().Be(1);
        }

        [Test]
        public void WhenProcessMethodIsCalled_ThenOrderStatusChangesStatus_FromInitialToProcess()
        {
            var order = this.GetOrderBuildingParam().Order;

            order.Process();

            order.Status.Should().Be(Models.OrderStatus.Processing);
        }

        [Test]
        public void WhenProcessMethodIsCalled_And_OrderStatusIsNotInitial_ThenThrowStatusTransitionException()
        {
            var oriStatus = Models.OrderStatus.Deliver;
            var errorMessage =
                $"Code: {OrderErrorCode.StatusTransit}, Message: Can not transit order status from {oriStatus} to {Models.OrderStatus.Processing}";
            var order = this.GetOrderBuildingParam(status: Models.OrderStatus.Deliver).Order;

            Action action = () => order.Process();

            action.Should().ThrowExactly<StatusTransitionException>()
                .And.Message.Should().Be(errorMessage);
        }

        [Test]
        public void WhenDeliverIsMethodCalled_ThenOrderStatusChangesStatus_FromProcessingToDeliver()
        {
            var order = this.GetOrderBuildingParam(status: Models.OrderStatus.Processing).Order;

            order.Deliver();

            order.Status.Should().Be(Models.OrderStatus.Deliver);
        }

        [Test]
        public void WhenDeliverMethodIsCalled_And_OrderStatusIsNotProcessing_ThenThrowStatusTransitionException()
        {
            var oriStatus = Models.OrderStatus.Closed;
            var errorMessage =
                $"Code: {OrderErrorCode.StatusTransit}, Message: Can not transit order status from {oriStatus} to {Models.OrderStatus.Deliver}";
            var order = this.GetOrderBuildingParam(status: oriStatus).Order;

            Action action = () => order.Deliver();

            action.Should().ThrowExactly<StatusTransitionException>()
                .And.Message.Should().Be(errorMessage);
        }

        [Test]
        public void WhenClosedIsMethodCalled_ThenOrderStatusChangesStatusFromDeliverToClosed()
        {
            var order = this.GetOrderBuildingParam(status: Models.OrderStatus.Deliver).Order;

            order.Closed();

            order.Status.Should().Be(Models.OrderStatus.Closed);
        }

        [Test]
        public void WhenClosedMethodIsCalled_And_OrderStatusIsNotDeliver_ThenThrowStatusTransitionException()
        {
            var oriStatus = Models.OrderStatus.Processing;
            var errorMessage =
                $"Code: {OrderErrorCode.StatusTransit}, Message: Can not transit order status from {oriStatus} to {Models.OrderStatus.Closed}";
            var order = this.GetOrderBuildingParam(status: oriStatus).Order;

            Action action = () => order.Closed();

            action.Should().ThrowExactly<StatusTransitionException>()
                .And.Message.Should().Be(errorMessage);
        }

        [Test]
        public void WhenCancelMethodIsCalled_And_OrderStatusIsDeliver_ThenOrderStatusChangesStatusFromDeliverToCancel()
        {
            var order = this.GetOrderBuildingParam(status: Models.OrderStatus.Deliver).Order;

            order.Cancel();

            order.Status.Should().Be(Models.OrderStatus.Cancel);
        }

        private (Models.OrderId OrderId, Models.OrderItem OrderItem, Models.Order Order) GetOrderBuildingParam(
            int seqNo = 0, DateTimeOffset? occuredDate = null,
            Models.OrderStatus? status = Models.OrderStatus.Initial,
            int? qty = null, decimal? price = null,
            DateTimeOffset? modifiedDate = null)
        {
            var orderId = new Models.OrderId(seqNo, occuredDate ?? DateTimeOffset.Now);
            var orderItem = new Models.OrderItem(new Models.Product(), qty ?? 0, price ?? 0);
            var order = new Models.Order(orderId, status ?? Models.OrderStatus.Initial,
                new[] { orderItem },
                DateTimeOffset.Now, modifiedDate);

            return (orderId, orderItem, order);
        }
    }
}