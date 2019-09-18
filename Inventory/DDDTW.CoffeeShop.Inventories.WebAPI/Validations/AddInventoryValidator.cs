using DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Messages;
using FluentValidation;
using System.Linq;

namespace DDDTW.CoffeeShop.Inventories.WebAPI.Validations
{
    public class AddInventoryValidator : AbstractValidator<AddInventoryMsg>
    {
        public AddInventoryValidator()
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