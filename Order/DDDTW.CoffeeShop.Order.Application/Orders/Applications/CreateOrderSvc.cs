using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Responses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models = DDDTW.CoffeeShop.Order.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Order.Application.Orders.Applications
{
    public class CreateOrderSvc : IRequestHandler<CreateOrderMsg, OrderResp>
    {
        private readonly ITranslator<IEnumerable<Models.OrderItem>, IEnumerable<OrderItemResp>> itemsTranslator;
        private readonly IOrderRepository repository;

        public CreateOrderSvc(
            ITranslator<IEnumerable<Models.OrderItem>, IEnumerable<OrderItemResp>> itemsTranslator,
            IOrderRepository repository)
        {
            this.itemsTranslator = itemsTranslator;
            this.repository = repository;
        }

        public Task<OrderResp> Handle(CreateOrderMsg request, CancellationToken cancellationToken)
        {
            var id = this.repository.GenerateOrderId();
            var items = this.itemsTranslator.Translate(request.Items);
            var order = new Models.Order(id, Models.OrderStatus.Initial, items, DateTimeOffset.Now);

            this.repository.Save(order, order.DomainEvents);

            var vm = new OrderResp(order);

            return Task.FromResult(vm);
        }
    }
}