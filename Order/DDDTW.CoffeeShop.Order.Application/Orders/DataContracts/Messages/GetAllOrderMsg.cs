using DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Order.Application.Orders.DataContracts.Messages
{
    public class GetAllOrderMsg : IRequest<IEnumerable<OrderVM>>
    {
        public GetAllOrderMsg(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }

        public int PageNo { get; set; }

        public int PageSize { get; set; }
    }
}