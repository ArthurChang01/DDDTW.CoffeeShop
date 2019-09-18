using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Messages;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Results;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.Applications
{
    public class GetOrderSvc : IRequestHandler<GetOrderMsg, OrderRst>
    {
        private readonly ITranslator<OrderId, string> idTranslator;
        private readonly IOrderRepository repository;

        public GetOrderSvc(
            ITranslator<OrderId, string> idTranslator,
            IOrderRepository repository)
        {
            this.idTranslator = idTranslator;
            this.repository = repository;
        }

        public async Task<OrderRst> Handle(GetOrderMsg request, CancellationToken cancellationToken)
        {
            var id = this.idTranslator.Translate(request.Id);
            var order = await this.repository.GetBy(id) ?? throw new ArgumentException();

            return new OrderRst(order);
        }
    }
}