namespace NonDeterminismInTests.Logic.Refactored;

public class ThresholdRange
{
    public UsageAmount Min { get; }
    public UsageAmount Max { get; }

    public ThresholdRange(UsageAmount min, UsageAmount max)
    {
        if (min.Value >= max.Value)
            throw new ArgumentException($"Invalid range: min ({min.Value}) must be less than max ({max.Value}).");

        Min = min;
        Max = max;
    }

    public decimal CalculateUpgradePressure(UsageAmount usage)
    {
        if (usage.Value <= Min.Value)
            return 0.0m;

        if (usage.Value >= Max.Value)
            return 1.0m;

        var range = Max.Value - Min.Value;
        var delta = usage.Value - Min.Value;

        return (decimal)delta / range;
    }
}
