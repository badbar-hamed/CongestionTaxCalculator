using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CongestionTaxCalculator.Domain.Entities;

namespace CongestionTaxCalculator.Domain.Aggregates;

public sealed class CongestionTax
{
    private readonly List<Passage> _passages = new();
    private readonly IReadOnlyCollection<Passage> _readOnlyPassages;

    public CongestionTax(Vehicle vehicle, DateOnly date, IEnumerable<Passage>? passages = null)
    {
        Vehicle = vehicle ?? throw new ArgumentNullException(nameof(vehicle));
        Date = date;
        _readOnlyPassages = new ReadOnlyCollection<Passage>(_passages);

        if (passages is not null)
        {
            foreach (var passage in passages)
            {
                AddPassage(passage);
            }
        }
    }

    public Vehicle Vehicle { get; }

    public DateOnly Date { get; }

    public IReadOnlyCollection<Passage> Passages => _readOnlyPassages;

    public void AddPassage(Passage passage)
    {
        if (passage is null)
        {
            throw new ArgumentNullException(nameof(passage));
        }

        if (passage.Date != Date)
        {
            throw new ArgumentException("Passage date must match the congestion tax date.", nameof(passage));
        }

        _passages.Add(passage);
    }

    public void AddPassages(IEnumerable<Passage> passages)
    {
        if (passages is null)
        {
            throw new ArgumentNullException(nameof(passages));
        }

        foreach (var passage in passages)
        {
            AddPassage(passage);
        }
    }

    public IReadOnlyList<Passage> GetPassagesOrdered() => _passages.OrderBy(passage => passage.Timestamp).ToList();
}
