using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Commands;
using FluentValidation;

namespace DDDTW.CoffeeShop.Inventory.WebAPI.Validations
{
    public class OutboundCmdValidator : AbstractValidator<OutBoundCmd>
    {
        public OutboundCmdValidator()
        {
            RuleFor(x => x.Id).Matches(@"\S{3}-\d{8}-\d+");
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        }
    }
}