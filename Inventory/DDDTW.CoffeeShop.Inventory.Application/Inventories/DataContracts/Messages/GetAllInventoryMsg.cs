using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Responses;
using MediatR;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Messages
{
    public class GetAllInventoryMsg : IRequest<IEnumerable<InventoryResp>>
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