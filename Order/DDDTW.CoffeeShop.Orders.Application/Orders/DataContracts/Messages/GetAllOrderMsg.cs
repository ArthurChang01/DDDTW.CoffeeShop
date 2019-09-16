using DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Responses;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Messages
{
    public class GetAllOrderMsg : IRequest<IEnumerable<OrderResp>>
    {
        public GetAllOrderMsg(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }

        public int PageNo { get; private set; }

        public int PageSize { get; private set; }
    }
}