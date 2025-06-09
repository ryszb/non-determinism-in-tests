namespace NonDeterminismInTests.Tests.Refactored;

public static class RandomNumberGenerator
{
    private static readonly Random _random = new();

    public static int AnyInt(int min, int max) => _random.Next(min, max);

    public static int AnyInt() => AnyInt(int.MinValue, int.MaxValue);
}