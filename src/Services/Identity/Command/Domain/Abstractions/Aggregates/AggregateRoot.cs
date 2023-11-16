﻿using Contracts.Abstractions.Messages;
using Domain.Abstractions.Entities;
using FluentValidation;
using Newtonsoft.Json;

namespace Domain.Abstractions.Aggregates;

public abstract class AggregateRoot<TValidator> : Entity<TValidator>, IAggregateRoot
    where TValidator : IValidator, new()
{
    private readonly List<IDomainEvent> _events = new();

    public uint Version { get; private set; }

    [JsonIgnore]
    public IEnumerable<IDomainEvent> UncommittedEvents
        => _events.AsReadOnly();

    public void LoadFromHistory(IEnumerable<IDomainEvent> events)
    {
        foreach (var @event in events)
        {
            Apply(@event);
            Version = @event.Version;
        }
    }

    public abstract void Handle(ICommand command);

    protected void RaiseEvent<TEvent>(Func<uint, TEvent> func) where TEvent : IDomainEvent
        => RaiseEvent((func as Func<uint, IDomainEvent>)!);

    protected void RaiseEvent(Func<uint, IDomainEvent>onRaise)
    {
        Version++;
        var @event = onRaise(Version);
        Apply(@event);
        Validate();
        _events.Add(@event);
    }

    protected abstract void Apply(IDomainEvent @event);
}