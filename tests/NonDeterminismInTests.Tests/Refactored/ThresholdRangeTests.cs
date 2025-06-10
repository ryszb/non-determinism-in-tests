using NonDeterminismInTests.Logic.Refactored;
using static NonDeterminismInTests.Tests.Refactored.RandomNumberGenerator;

namespace NonDeterminismInTests.Tests.Refactored;

public class ThresholdRangeTests
{
    [Fact]
    public void Ctor_allows_valid_min_and_max()
    {
        var min = new UsageAmount(AnyIntBiggerOrEqualThan(0));
        var max = new UsageAmount(AnyIntBiggerThan(min.Value));

        var range = new ThresholdRange(min, max);

        Assert.Equal(min.Value, range.Min.Value);
        Assert.Equal(max.Value, range.Max.Value);
    }

    [Fact]
    public void Ctor_throws_when_max_is_less_than_min_or_equal()
    {
        var min = new UsageAmount(AnyIntBiggerOrEqualThan(0));
        var max = new UsageAmount(AnyInt(0, min.Value + 1));

        Assert.Throws<ArgumentException>(() => new ThresholdRange(min, max));
    }

    [Theory]
    [MemberData(nameof(CalculateUsageTestData))]
    public void Calculates_upgrade_pressure_correctly_for_randomized_inputs(
        int minThreshold, int maxThreshold, int usage, decimal expected)
    {
        var range = new ThresholdRange(
            new UsageAmount(minThreshold),
            new UsageAmount(maxThreshold));

        var result = range.CalculateUpgradePressure(new UsageAmount(usage));

        Assert.True(
            result == expected,
            $"Failed for minThreshold={minThreshold}, maxThreshold={maxThreshold}, usage={usage}. Expected: {expected}, Actual: {result}");
    }

    public static IEnumerable<object[]> CalculateUsageTestData()
    {
        int min = AnyIntBiggerThan(0);
        int max = AnyIntBiggerThan(min);
        
        int evenRange = AnyInt(2, (int.MaxValue  - min) / 2) * 2; // guarantees even difference without causing overflow

        yield return new object[] { min, min + evenRange, min + evenRange / 2, 0.5m };   // Midpoint
        yield return new object[] { min, max, min, 0.0m };                               // Exactly at min
        yield return new object[] { min, max, max, 1.0m };                               // Exactly at max
        yield return new object[] { min, max, min - 1, 0.0m };                           // Below min (clamped)
        yield return new object[] { min, max, max + 1, 1.0m };                           // Above max (clamped)
    }

    [Fact]
    public void Calculate_upgrade_pressure_returns_expected_value_when_usage_is_within_range()
    {
        int minThreshold = AnyIntBiggerOrEqualThan(0);
        int maxThreshold = AnyIntBiggerThan(minThreshold);
        int usage = AnyInt(minThreshold, maxThreshold);

        var range = new ThresholdRange(
            new UsageAmount(minThreshold),
            new UsageAmount(maxThreshold));

        var result = range.CalculateUpgradePressure(new UsageAmount(usage));

        Assert.InRange(result, 0.0m, 1.0m);
    }
}
