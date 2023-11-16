﻿using Application.Abstractions.Services;
using Application.Services.PayPal;
using Application.Services.PayPal.Http;
using Domain.Entities.PaymentMethods;
using Domain.ValueObjects.PaymentOptions.PayPals;

namespace Infrastructure.HTTP.PayPals;

public class PayPalPaymentService(IPayPalHttpClient client) : PaymentService, IPayPalPaymentService
{
    public override Task<IPaymentResult>? HandleAsync(Func<IPaymentService, PaymentMethod, CancellationToken, Task<IPaymentResult>> behaviorProcessor, PaymentMethod method, CancellationToken cancellationToken)
        => method.Option is PayPal
            ? behaviorProcessor(this, method, cancellationToken)
            : base.HandleAsync(behaviorProcessor, method, cancellationToken);

    public override async Task<IPaymentResult?> AuthorizeAsync(PaymentMethod method, CancellationToken cancellationToken)
    {
        // TODO - Use method to hydrate
        Requests.PaypalAuthorizePayment request = new();
        var response = await client.AuthorizeAsync(request, cancellationToken);
        return response.ActionResult;
    }

    public override async Task<IPaymentResult> CancelAsync(PaymentMethod method, CancellationToken cancellationToken)
    {
        // TODO - Use method to hydrate
        Requests.PaypalCancelPayment request = new();
        var response = await client.CancelAsync(Guid.NewGuid(), request, cancellationToken);
        return response.ActionResult;
    }

    public override async Task<IPaymentResult> RefundAsync(PaymentMethod method, CancellationToken cancellationToken)
    {
        // TODO - Use method to hydrate
        Requests.PaypalRefundPayment request = new();
        var response = await client.RefundAsync(Guid.NewGuid(), request, cancellationToken);
        return response.ActionResult;
    }
}