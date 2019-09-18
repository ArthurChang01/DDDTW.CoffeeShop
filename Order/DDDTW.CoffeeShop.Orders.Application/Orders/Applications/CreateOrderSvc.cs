﻿using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Messages;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Responses;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Commands;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.Applications
{
    public class CreateOrderSvc : IRequestHandler<CreateOrderMsg, OrderResp>
    {
        private readonly ITranslator<IEnumerable<OrderItem>, IEnumerable<OrderItemResp>> itemsTranslator;
        private readonly IOrderRepository repository;

        public CreateOrderSvc(
            ITranslator<IEnumerable<OrderItem>, IEnumerable<OrderItemResp>> itemsTranslator,
            IOrderRepository repository)
        {
            this.itemsTranslator = itemsTranslator;
            this.repository = repository;
        }

        public async Task<OrderResp> Handle(CreateOrderMsg request, CancellationToken cancellationToken)
        {
            var id = await this.repository.GenerateOrderId();
            var items = this.itemsTranslator.Translate(request.Items);
            var cmd = new CreateOrder(id, request.TableNo, OrderStatus.Initial, items);
            var order = Order.Create(cmd);

            await this.repository.Save(order);

            return new OrderResp(order);
        }
    }
}