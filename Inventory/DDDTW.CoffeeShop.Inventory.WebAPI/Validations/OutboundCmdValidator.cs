using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Messages;
using FluentValidation;

namespace DDDTW.CoffeeShop.Inventory.WebAPI.Validations
{
    public class OutboundCmdValidator : AbstractValidator<OutBoundMsg>
    {
        public OutboundCmdValidator()
        {
            RuleFor(x => x.Id).Matches(@"\S{3}-\d{8}-\d+");
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        }
    }
}