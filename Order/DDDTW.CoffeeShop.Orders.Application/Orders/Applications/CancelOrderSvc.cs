using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Messages;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.Applications
{
    public class CancelOrderSvc : IRequestHandler<CancelOrderMsg>
    {
        private readonly ITranslator<OrderId, string> idTranslator;
        private readonly IOrderRepository repository;

        public CancelOrderSvc(
            ITranslator<OrderId, string> idTranslator,
            IOrderRepository repository)
        {
            this.idTranslator = idTranslator;
            this.repository = repository;
        }

        public async Task<Unit> Handle(CancelOrderMsg request, CancellationToken cancellationToken)
        {
            var id = this.idTranslator.Translate(request.Id);
            var order = await this.repository.GetBy(id) ?? throw new ArgumentException();

            order.Cancel();

            return new Unit();
        }
    }
}