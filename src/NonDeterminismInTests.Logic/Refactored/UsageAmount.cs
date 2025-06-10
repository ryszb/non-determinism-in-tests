namespace NonDeterminismInTests.Logic.Refactored;

public class UsageAmount
{
    public int Value { get; }

    public UsageAmount(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Usage amount cannot be negative.");

        Value = value;
    }

    public static implicit operator int(UsageAmount usage) => usage.Value;
}
