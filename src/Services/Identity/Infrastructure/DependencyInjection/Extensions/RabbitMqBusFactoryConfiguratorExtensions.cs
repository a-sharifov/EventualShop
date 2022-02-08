﻿using Application.UseCases.Events.Projections;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Identity;
using MassTransit;

namespace Infrastructure.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext registration)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectUserDetailsWhenUserChangedConsumer, DomainEvents.UserRegistered>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectUserDetailsWhenUserChangedConsumer, DomainEvents.UserPasswordChanged>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectUserDetailsWhenUserChangedConsumer, DomainEvents.UserDeleted>(registration);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext registration)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"identity.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(registration);
            });
}