using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Commands;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels;
using DDDTW.CoffeeShop.Order.Domain.Orders.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Models = DDDTW.CoffeeShop.Order.Domain.Orders.Models;

namespace DDDTW.CoffeeShop.Order.Application.Orders.Applications
{
    public class CreateOrderSvc : IRequestHandler<CreateOrderCmd, OrderVM>
    {
        private readonly ITranslator<OrderVM, Models.Order> vmTranslator;
        private readonly IOrderRepository repository;

        public CreateOrderSvc(
            ITranslator<OrderVM, Models.Order> vmTranslator,
            IOrderRepository repository)
        {
            this.vmTranslator = vmTranslator;
            this.repository = repository;
        }

        public Task<OrderVM> Handle(CreateOrderCmd request, CancellationToken cancellationToken)
        {
            var id = this.repository.GenerateOrderId();
            var order = new Models.Order(id, Models.OrderStatus.Initial, request.Items, DateTimeOffset.Now);

            this.repository.Save(order, order.DomainEvents);

            var vm = this.vmTranslator.Translate(order);

            return Task.FromResult(vm);
        }
    }
}