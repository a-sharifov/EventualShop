﻿using FluentValidation;

namespace ECommerce.Contracts.Catalogs.Validators;

public class CatalogItemAddedValidator : AbstractValidator<DomainEvent.CatalogItemAdded> { }