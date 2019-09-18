using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Results;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Messages
{
    public class GetAllInventoryMsg : IRequest<IEnumerable<InventoryRst>>
    {
        public GetAllInventoryMsg(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }

        public int PageNo { get; private set; }

        public int PageSize { get; private set; }
    }
}