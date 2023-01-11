using FluentValidation;

namespace WebAPI.APIs.Catalogs.Validators;

public class CreateCatalogPayloadValidator : AbstractValidator<Payload.CreateCatalog>
{
    public CreateCatalogPayloadValidator()
    {
        RuleFor(request => request.Description)
            .NotNull()
            .NotEmpty();

        RuleFor(request => request.Title)
            .NotNull()
            .NotEmpty();

        RuleFor(request => request.CatalogId)
            .NotNull()
            .NotEmpty();
    }
}