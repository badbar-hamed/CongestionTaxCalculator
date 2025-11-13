using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.Core;

/// <summary>
/// Represents the aggregate root.
/// </summary>
public abstract class AggregateRoot<TKey> : Entity<TKey>
{
    protected AggregateRoot(TKey id) : base(id)
    {
    }

    protected AggregateRoot()
    {
    }

    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void AddDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);
}

