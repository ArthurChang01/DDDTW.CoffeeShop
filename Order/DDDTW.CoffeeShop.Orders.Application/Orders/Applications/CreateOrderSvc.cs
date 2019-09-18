using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Messages;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Results;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Commands;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.Applications
{
    public class CreateOrderSvc : IRequestHandler<CreateOrderMsg, OrderRst>
    {
        private readonly ITranslator<IEnumerable<OrderItem>, IEnumerable<OrderItemRst>> itemsTranslator;
        private readonly IOrderRepository repository;

        public CreateOrderSvc(
            ITranslator<IEnumerable<OrderItem>, IEnumerable<OrderItemRst>> itemsTranslator,
            IOrderRepository repository)
        {
            this.itemsTranslator = itemsTranslator;
            this.repository = repository;
        }

        public async Task<OrderRst> Handle(CreateOrderMsg request, CancellationToken cancellationToken)
        {
            var id = await this.repository.GenerateOrderId();
            var items = this.itemsTranslator.Translate(request.Items);
            var cmd = new CreateOrder(id, request.TableNo, OrderStatus.Initial, items);
            var order = Order.Create(cmd);

            await this.repository.Save(order);

            return new OrderRst(order);
        }
    }
}