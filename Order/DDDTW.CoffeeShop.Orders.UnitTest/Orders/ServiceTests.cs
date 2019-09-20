using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Application.Orders.Applications;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Messages;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Results;
using DDDTW.CoffeeShop.Orders.Application.Orders.DomainServices;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Commands;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Orders.UnitTest.Orders
{
    [Parallelizable(ParallelScope.All)]
    public class ServiceTests
    {
        private readonly OrderId id;
        private readonly Order order;
        private readonly OrderItem item;

        private readonly OrderRst result;

        private readonly IdTranslator idTranslator;
        private readonly OrderItemsTranslator itemsTranslator;

        private readonly IOrderRepository mockRepository;

        public ServiceTests()
        {
            this.id = new OrderId();
            this.item = new OrderItem($"prd-{DateTimeOffset.Now:yyyyMMdd}-1", 10, 10);
            var cmd = new CreateOrder(id, "0", OrderStatus.Initial, new[] { this.item });
            this.order = Order.Create(cmd);
            this.idTranslator = new IdTranslator();
            this.itemsTranslator = new OrderItemsTranslator();

            this.result = new OrderRst(this.order);

            this.mockRepository = Substitute.For<IOrderRepository>();
            this.mockRepository.GenerateOrderId().Returns(id);
        }

        [Test]
        public async Task GetAllOrder()
        {
            var msg = new GetAllOrderMsg(1, 1);
            var expect = new List<OrderRst>() { this.result };
            mockRepository.Get(Arg.Any<Specification<Order>>(), 1, 1)
                .Returns(new List<Order>() { order });

            var svc = new GetAllOrderSvc(this.mockRepository);
            var actual = await svc.Handle(msg, new CancellationToken());

            actual.Should().BeEquivalentTo(expect.AsEnumerable());
        }

        [Test]
        public async Task GetOrder()
        {
            var msg = new GetOrderMsg(this.id.ToString());
            var expect = this.result;
            mockRepository.GetBy(Arg.Any<OrderId>()).Returns(this.order);

            var svc = new GetOrderSvc(new IdTranslator(), this.mockRepository);
            var actual = await svc.Handle(msg, new CancellationToken());

            actual.Should().Be(expect);
        }

        [Test]
        public async Task CreateOrder()
        {
            var msg = new CreateOrderMsg("0", new[]
            {
                new OrderItemRst(this.item),
            });
            await this.mockRepository.Save(Arg.Any<Order>());

            var svc = new CreateOrderSvc(this.itemsTranslator, this.mockRepository);
            var actual = await svc.Handle(msg, new CancellationToken());

            actual.Id.Should().Be(this.result.Id);
            actual.Status.Should().Be(this.result.Status);
            actual.Items.Should().BeEquivalentTo(this.result.Items.AsEnumerable());
        }

        [Test]
        public async Task ChangeItem()
        {
            var msg = new ChangeItemMsg(this.id.ToString(), new[]
            {
                new OrderItemRst($"prd-{DateTimeOffset.Now:yyyyMMdd}-3",  11, 11)
            });
            var cmd = new CreateOrder(this.id, "0", OrderStatus.Initial, new[] { this.item });
            var result = Order.Create(cmd);
            var expectedCmd = new CreateOrder(this.id, "0", OrderStatus.Initial, new[]
            {
                new OrderItem($"prd-{DateTimeOffset.Now:yyyyMMdd}-3", 11, 11 ),
            });
            var expect = Order.Create(expectedCmd);
            var repository = NSubstitute.Substitute.For<IOrderRepository>();
            repository.GetBy(Arg.Any<OrderId>()).Returns(result);

            var svc = new ChangeItemSvc(this.idTranslator, this.itemsTranslator, repository);
            await svc.Handle(msg, new CancellationToken());

            result.OrderItems.First().Should().Be(expect.OrderItems.First());
        }

        [Test]
        public async Task ProcessingOrder()
        {
            await this.VerifyStatusChanging(
                () => new ProcessOrderMsg(this.id.ToString()),
                repository => new ProcessOrderSvc(this.idTranslator, repository),
                OrderStatus.Initial,
                OrderStatus.Processing);
        }

        [Test]
        public async Task DeliverOrder()
        {
            await this.VerifyStatusChanging(
                () => new DeliverOrderMsg(this.id.ToString()),
                repository => new DeliverOrderSvc(this.idTranslator, repository),
                OrderStatus.Processing,
                OrderStatus.Deliver);
        }

        [Test]
        public async Task CloseOrder()
        {
            await this.VerifyStatusChanging(
                () => new CloseOrderMsg(this.id.ToString()),
                repository => new CloseOrderSvc(this.idTranslator, repository),
                OrderStatus.Deliver,
                OrderStatus.Closed);
        }

        [Test]
        public async Task CancelOrder()
        {
            await this.VerifyStatusChanging(
                () => new CancelOrderMsg(this.id.ToString()),
                repository => new CancelOrderSvc(this.idTranslator, repository),
                OrderStatus.Initial,
                OrderStatus.Cancel);
        }

        private async Task VerifyStatusChanging<T>(
            Func<T> msgFunc,
            Func<IOrderRepository, IRequestHandler<T>> svcFunc,
            OrderStatus oriStatus,
            OrderStatus expectedStatus)
        where T : IRequest<Unit>
        {
            var msg = msgFunc();
            var cmd = new CreateOrder(this.id, "0", oriStatus, new[] { this.item });
            var result = Order.Create(cmd);
            var repository = NSubstitute.Substitute.For<IOrderRepository>();
            repository.GetBy(Arg.Any<OrderId>()).Returns(result);

            var svc = svcFunc(repository);
            await svc.Handle(msg, new CancellationToken());

            result.Status.Should().Be(expectedStatus);
        }
    }
}