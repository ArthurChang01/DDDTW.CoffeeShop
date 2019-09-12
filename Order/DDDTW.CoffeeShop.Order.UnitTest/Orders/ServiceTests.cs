using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Order.Application.Orders.Applications;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Responses;
using DDDTW.CoffeeShop.Order.Application.Orders.DomainServices;
using DDDTW.CoffeeShop.Order.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Models = DDDTW.CoffeeShop.Order.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Order.UnitTest.Orders
{
    [Parallelizable(ParallelScope.All)]
    public class ServiceTests
    {
        private readonly OrderId id;
        private readonly Models.Order order;
        private readonly Models.OrderItem item;

        private readonly OrderResp resp;

        private readonly IdTranslator idTranslator;
        private readonly OrderItemsTranslator itemsTranslator;

        private readonly IOrderRepository mockRepository;

        public ServiceTests()
        {
            var created = DateTimeOffset.Now;
            var modified = DateTimeOffset.Now;

            this.id = new OrderId();
            this.item = new OrderItem(new Product("1", "Prod"), 10, 10);
            this.order = new Models.Order(id, OrderStatus.Initial, new[]
            {
                this.item
            }, created, modified);
            this.idTranslator = new IdTranslator();
            this.itemsTranslator = new OrderItemsTranslator();

            this.resp = new OrderResp(this.order);

            this.mockRepository = NSubstitute.Substitute.For<IOrderRepository>();
            this.mockRepository.GenerateOrderId().Returns(id);
        }

        [Test]
        public async Task GetAllOrder()
        {
            var msg = new GetAllOrderMsg(1, 1);
            var expect = new List<OrderResp>() { this.resp };
            mockRepository.Get(Arg.Any<Specification<Models.Order>>(), 1, 1)
                .Returns(new List<Models.Order>() { order });

            var svc = new GetAllOrderSvc(this.mockRepository);
            var actual = await svc.Handle(msg, new CancellationToken());

            actual.Should().BeEquivalentTo(expect.AsEnumerable());
        }

        [Test]
        public async Task GetOrder()
        {
            var msg = new GetOrderMsg(this.id.ToString());
            var expect = this.resp;
            mockRepository.GetBy(Arg.Any<OrderId>()).Returns(this.order);

            var svc = new GetOrderSvc(new IdTranslator(), this.mockRepository);
            var actual = await svc.Handle(msg, new CancellationToken());

            actual.Should().Be(expect);
        }

        [Test]
        public async Task CreateOrder()
        {
            var msg = new CreateOrderMsg(new[]
            {
                new OrderItemResp(this.item),
            });
            this.mockRepository.Save(Arg.Any<Models.Order>(),
                Arg.Any<IReadOnlyList<IDomainEvent>>());

            var svc = new CreateOrderSvc(this.itemsTranslator, this.mockRepository);
            var actual = await svc.Handle(msg, new CancellationToken());

            actual.Should().Be(this.resp);
        }

        [Test]
        public async Task ChangeItem()
        {
            var msg = new ChangeItemMsg(this.id.ToString(), new[]
            {
                new OrderItemResp(new ProductResp("3", "pp"), 11, 11)
            });
            var result = new Models.Order(this.id, OrderStatus.Initial, new[]
            {
                this.item
            }, DateTimeOffset.Now, DateTimeOffset.Now);
            var expect = new Models.Order(this.id, OrderStatus.Initial, new[]
            {
                new OrderItem(new Product("3", "pp"), 11, 11),
            }, DateTimeOffset.Now, DateTimeOffset.Now);
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
            var result = new Models.Order(this.id, oriStatus, new[]
            {
                this.item
            }, DateTimeOffset.Now, DateTimeOffset.Now);
            var repository = NSubstitute.Substitute.For<IOrderRepository>();
            repository.GetBy(Arg.Any<OrderId>()).Returns(result);

            var svc = svcFunc(repository);
            await svc.Handle(msg, new CancellationToken());

            result.Status.Should().Be(expectedStatus);
        }
    }
}