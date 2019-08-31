using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Commands;
using FluentValidation;

namespace DDDTW.CoffeeShop.Inventory.WebAPI.Validations
{
    public class InboundCmdValidator : AbstractValidator<InboundCmd>
    {
        public InboundCmdValidator()
        {
            RuleFor(x => x.Id).Matches(@"\S{3}-\d{8}-\d+");
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        }
    }
}