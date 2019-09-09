using DDDTW.CoffeeShop.Inventory.Application.Inventories.DataContracts.Commands;
using FluentValidation;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventory.WebAPI.Validations
{
    public class AddInventoryCmdValidator : AbstractValidator<AddInventoryCmd>
    {
        public AddInventoryCmdValidator()
        {
            RuleFor(x => x.Qty).GreaterThanOrEqualTo(0);

            RuleFor(x => x.Item)
                .NotNull()
                .DependentRules(() =>
                {
                    RuleFor(x => x.Item.Capacity).GreaterThanOrEqualTo(0);
                    RuleFor(x => x.Item.Name).NotEmpty();
                    RuleFor(x => x.Item.SKU).NotEmpty();
                    RuleFor(x => x.Item.Price).GreaterThanOrEqualTo(0);
                });

            RuleFor(x => x.Constraints)
                .Must(collection => collection == null || collection.All(x =>
                                        string.IsNullOrWhiteSpace(x.Value)));
        }
    }
}