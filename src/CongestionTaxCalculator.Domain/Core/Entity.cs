using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Domain.Core;

/// <summary>
/// Represents the base class that all entities derive from.
/// </summary>
public abstract class Entity<TKey> : IEquatable<Entity<TKey>>
{
    protected Entity(TKey id)
    {
        Id = id;
    }

    protected Entity()
    {
    }

    public TKey Id { get; private set; }

    public static bool operator ==(Entity<TKey> a, Entity<TKey> b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }

    public static bool operator !=(Entity<TKey> a, Entity<TKey> b) => !(a == b);

    public bool Equals(Entity<TKey> other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        var other = (Entity<TKey>)obj;

        return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}

