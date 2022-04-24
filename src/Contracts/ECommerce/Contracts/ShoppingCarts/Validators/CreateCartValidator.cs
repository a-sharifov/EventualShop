﻿using FluentValidation;

namespace ECommerce.Contracts.ShoppingCarts.Validators;

public class CreateCartValidator : AbstractValidator<Command.CreateCart>
{
    public CreateCartValidator()
    {
        RuleFor(cart => cart.CustomerId)
            .NotEqual(Guid.Empty);
    }
}