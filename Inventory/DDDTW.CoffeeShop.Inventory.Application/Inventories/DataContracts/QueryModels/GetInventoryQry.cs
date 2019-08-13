using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.ViewModels;
using MediatR;

namespace DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.QueryModels
{
    public class GetInventoryQry : IRequest<InventoryVM>
    {
        public string Id { get; set; }
    }
}