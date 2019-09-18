using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Messages;
using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Responses;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Orders.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.Applications
{
    public class GetAllOrderSvc : IRequestHandler<GetAllOrderMsg, IEnumerable<OrderResp>>
    {
        private readonly IOrderRepository repository;

        public GetAllOrderSvc(
            IOrderRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<OrderResp>> Handle(GetAllOrderMsg request, CancellationToken cancellationToken)
        {
            var orders = await this.repository.Get(new Specification<Order>(q => true), request.PageNo,
                request.PageSize);

            return orders.Select(o => new OrderResp(o));
        }
    }
}