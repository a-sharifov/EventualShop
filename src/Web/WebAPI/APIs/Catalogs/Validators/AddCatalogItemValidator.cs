using Contracts.DataTransferObjects.Validators;
using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class AddCatalogItemValidator : AbstractValidator<Requests.AddCatalogItem>
{
    public AddCatalogItemValidator()
    {
        RuleFor(request => request.CatalogId)
            .NotNull()
            .NotEmpty();

        RuleFor(request => request.InventoryId)
            .NotNull()
            .NotEmpty()
            .NotEqual(request => request.CatalogId);

        RuleFor(request => request.Quantity)
            .GreaterThan(0);

        RuleFor(request => request.Sku)
            .NotNull()
            .NotEmpty();

        RuleFor(request => request.UnitPrice)
            .GreaterThan(0);

        RuleFor(request => request.Product)
            .SetValidator(new ProductValidator())
            .OverridePropertyName(string.Empty);
    }
}