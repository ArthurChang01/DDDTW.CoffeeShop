using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Order.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Order.Domain.Orders.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

using Models = DDDTW.CoffeeShop.Order.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Order.Application.Orders.Applications
{
    public class GetOrderSvc : IRequestHandler<GetOrderMsg, OrderVM>
    {
        private readonly ITranslator<OrderId, string> idTranslator;
        private readonly ITranslator<OrderVM, Models.Order> vmTranslator;
        private readonly IOrderRepository repository;

        public GetOrderSvc(
            ITranslator<OrderId, string> idTranslator,
            ITranslator<OrderVM, Models.Order> vmTranslator,
            IOrderRepository repository)
        {
            this.idTranslator = idTranslator;
            this.vmTranslator = vmTranslator;
            this.repository = repository;
        }

        public Task<OrderVM> Handle(GetOrderMsg request, CancellationToken cancellationToken)
        {
            var id = this.idTranslator.Translate(request.Id);
            var order = this.repository.GetBy(id) ?? throw new ArgumentException();

            var vm = this.vmTranslator.Translate(order);

            return Task.FromResult(vm);
        }
    }
}