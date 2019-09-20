using DDDTW.CoffeeShop.Orders.Application.Products.DataContracts.Results;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Orders.Application.Products.DataContracts.Messages
{
    public class GetProductMsg : IRequest<IEnumerable<ProductRst>>
    {
        public GetProductMsg()
        {
        }

        public GetProductMsg(int pageNo, int pageSize)
        {
            this.PageNo = pageNo;
            this.PageSize = pageSize;
        }

        public int PageNo { get; set; }

        public int PageSize { get; set; }
    }
}