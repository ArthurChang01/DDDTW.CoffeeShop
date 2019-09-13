using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Responses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Commands;
using DDDTW.CoffeeShop.Order.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Order.Application.Orders.Applications
{
    public class ChangeItemSvc : IRequestHandler<ChangeItemMsg>
    {
        private readonly ITranslator<OrderId, string> idTranslator;
        private readonly ITranslator<IEnumerable<OrderItem>, IEnumerable<OrderItemResp>> itemsTranslator;
        private readonly IOrderRepository repository;

        public ChangeItemSvc(
            ITranslator<OrderId, string> idTranslator,
            ITranslator<IEnumerable<OrderItem>, IEnumerable<OrderItemResp>> itemsTranslator,
            IOrderRepository repository)
        {
            this.idTranslator = idTranslator;
            this.itemsTranslator = itemsTranslator;
            this.repository = repository;
        }

        public Task<Unit> Handle(ChangeItemMsg request, CancellationToken cancellationToken)
        {
            var id = this.idTranslator.Translate(request.Id);
            var order = this.repository.GetBy(id) ?? throw new ArgumentException();
            var items = this.itemsTranslator.Translate(request.Items);

            order.ChangeItem(new ChangeItem(items));

            this.repository.Save(order, order.DomainEvents);

            return Task.FromResult(new Unit());
        }
    }
}