using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages;
using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Responses;
using DDDTW.CoffeeShop.Order.Domain.Orders.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Order.Application.Orders.Applications
{
    public class GetAllOrderSvc : IRequestHandler<GetAllOrderMsg, IEnumerable<OrderResp>>
    {
        private readonly IOrderRepository repository;

        public GetAllOrderSvc(
            IOrderRepository repository)
        {
            this.repository = repository;
        }

        public Task<IEnumerable<OrderResp>> Handle(GetAllOrderMsg request, CancellationToken cancellationToken)
        {
            var orders = this.repository.Get(new Specification<Domain.Orders.Models.Order>(q => true), request.PageNo,
                request.PageSize);
            return Task.FromResult(orders.Select(o => new OrderResp(o)));
        }
    }
}